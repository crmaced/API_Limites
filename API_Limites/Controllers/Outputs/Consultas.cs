using API_Limites.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Numerics;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Collections;
using API_Limites.Comandos;
using API_Limites.Controllers.Inputs;

namespace API_Limites.Controllers.Outputs
{
    [Route("api/[controller]")]
    [ApiController]
    public class Consultas : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public Consultas(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //Listar todos os clientes e saldos
        [HttpGet]
        [Route("Clientes e Saldos")]
        public JsonResult ListarClientesESaldos()
        {
            var resultado = new List<ListarClientesESaldosCommand>();
            SqlDataReader reader;
            string query = @"SELECT [CD_Cliente],[Nome_RazaoSocial],[ID_Tipo],[Tipo],[CPF_CNPJ],[Conta],[Email],[ID_Status],[Status], [Limite_Total],[Limite_Tomado],[Limite_Disponivel] FROM [dbo].[VW_LIMITES]";

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
            {
                conexao.Open();

                using (SqlCommand lista = new SqlCommand(query, conexao))
                {
                    reader = lista.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            resultado.Add(new ListarClientesESaldosCommand
                            {
                                CD_Cliente = reader.GetInt32(0),
                                Nome_RazaoSocial = reader.GetString(1),
                                ID_Tipo = reader.GetInt32(2),
                                Tipo = reader.GetString(3),
                                CPF_CNPJ = reader.GetString(4),
                                Conta = reader.GetString(5),
                                Email = reader.GetString(6),
                                ID_Status = reader.GetInt32(7),
                                Status = reader.GetString(8),
                                Limite_Total = reader.GetDecimal(9),
                                Limite_Tomado = reader.GetDecimal(10),
                                Limite_Disponivel = reader.GetDecimal(11)
                               
                            });

                        }
                    }

                    else
                    {
                        return new JsonResult("Não há clientes cadastrados.");
                    }
                    reader.Close();
                    conexao.Close();
                }
            }

            return new JsonResult(resultado);
        }
        //Fim - Listar todos os clientes e saldos

        //Listar todos os clientes
        [HttpGet]
        [Route("Clientes")]
        public JsonResult ListarClientes()
        {
            var resultado = new List<ListarClientesCommand>();
            SqlDataReader reader;
            string query = @"SELECT [CD_Cliente],[Nome_RazaoSocial],[ID_Tipo],[Tipo],[CPF_CNPJ],[Conta],[Email],[ID_Status],[Status] FROM [dbo].[VW_LIMITES]";

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
            {
                conexao.Open();

                using (SqlCommand lista = new SqlCommand(query, conexao))
                {
                    reader = lista.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            resultado.Add(new ListarClientesCommand
                            {
                                CD_Cliente = reader.GetInt32(0),
                                Nome_RazaoSocial = reader.GetString(1),
                                ID_Tipo = reader.GetInt32(2),
                                Tipo = reader.GetString(3),
                                CPF_CNPJ = reader.GetString(4),
                                Conta = reader.GetString(5),
                                Email = reader.GetString(6),
                                ID_Status = reader.GetInt32(7),
                                Status = reader.GetString(8),
                                
                            });

                        }
                    }

                    else
                    {
                        return new JsonResult("Não há clientes cadastrados.");
                    }
                    reader.Close();
                    conexao.Close();
                }
            }

            return new JsonResult(resultado);
        }
        //Fim - Listar todos os clientes


        //Buscar um cliente por ID
        //[HttpGet("{id}")]
        //[Route("Buscar Cliente")]
        //public JsonResult BuscaCliente (int id)
        //{
        //    var resultado = new List<BuscaClienteCommand>();
        //    SqlDataReader reader;
        //    string query = @"SELECT [Nome_RazaoSocial],[ID_Tipo],[Tipo],[CPF_CNPJ],[Conta],[Email],[ID_Status],[Status],[Limite_Total],[Limite_Tomado],[Limite_Disponivel] from [dbo].[VW_LIMITES] WHERE [CD_Cliente] = @CD_Cliente";

        //    using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
        //    {
        //        conexao.Open();

        //        using (SqlCommand getIDcommand = new SqlCommand(query, conexao))
        //        {
        //            getIDcommand.Parameters.AddWithValue("@CD_Cliente", id);
        //            reader = getIDcommand.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    resultado.Add(new BuscaClienteCommand
        //                    {
        //                        //CD_Cliente = reader.GetInt32(0),
        //                        Nome_RazaoSocial = reader.GetString(0),
        //                        ID_Tipo = reader.GetInt32(1),
        //                        Tipo = reader.GetString(2),
        //                        CPF_CNPJ = reader.GetString(3),
        //                        Conta = reader.GetString(4),
        //                        Email = reader.GetString(5),
        //                        ID_Status = reader.GetInt32(6),
        //                        Status = reader.GetString(7),
        //                        Limite_Total = reader.GetDecimal(8),
        //                        Limite_Tomado = reader.GetDecimal(9),
        //                        Limite_Disponivel = reader.GetDecimal(10)
        //                    });
        //                }
        //            }

        //            else
        //            {
        //                return new JsonResult("Cliente não cadastrado.");
        //            }
        //            reader.Close();
        //            conexao.Close();
        //        }
        //    }

        //    return new JsonResult(resultado);
        //}
        //Fim - Buscar um cliente por ID
    }
}
