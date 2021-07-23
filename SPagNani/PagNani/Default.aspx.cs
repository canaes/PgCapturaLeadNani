using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SPagNani.DAL;

namespace PagNani
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbErro.Visible = false;
        }

        protected void Salvar(object sender, EventArgs e)
        {
            try
            {
                string Nome = "";
                string Email = "";
                string campanha = "Maratona Grátis";

                if (txtNome.Value == "" || txtEmail.Value == "")
                {
                    lbErro.Visible = true;
                    lbErro.Text = "Campos em branco.";
                    return;
                }
                Nome = txtNome.Value;
                Email = txtEmail.Value;

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                cmd.CommandText = $"INSERT INTO {Schema.tbLeads} (NAME, EMAIL, CAMPANHA) VALUES "
                                + $"('{Nome}', '{Email}', '{campanha}')";

                new SPagNani.DAL.ConnectionDB().MySqlExecute(ref cmd);

                string mensagem = 
                    "Olá, tudo bem?<br/>Gostaria de te dar os parabéns!<br/>"
                    + "<br/>A sua inscrição para participar na <strong>Maratona de Baralho Cigano Iniciante</strong> que vai acontecer no <strong>mês de Agosto </strong> foi concluída com sucesso!"
                    + "<br/>O curso é inteiramente grátis e você irá aprender os significados das cartas e técnicas de tiragens simples."
                    + "<br/>Ao final do curso você será capaz de fazer suas primeiras leituras. Você vai se apaixonar!"

                    + "<br/><br/>Não se esqueça de entrar no grupo de whatsapp! É super importante para que você não perca as orientações sobre o curso, ok?"
                    + "<br/><br/>Clique aqui e entre agora no grupo: <a href='https://chat.whatsapp.com/ENtKxxVLlRaB4gNUjm1YCd' target='_blank'>Link do Grupo</a>"

                    + "<br/><br/>E como prometido, estou disponibilizando para você o <a href='https://maratonagratis.nanimattos.trixxfs.com/Arquivos/Costumes_e_curiosidade_dos_povos_ciganos.pdf' target='_blank'>download</a> de um livro que escrevi com muito carinho sobre <strong>Os Costumes e Curiosidades do Povo Cigano</strong>"
                    + "<br/><br/>Esta é uma oportunidade incrível para você adquirir bastante conhecimento a respeito da magia do Povo Cigano."

                    + "<br/><br/><br/>Um beijo e muita luz!"
                    + "<br/><i>Nani Mattos</i>";

                SendEmail(Email, "Você já garantiu a sua vaga", mensagem);
                Response.Redirect("InscricaoConfirmada.aspx");
            }
            catch (Exception ex)
            {
            }
        }

        /*
			Caso prefira não permitir o acesso de aplicativos menos seguros, a seguinte solução pode ser adotada:

			Logado na conta do gmail que será usada pelo site selecione "Minha Conta"
			Selecione a opção "Login e segurança"
			Ative a "Verificação em duas etapas" (siga os procedimentos solicitados)
			Selecione a opção "Senhas de app" (siga os procedimentos solicitados)
			Selecione a opção "Selecionar app"
			Escolha a opção "Outro (nome personalizado)"
			Informe o nome do seu site / webapp
			Copie a senha gerada pelo google
			No trecho de código abaixo, informe a senha gerada ao invés da senha da conta:

			SmtpClient.Credentials = new NetworkCredential("raffa.ferreiira@gmail.com","senha gerada pelo google");

			Isto feito, seu webapp ou site estará habilitado a conectar na conta sem ser barrado pelo google security e sem reduzir o nível de segurança.		
		*/

        public static string serverSMTP = "smtp.gmail.com";

        public bool SendEmail(string argAddress, string argSubj, string argBody)
        {
            /*string myEmail = "norepply.hellocorp@gmail.com";
            string pwd = "llghtpigmodqetjg";*/
            string myEmail = "naorespondananimattos@gmail.com"; //"florenzaviagens.noreply.webmaster@gmail.com";
            string pwd = "msclmzptixttzspy";


            MailMessage objEmail = new MailMessage();
            objEmail.From = new MailAddress(myEmail, "Nanni Mattos - Divulgação", System.Text.Encoding.UTF8);
            if (argAddress.Contains(";"))
            {
                string[] vetAdd = argAddress.Split(';');
                for (int i = 0; i < vetAdd.Length; i++)
                {
                    objEmail.To.Add(vetAdd[i]);
                }
            }
            else
            {
                objEmail.To.Add(argAddress);
            }

            objEmail.Priority = MailPriority.High;
            objEmail.IsBodyHtml = true;
            objEmail.Subject = argSubj;
            objEmail.Body = argBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.IsBodyHtml = true;

            var objSmtp = new System.Net.Mail.SmtpClient();
            {
                objSmtp.UseDefaultCredentials = false;
                objSmtp.Host = serverSMTP;
                objSmtp.Port = 587;
                objSmtp.Credentials = new System.Net.NetworkCredential(myEmail, pwd);
                objSmtp.EnableSsl = true;
                objSmtp.Timeout = 15000;
            }
            try
            {
                objSmtp.Send(objEmail);
                return true;
            }
            catch (SmtpException ex)
            {
                //new ConnectionDB().InsertErrorLogOnDB(JsonConvert.SerializeObject(ex));
                return false;
            }
        }

        //public string FormatMsgToEmail(Infra.Entity.PurchaseSite item)
        //{
        //    string esp = "<br/><strong>";
        //    string response = "";

        //    response = $"<strong>Data da Compra</strong>: {item.DataCompra} {esp} VEIO ATRAVÉS DE</strong>: {item.Produto.DescricaoProduto} {esp} "
        //        + $"NOME:</strong> {item.Name} {esp} EMAIL</strong>: {item.Email} {esp} DDD</strong>: {item.DDD} {esp} TELEFONE</strong>: {item.Telefone} {esp}"
        //        + $"RG:</strong> {item.RG} {esp} CPF</strong>: {item.CPF} {esp} PREÇO</strong>: {item.Produto.Preco} {esp}"
        //        + $" OBS</strong>: {item.Obs}";
        //    return response;
        //}
    }
}