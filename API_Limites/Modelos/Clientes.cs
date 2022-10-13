using Microsoft.AspNetCore.Mvc;
using API_Limites.Controllers;

namespace API_Limites.Modelos
{
    public class Clientes
    {
        public int CD_Cliente { get; set; }

        public string Nome_RazaoSocial { get; set; }

        public string ID_Tipo { get; set; }

        public string CPF_CNPJ { get; set; }

        public string Conta { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }


    }
}
