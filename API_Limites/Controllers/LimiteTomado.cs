using API_Limites.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Numerics;
using System.Drawing;
using System.Security.Cryptography;

namespace API_Limites.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimiteTomado : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public LimiteTomado (IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //Acrescentar Limite
        [HttpPut("{id}")]
        public JsonResult Put(Modelos.LimiteTomado update, int id)
        {
            decimal limiteDisponivel = 0;
            decimal saldoAnterior = 0;
            string query = @"UPDATE [dbo].[Limites] SET [Limite_Tomado] = @novoSaldo WHERE [CD_Cliente] = @CD_Cliente";
            string check1 = @"SELECT [Limite_Disponivel] FROM [dbo].[Limites] WHERE [CD_Cliente] = @CD_Cliente";
            string check2 = @"SELECT [Limite_Tomado] FROM [dbo].[Limites] WHERE [CD_Cliente] = @CD_Cliente";

            DataTable tabela = new DataTable();
            SqlDataReader reader;

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
            {

                SqlCommand buscaLimiteDisponivel = new SqlCommand(check1, conexao);
                buscaLimiteDisponivel.Parameters.AddWithValue("@CD_Cliente", id);

                try
                {
                    conexao.Open();
                    limiteDisponivel = (decimal)buscaLimiteDisponivel.ExecuteScalar();
                }
                catch { }

                SqlCommand buscaLimiteAnterior = new SqlCommand(check2, conexao);
                buscaLimiteAnterior.Parameters.AddWithValue("@CD_Cliente", id);

                try
                {
                    saldoAnterior = (decimal)buscaLimiteAnterior.ExecuteScalar();
                }
                catch { }

                SqlCommand insereValor = new SqlCommand(query, conexao);
                {
                    decimal novoSaldo = saldoAnterior + update.Limite_Tomado;
                    insereValor.Parameters.AddWithValue("@novoSaldo", novoSaldo);
                    insereValor.Parameters.AddWithValue("@CD_Cliente", id);

                    if (update.Limite_Tomado <= 0)
                    {
                        return new JsonResult("Não é possível inserir valores igual ou menor a 0. Por favor, informe um valor válido.");
                    }
                    else
                    {
                        if (update.Limite_Tomado > limiteDisponivel)
                        {
                            return new JsonResult("O valor informado é maior que o limite disponível.");
                        }

                        else
                        {
                            //return new JsonResult("Limite disponível" + limiteDisponivel + "Saldo Anterior" + saldoAnterior + "Novo saldo" + novoSaldo);
                            reader = insereValor.ExecuteReader();
                            tabela.Load(reader);
                            reader.Close();
                        }
                    }
                conexao.Close();
                }

            }
            return new JsonResult("Limite Tomado alterado com sucesso.");
        }
        //Fim - Definir Limite Total
    }
}
