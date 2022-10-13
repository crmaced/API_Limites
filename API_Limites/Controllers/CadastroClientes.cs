using API_Limites.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Numerics;
using System.Collections.Generic;

namespace API_Limites.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroClientes : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public CadastroClientes(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //Cadastro de Cliente
        [HttpPost]
        public JsonResult Post(Clientes cadastro)
        {
            DataTable tabela = new DataTable();
            SqlDataReader reader;
            string query = "INSERT INTO [Cadastro_Clientes] ([Nome_RazaoSocial], [ID_Tipo], [CPF_CNPJ], [Conta], [Email], [Status]) VALUES(@Nome_RazaoSocial, @ID_Tipo, @CPF_CNPJ, @Conta, @Email, @Status); INSERT INTO LIMITES([CD_Cliente], [Nome_RazaoSocial], [ID_Tipo], [CPF_CNPJ], [Limite_Total],[Limite_Tomado]) VALUES((SELECT[CD_Cliente] FROM[Cadastro_Clientes] WHERE[CPF_CNPJ] = @CPF_CNPJ), @Nome_RazaoSocial, @ID_Tipo, @CPF_CNPJ, 0, 0);";
            
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
            {
                conexao.Open();

                using (SqlCommand postcommand1 = new SqlCommand(query, conexao))
                {
                    postcommand1.Parameters.AddWithValue("@Nome_RazaoSocial", cadastro.Nome_RazaoSocial);
                    postcommand1.Parameters.AddWithValue("@ID_Tipo", cadastro.ID_Tipo);
                    postcommand1.Parameters.AddWithValue("@CPF_CNPJ", cadastro.CPF_CNPJ);
                    postcommand1.Parameters.AddWithValue("@Conta", cadastro.Conta);
                    postcommand1.Parameters.AddWithValue("@Email", cadastro.Email);
                    postcommand1.Parameters.AddWithValue("@Status", cadastro.Status);

                    reader = postcommand1.ExecuteReader();
                    tabela.Load(reader);
                    reader.Close();
                    conexao.Close();
                }
            }
            return new JsonResult("Cliente adicionado com sucesso.");
        }
        //Fim - Cadastro de Cliente

        //Alterar Cadastro de Cliente
        [HttpPut("{id}")]
        public JsonResult Put(Clientes update, int id)
        {
            string query = "UPDATE [dbo].[Cadastro_Clientes] SET [Nome_RazaoSocial] = @Nome_RazaoSocial, [ID_Tipo] = @ID_Tipo, [CPF_CNPJ] = @CPF_CNPJ, [Conta] = @Conta, [Email] = @Email WHERE [CD_Cliente] = @CD_Cliente; UPDATE [dbo].[Limites] SET [CD_Cliente] = @CD_Cliente, [Nome_RazaoSocial] = @Nome_RazaoSocial, [ID_Tipo] = @ID_Tipo, [CPF_CNPJ] = @CPF_CNPJ WHERE [CD_Cliente] = @CD_Cliente;";
            DataTable tabela = new DataTable();
            SqlDataReader reader;

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
            {
                conexao.Open();

                using (SqlCommand updatecommand = new SqlCommand(query, conexao))
                {
                    updatecommand.Parameters.AddWithValue("@Nome_RazaoSocial", update.Nome_RazaoSocial);
                    updatecommand.Parameters.AddWithValue("@ID_Tipo", update.ID_Tipo);
                    updatecommand.Parameters.AddWithValue("@CPF_CNPJ", update.CPF_CNPJ);
                    updatecommand.Parameters.AddWithValue("@Conta", update.Conta);
                    updatecommand.Parameters.AddWithValue("@Email", update.Email);
                    updatecommand.Parameters.AddWithValue("@CD_Cliente", id);
                    
                    reader = updatecommand.ExecuteReader();
                    tabela.Load(reader);
                    reader.Close();
                    conexao.Close();
                }
            }

            return new JsonResult("Cadastro alterado com sucesso.");
        }
        //Fim - Alterar Cadastro de Cliente

        //Listar todos os clientes
        [HttpGet]
        public JsonResult Get()
        {
            var resultado = new List<Clientes>();
            SqlDataReader reader;

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
            {
                conexao.Open();

                using (SqlCommand getcommand = new SqlCommand(@"SELECT [CD_Cliente], [Nome_RazaoSocial], [ID_Tipo], [CPF_CNPJ], [Conta], [Email] from [dbo].[Cadastro_Clientes]", conexao))
                {
                    reader = getcommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            resultado.Add(new Clientes
                            {
                                CD_Cliente = reader.GetInt32(0),
                                Nome_RazaoSocial = reader.GetString(1),
                                ID_Tipo = reader.GetString(2),
                                CPF_CNPJ = reader.GetString(3),
                                Conta = reader.GetString(4),
                                Email = reader.GetString(5)
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


        //Buscar um cliente
        [HttpGet("{id}")]
        public JsonResult GetID (int id)
        {
            var resultado = new List<Clientes>();
            SqlDataReader reader;

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
            {
                conexao.Open();

                using (SqlCommand getIDcommand = new SqlCommand(@"SELECT [CD_Cliente], [Nome_RazaoSocial], [ID_Tipo], [CPF_CNPJ], [Conta], [Email] from [dbo].[Cadastro_Clientes] WHERE [CD_Cliente] = @CD_Cliente", conexao))
                {
                    getIDcommand.Parameters.AddWithValue("@CD_Cliente", id);
                    reader = getIDcommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            resultado.Add(new Clientes
                            {

                                CD_Cliente = reader.GetInt32(0),
                                Nome_RazaoSocial = reader.GetString(1),
                                ID_Tipo = reader.GetString(2),
                                CPF_CNPJ = reader.GetString(3),
                                Conta = reader.GetString(4),
                                Email = reader.GetString(5)
                            }) ;
                        }
                    }

                    else
                    {
                        return new JsonResult("Cliente não cadastrado.");
                    }
                    reader.Close();
                    conexao.Close();
                }
            }

            return new JsonResult(resultado);
        }
        //Fim - Buscar um cliente

        //Alterar Status do Cliente
        
        //Fim - Alterar Status do Cliente

    }
}
