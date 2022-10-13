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
    public class LimiteTotal : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public LimiteTotal(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //Definir Limite Total
        [HttpPut("{id}")]
        public JsonResult Put(Modelos.LimiteTotal update, int id)
        {
            string query = @"UPDATE [dbo].[Limites] SET [Limite_Total] = @Limite_Total WHERE [CD_Cliente] = @CD_Cliente";
            string check = @"SELECT [Limite_Tomado] FROM [dbo].[Limites] WHERE [CD_Cliente] = @CD_Cliente";
            decimal lim = 0;

            DataTable tabela = new DataTable();
            SqlDataReader reader;
            
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
            {
                //conexao.Open();

                SqlCommand checkvalor = new SqlCommand(check, conexao);
                checkvalor.Parameters.AddWithValue("@CD_Cliente", id);

                try
                {
                    conexao.Open();
                    lim = (decimal)checkvalor.ExecuteScalar();
                }
                catch { }
                
                using (SqlCommand definelimite = new SqlCommand(query, conexao))
                {
                    //conexao.Open();
                    definelimite.Parameters.AddWithValue("@Limite_Total", update.Limite_Total);
                    definelimite.Parameters.AddWithValue("@CD_Cliente", id);

                    if (update.Limite_Total <= 0)
                    {
                        return new JsonResult("O limite não pode ser menor ou igual a 0. Por favor, insira um valor válido.");
                    }
                    if (update.Limite_Total < lim)
                    {
                        return new JsonResult("O limite informado é maior do que o limite já utilizado.");
                    }

                    else
                    {
                        reader = definelimite.ExecuteReader();
                        tabela.Load(reader);
                        reader.Close();
                    }

                    conexao.Close();
                }

            }
            return new JsonResult("Limite Total alterado com sucesso.");
        }
        //Fim - Definir Limite Total
    }
}
