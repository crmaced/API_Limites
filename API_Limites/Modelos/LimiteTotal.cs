using System.Data.SqlTypes;

namespace API_Limites.Modelos
{
    public class LimiteTotal
    {
        public int CD_Cliente { get; private set; }

        public decimal Limite_Total {get; set;}
    }
}
