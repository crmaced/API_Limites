using FluentValidator;
using FluentValidator.Validation;

namespace API_Limites.Validadores
{
    public class ValidaCNPJ : Notifiable
    {
        public ValidaCNPJ(string numero)
        {
            this.Numero = numero;

            AddNotifications(new ValidationContract().IsTrue(Validate(this.Numero), "CNPJ", "CNPJ Inválido"));
        }

        public string Numero { get; private set; }

        public override string ToString()
        {
            return this.Numero;
        }

        public bool Validate(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCnpj;
            string digito;
            int soma;
            int resto;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length > 14)
                return false;
            if (cnpj.Length < 14)
                cnpj = cnpj.PadLeft(14, '0');

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

    }
}
