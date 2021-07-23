using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagNani.Modelo
{
    public class Login
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class ResponseLogin
    {
        public bool Sucesso { get; set; }
        public string MensagemErro { get; set; }
        public int CodigoHtml { get; set; }

        public Pessoa Pessoa { get; set; }
        public string TokenAcesso { get; set; }
    }

    public class Pessoa
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string NivelAcesso { get; set; }
    }
}