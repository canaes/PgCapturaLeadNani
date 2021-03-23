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
                string campanha = "Magia das Cartas";

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
                    + "<br/>A sua inscrição para participar do meu Workshop que <strong>vai acontecer nos dias 22, 23 e 24 de março</strong> foi concluída com sucesso!"
                    + "<br/>Marque na sua agenda, <strong>sempre às 20h30</strong>. Será transmitido pelo Instagram e Youtube."
                    + "<br/><br/>Participe do nosso <a href='https://chat.whatsapp.com/F5NYQ0nqhBc3vF6lp7RNu8'>grupo no whatsapp</a> para acompanhar as novidades!!"

                    + "<br/><br/>Eu quero te apresentar aqui, um pouco do que você vai aprender nestes dias. Veja a programação!"                    

                    + "<br/><br/><strong>Dia 22 de março</strong>"
                    + "<br/>Tema: Como adquirir e guardar o seu primeiro Baralho Cigano"
                    + "<br/>Assuntos abordados:"
                    + "<br/>-	Quais são os diferentes tipos de baralhos"
                    + "<br/>-	Como saber qual deles é para você"
                    + "<br/>-	Como ouvir a sua intuição"
                    + "<br/>-	Quais os outros locais que pode armazenar o baralho"

                    + "<br/><br/><strong>Dia 23 de março</strong>"
                    + "<br/>Tema: Os elementos essenciais para usar na consulta"
                    + "<br/>Assuntos abordados:"
                    + "<br/>-	os 4 elementos"
                    + "<br/>-	como montar uma mesa"
                    + "<br/>-	que tipo de lenço usar na mesa"
                    + "<br/>-	posição de cada elemento na mesa"
                    + "<br/>-	como se proteger espiritualmente de energias negativas"
                    + "<br/>-	como consagrar o Baralho Cigano"
                    + "<br/>-	por que é importante a consagração?"
                    + "<br/>-	qual a importância de cada elemento na consagração"
                    + "<br/>-	como programar o Baralho Cigano para trabalhar com a sua energia"
                    + "<br/>-	quando é hora de fazer nova consagração"
                    + "<br/>-	o que fazer após a consagração"

                    + "<br/><br/><strong>Dia 24 de março</strong>"
                    + "<br/>Tema: Técnicas de leitura mais usadas"
                    + "<br/>Assuntos abordados:"
                    + "<br/>-	quais os tipos de tiragem que você pode trabalhar"
                    + "<br/>-	por que cada tarólogo trabalha do seu jeito?"
                    + "<br/>-	como encontrar o seu próprio método de trabalho"
                    + "<br/>-	as manifestações do Baralho Cigano nas consultas"
                    + "<br/>-	por onde começar?"
                    + "<br/>Esta é uma oportunidade incrível para você adquirir bastante conhecimento a respeito da magia do Povo Cigano."
                    + "<br/><br/>Agora, se você tem dúvida se vale a pena ocupar o seu tempo com isso, ou se não sabe se o Baralho Cigano é pra você, eu preparei uma aula especial esclarecendo todas as dúvidas a respeito desse assunto."
                    + "<br/><br/>Assista a esta aula e veja se o Baralho Cigano é mesmo pra você:"
                    + "<br/><br/><a href='https://youtu.be/YpaUQqxSVbM'>Link para o meu vídeo</a>"
                    + "<br/><br/>Esta pode ser aquela resposta que você estava esperando do Universo para sua vida."
                    + "<br/>Um beijo e muita luz!"
                    + "<br/><br/><br/>Te espero nas aulas, viu?"
                    + "<br/>Nani Mattos";

                SendEmail(Email, "Parabéns, você já garantiu a sua vaga! ", mensagem);
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
            objEmail.From = new MailAddress(myEmail, "", System.Text.Encoding.UTF8);
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