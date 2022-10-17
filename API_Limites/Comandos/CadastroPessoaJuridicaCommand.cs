using API_Limites.Validadores;
using FluentValidator;
using FluentValidator.Validation;
using System.Windows.Input;

namespace API_Limites.Comandos
{
    public class CadastroPessoaJuridicaCommand : Notifiable
    {
        public int CD_Cliente { get; private set; }
        public string RazaoSocial { get; set; }
        public int ID_Tipo { get; private set; }
        public string Tipo { get; private set; }
        public string CNPJ { get; set; }
        public string Conta { get; set; }
        public string Email { get; set; }
        public string Status { get; private set; }
        public int ID_Status { get; private set; }

        public CadastroPessoaJuridicaCommand()
        {
            this.Tipo = "Pessoa Jurídica";
            this.Status = "Ativo";
        }

        public bool isValid()
        {
            ValidaCNPJ _cnpj = new ValidaCNPJ(CNPJ);

            AddNotifications(new ValidationContract()
            .IsNotNullOrEmpty(this.CNPJ, "CNPJ", "CNPJ é um campo origatório")
            .IsNotNullOrEmpty(this.RazaoSocial, "RazaoSocial", "Razão Social é um campo obrigatorio")
            .IsNotNullOrEmpty(this.Conta, "Conta", "Conta é um campo obrigatorio")
            .IsNotNullOrEmpty(this.Email, "Email", "Email é um campo obrigatorio")
            .HasLen(CNPJ, 14, "CNPJ", "CNPJ deve ser numérico com 14 digitos (zeros a esquerda)")
            .HasLen(Conta, 5, "Conta", "Número da conta deve 5 dígitos (zeros a esquerda)")
        );

            AddNotifications(_cnpj.Notifications);
            return Valid;

        }


    }

}
