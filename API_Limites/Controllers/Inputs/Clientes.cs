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
using System.Diagnostics.Metrics;
using API_Limites.Comandos;

namespace API_Limites.Controllers.Inputs
{
    [Route("api/[controller]")]
    [ApiController]
    public class Clientes : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public Clientes(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //Cadastro de Cliente - Pessoa Física
        [HttpPost]
        [Route("Cadastro PF")]
        public JsonResult CadastroPessoaFisica(CadastroPessoaFisicaCommand command)
        {
            if (!command.isValid())
            {
                return new JsonResult("Houveram criticas aos parâmetros da solicitação.",
                                      command.Notifications);
            }

            DataTable tabela = new DataTable();
            SqlDataReader reader;
            string query = @"INSERT INTO [Cadastro_Clientes] ([Nome_RazaoSocial], [ID_Tipo], [Tipo], [CPF_CNPJ], [Conta], [Email], [ID_Status], [Status])
                            VALUES(@Nome_RazaoSocial, @ID_Tipo, @Tipo, @CPF_CNPJ, @Conta, @Email, @ID_Status, @Status);
                            INSERT INTO [Limites] ([CD_Cliente], [Nome_RazaoSocial], [CPF_CNPJ], [Limite_Total],[Limite_Tomado])
                            VALUES((SELECT [CD_Cliente] FROM [Cadastro_Clientes] WHERE[CPF_CNPJ] = @CPF_CNPJ), @Nome_RazaoSocial, @CPF_CNPJ, 0, 0);";

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
            {
                conexao.Open();

                using (SqlCommand criacadastro = new SqlCommand(query, conexao))
                {
                    criacadastro.Parameters.AddWithValue("@Nome_RazaoSocial", command.Nome);
                    criacadastro.Parameters.AddWithValue("@ID_Tipo", 1);
                    criacadastro.Parameters.AddWithValue("@Tipo", command.Tipo);
                    criacadastro.Parameters.AddWithValue("@CPF_CNPJ", command.CPF);
                    criacadastro.Parameters.AddWithValue("@Conta", command.Conta);
                    criacadastro.Parameters.AddWithValue("@Email", command.Email);
                    criacadastro.Parameters.AddWithValue("@ID_Status", 1);
                    criacadastro.Parameters.AddWithValue("@Status", command.Status);

                    reader = criacadastro.ExecuteReader();
                    tabela.Load(reader);
                    reader.Close();
                    conexao.Close();
                }
            }
            return new JsonResult("Cliente adicionado com sucesso.");
        }
        //Fim - Cadastro de Cliente - Pessoa Física

        //Cadastro de Cliente - Pessoa Jurídica
        [HttpPost]
        [Route("Cadastro PJ")]
        public JsonResult CadastroPessoaJuridica(CadastroPessoaJuridicaCommand command)
        {
            if (!command.isValid())
            {
                return new JsonResult("Houveram criticas aos parâmetros da solicitação.",
                                      command.Notifications);
            }

            DataTable tabela = new DataTable();
            SqlDataReader reader;
            string query = @"INSERT INTO [Cadastro_Clientes] ([Nome_RazaoSocial], [ID_Tipo], [Tipo], [CPF_CNPJ], [Conta], [Email], [ID_Status], [Status])
                            VALUES(@Nome_RazaoSocial, @ID_Tipo, @Tipo, @CPF_CNPJ, @Conta, @Email, @ID_Status, @Status);
                            INSERT INTO [Limites] ([CD_Cliente], [Nome_RazaoSocial], [CPF_CNPJ], [Limite_Total],[Limite_Tomado])
                            VALUES((SELECT [CD_Cliente] FROM [Cadastro_Clientes] WHERE[CPF_CNPJ] = @CPF_CNPJ), @Nome_RazaoSocial, @CPF_CNPJ, 0, 0);";

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("APICon")))
            {
                conexao.Open();

                using (SqlCommand criacadastro = new SqlCommand(query, conexao))
                {
                    criacadastro.Parameters.AddWithValue("@Nome_RazaoSocial", command.RazaoSocial);
                    criacadastro.Parameters.AddWithValue("@ID_Tipo", 2);
                    criacadastro.Parameters.AddWithValue("@Tipo", command.Tipo);
                    criacadastro.Parameters.AddWithValue("@CPF_CNPJ", command.CNPJ);
                    criacadastro.Parameters.AddWithValue("@Conta", command.Conta);
                    criacadastro.Parameters.AddWithValue("@Email", command.Email);
                    criacadastro.Parameters.AddWithValue("@ID_Status", 1);
                    criacadastro.Parameters.AddWithValue("@Status", command.Status);

                    reader = criacadastro.ExecuteReader();
                    tabela.Load(reader);
                    reader.Close();
                    conexao.Close();
                }
            }
            return new JsonResult("Cliente adicionado com sucesso.");
        }
        //Fim - Cadastro de Cliente - Pessoa Jurídica

    }
}
