namespace API_Limites.Comandos
{
    public class ListarClientesCommand
    {
        public int CD_Cliente { get; set; }

        public string Nome_RazaoSocial { get; set; }

        public int ID_Tipo { get; set; }

        public string Tipo { get; set; }

        public string CPF_CNPJ { get; set; }

        public string Conta { get; set; }

        public string Email { get; set; }

        public int ID_Status { get; set; }

        public string Status { get; set; }
    }
}
