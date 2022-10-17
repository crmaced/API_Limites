using API_Limites.Validadores;
using FluentValidator;
using FluentValidator.Validation;
using System.Windows.Input;

namespace API_Limites.Comandos
{
    public class CadastroPessoaFisicaCommand : Notifiable
    {
        public int CD_Cliente { get; private set; }
        public string Nome { get; set; }
        public int ID_Tipo { get; private set; }
        public string Tipo { get; private set; }
        public string CPF { get; set; }
        public string Conta { get; set; }
        public string Email { get; set; }
        public string Status { get; private set; }
        public int ID_Status { get; private set; }

        public CadastroPessoaFisicaCommand()

        {
            this.Tipo = "Pessoa Física";
            this.Status = "Ativo";
        }

        public bool isValid()
        {
            ValidaCPF _cpf = new ValidaCPF(CPF);

            AddNotifications(new ValidationContract()
            .IsNotNullOrEmpty(this.CPF, "CPF", "CPF é um campo origatório")
            .IsNotNullOrEmpty(this.Nome, "Nome", "Nome é um campo obrigatorio")
            .IsNotNullOrEmpty(this.Conta, "@Conta", "Conta é um campo obrigatorio")
            .IsNotNullOrEmpty(this.Email, "Email", "Email é um campo obrigatorio")
            .HasLen(CPF, 11, "CPF", "CPF deve ser numérico com 11 digitos (zeros a esquerda)")
            .HasLen(Conta, 5, "@Conta", "Número da conta deve 5 dígitos (zeros a esquerda)")
            );

            AddNotifications(_cpf.Notifications);
            return Valid;

        }
    }
}
