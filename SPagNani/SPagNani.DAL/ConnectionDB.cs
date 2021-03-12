using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace SPagNani.DAL
{
    public class LoadedConnection
    {
        public MySqlConnection Connection{ get; set; }
        public bool inUse { get; set; }

        public LoadedConnection(string argConString, bool argUse = true)
        {
            this.Connection = new MySqlConnection(argConString);
            this.inUse = argUse;
        }
    }

    public class ConnectionDB
    {
        #region Variáveis

        private static MySqlConnection conMySQL = null;
        private static bool onError = false;
        public string msgError = "";
        private string stringConn = $"Server=localhost;Port=3306;Database={Schema.dataBase};Uid={Schema.user};Pwd={Schema.pwd};"; // DESENV

        /// <summary>
        /// Lista todas as conexões
        /// </summary>
        private static System.Collections.Generic.List<LoadedConnection> listConnections = new System.Collections.Generic.List<LoadedConnection>(); 
        #endregion

        public ConnectionDB()
        {
        }
        
        #region Consulta        
        /// <summary>
        /// Realiza um Consulta no banco de dados MySQL de forma Assincrona.
        /// </summary>
        /// <param name="argCmd">MySQL Comand</param>
        /// <param name="argDt">DataTable de retorno -- OUT</param>
        /// <returns></returns>
        public bool MySqlConsult(ref MySqlCommand argCmd, out System.Data.DataTable argDt, int argTimeout = 30)
        {
            bool flg = false;
            argDt = new System.Data.DataTable();
            int index = -1;
            try
            {
                index = GetIndexListLoadedConnection();
                argCmd.Connection = listConnections[index].Connection;
                argCmd.CommandTimeout = argTimeout;
                listConnections[index].Connection.Open();
                argDt.BeginLoadData();
                MySqlDataAdapter da = new MySqlDataAdapter(argCmd);
                System.Threading.Tasks.Task<int> list = da.FillAsync(argDt);
                if (list.Exception != null)
                {
                    int errorNumber = 0;
                    for (int i = 0; i < list.Exception.InnerExceptions.Count; i++)
                    {
                        int number = 0;
                        if (list.Exception.InnerExceptions[i].Source == "MySql.Data")
                        {
                            number = ((MySqlException)list.Exception.InnerExceptions[i]).Number;
                            errorNumber = number;
                        }
                        onError = true;
                        InsertErrorLogOnDB(ref argCmd, number, list.Exception.InnerExceptions[i].Message);
                        onError = false;
                    }
                    throw new Exception("Erro no banco de dados. Informe ao Administrador do Sistema o número: " + errorNumber);
                }
                flg = true;
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042)
                {
                    throw new Exception("Erro ao tentar conectar o banco de dados. Verifique junto ao administrador do sistema.");
                }
                else
                {
                    if (!onError)
                    {
                        onError = true;
                        InsertErrorLogOnDB(ref argCmd, ex.Number, ex.Message);
                        onError = false;
                    }
                    throw new Exception("Erro no banco de dados. Informe ao Administrador do Sistema o número: " + ex.Number + " - " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no banco de dados. Mensagem:" + ex.Message);
            }
            finally
            {
                argDt.EndLoadData();
                CloseConnection(index);
            }
            return flg;
        } 
        #endregion
        #region Execute        
        /// <summary>
        /// Executa um Comando no banco de dados MySQL de forma Assincrona.
        /// </summary>
        /// <param name="argCmd">MySQL Comand</param>
        /// <param name="argTimeout">Tmeout</param>
        /// <returns>Verdadeiro ou falso</returns>
        public bool MySqlExecute(ref MySqlCommand argCmd, int argTimeout = 30)
        {
            bool flg = false;
            int index = -1;
            try
            {
                index = GetIndexListLoadedConnection();
                argCmd.Connection = listConnections[index].Connection;
                listConnections[index].Connection.Open();

                argCmd.CommandTimeout = argTimeout;
                System.Threading.Tasks.Task<int> taskRowAffects = argCmd.ExecuteNonQueryAsync();
                if (taskRowAffects.Exception != null)
                {
                    int errorNumber = 0;                    
                    for (int i = 0; i < taskRowAffects.Exception.InnerExceptions.Count; i++)
                    {
                        int number = 0;
                        if (taskRowAffects.Exception.InnerExceptions[i].Source == "MySql.Data")
                        {
                            number = ((MySqlException)taskRowAffects.Exception.InnerExceptions[i]).Number;
                            errorNumber = number;
                        }
                        onError = true;
                        InsertErrorLogOnDB(ref argCmd, number, "MySqlExecute:taskRowAffects.Exception:" + taskRowAffects.Exception.InnerExceptions[i].Message);
                        onError = false;
                    }
                    switch (errorNumber)
                    {
                        case 1042:
                            throw new Exception("Problemas ao tentar conectar-se ao banco de dados. Informe ao administrador do sistema. (1042)");
                        case 1062:
                            throw new Exception("Dados já cadastrados para outro registro. Cancelado. (1062)");
                        case 1451:
                            throw new Exception("Há outros dados associados à este registro, não sendo possível sua exclusão. (1451)");
                        default:
                            throw new Exception("Problemas no banco de dados. Informe ao Administrador do Sistema o número: " + errorNumber + "");
                    }
                }
                flg = true;
                return flg;
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042)
                {
                    throw new Exception("Erro ao tentar conectar o banco de dados. Verifique junto ao administrador do sistema.");
                }
                else if (ex.Number == 1451)
                {
                    throw new Exception("Há outros dados associados à este registro, não sendo possível sua exclusão. (" + ex.Number + ")");
                }
                else if (ex.Number == 1062)
                {
                    throw new Exception("Dados já cadastrados para outro registro. Cancelado - 1062.");
                }
                else
                {
                    if (!onError)
                    {
                        onError = true;
                        InsertErrorLogOnDB(ref argCmd, ex.Number, ex.Message);
                        onError = false;
                    }
                    throw new Exception("Erro no banco de dados. Informe ao Administrador do Sistema o número: " + ex.Number + " - " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro genérico no banco de dados. Informe ao Administrador do Sistema." + ex.Message);
            }
            finally
            {
                CloseConnection(index);
            }
        } 
        #endregion        
        #region Conexão
        /// <summary>
        /// Obtém o indice da conexão livre, se nenhuma livre, inclui nova Conexão no List
        /// </summary>
        /// <returns>Indice do List com conexão para uso</returns>
        public int GetIndexListLoadedConnection()
        {
            if (listConnections.Count == 0)
            {
                listConnections.Add(new LoadedConnection(stringConn));
                return listConnections.Count - 1;
            }
            try
            {
                int index = listConnections.FindIndex(temp => temp.Connection.State == System.Data.ConnectionState.Closed && !temp.inUse);
                if (index == -1)
                {
                    listConnections.Add(new LoadedConnection(stringConn));
                    return listConnections.Count - 1;
                }
                return index;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Fecha conexão e libera para uso 
        /// </summary>
        /// <param name="argIndex"></param>
        public void CloseConnection(int argIndex)
        {
            try
            {
                listConnections[argIndex].Connection.CloseAsync();
                listConnections[argIndex].inUse = false;
            }
            catch (Exception) { }
        }
        /// <summary>
        /// Remove conexão "de bobeira" do list
        /// </summary>
        /// <param name="argItem"></param>
        public void RemoveIndexListLoadedConnection(LoadedConnection argItem)
        {
            try
            {
                if (argItem.Connection.State == System.Data.ConnectionState.Open)
                {
                    argItem.Connection.Close();
                }
                listConnections.Remove(argItem);
            }
            catch (Exception)
            {
            }
        }
        #endregion
        /// <summary>
        /// Insere um Log de Erros na tabela do Banco de dados.
        /// </summary>
        /// <param name="argCmd">MySQL Comand</param>
        /// <param name="argErrorCode">Código do Erro</param>
        /// <param name="argMsgError">Mensagem de Erro</param>
        /// <returns></returns>
        public bool InsertErrorLogOnDB(ref MySqlCommand argCmd, int argErrorCode, string argMsgError)
        {

            string value = argCmd.CommandText;
            string company = "1";
            value += " (";
            for (int i = 0; i < argCmd.Parameters.Count; i++)
            {
                if (argCmd.Parameters[i].ParameterName.ToUpper().Contains("COMP"))
                {
                    if (argCmd.Parameters[i].MySqlDbType == MySqlDbType.Int32)
                    {
                        company = argCmd.Parameters[i].Value.ToString();
                    }
                }
                value += argCmd.Parameters[i].Value + ",";
            }
            value = value.Remove(value.Length - 1, 1);
            value += ") --- " + argMsgError + " Error Code: " + argErrorCode;


            string query = "INSERT INTO " + Schema.tbError
                + " (ID_ERROR ,VALUE_ERROR ,DT_REGISTER) "
                + " VALUES "
                + " (DEFAULT ,@VALUE ,DEFAULT)";
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@VALUE", value);

            if (new DAL.ConnectionDB().MySqlExecute(ref cmd))
            {
                return true;
            }
            else
            {
                //TODO: nada a fazer ?
                //throw new Exceptions.InsertErrorOnDataBaseException();
                return false;
            }
        }
    }
}