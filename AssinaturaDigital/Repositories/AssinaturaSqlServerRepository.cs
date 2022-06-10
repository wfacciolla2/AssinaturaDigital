using AssinaturaDigital.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AssinaturaDigital.Repositories
{
    public class AssinaturaSqlServerRepository : IAssinaturaRepository
    {
        private readonly SqlConnection sqlConnection;

        public AssinaturaSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Assinatura>> Obter(int pagina, int quantidade)
        {

            var assinaturas = new List<Assinatura>();

            var comando = $"select * from Assinaturas order by id offset{((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                assinaturas.Add(new Assinatura
                {
                    Id = (Guid)sqlDataReader["Id"],              
                    Url = (string)sqlDataReader["Url"],
                    Nome = (string)sqlDataReader["Nome"],
                    Cargo = (string)sqlDataReader["Cargo"],
                    Empresa = (string)sqlDataReader["Empresa"],
                    Telefone = (string)sqlDataReader["Telefone"],
                    Email = (string)sqlDataReader["Email"],
                    Site = (string)sqlDataReader["Site"],
                    Endereco = (string)sqlDataReader["Endereco"],
                    Usuario = (string)sqlDataReader["Usuario"],
                    Senha = (string)sqlDataReader["Senha"],
                });

                await sqlConnection.CloseAsync();

                
            }
            return assinaturas;
        }

        public async Task<Assinatura> Obter(Guid id)
        {
            Assinatura assinatura = null;

            var comando = $"select * from Assinaturas where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                assinatura = new Assinatura
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Url = (string)sqlDataReader["Url"],
                    Nome = (string)sqlDataReader["Nome"],
                    Cargo = (string)sqlDataReader["Cargo"],
                    Empresa = (string)sqlDataReader["Empresa"],
                    Telefone = (string)sqlDataReader["Telefone"],
                    Email = (string)sqlDataReader["Email"],
                    Site = (string)sqlDataReader["Site"],
                    Endereco = (string)sqlDataReader["Endereco"],
                    Usuario = (string)sqlDataReader["Usuario"],
                    Senha = (string)sqlDataReader["Senha"],
                };
            }

            await sqlConnection.CloseAsync();

            return assinatura;
        }

        public async Task<List<Assinatura>> Obter(string usuario, string email)
        {
            var assinaturas = new List<Assinatura>();

            var comando = $"select * from Assinaturas where Usuario = '{usuario}' and Email = '{email}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                assinaturas.Add(new Assinatura
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Url = (string)sqlDataReader["Url"],
                    Nome = (string)sqlDataReader["Nome"],
                    Cargo = (string)sqlDataReader["Cargo"],
                    Empresa = (string)sqlDataReader["Empresa"],
                    Telefone = (string)sqlDataReader["Telefone"],
                    Email = (string)sqlDataReader["Email"],
                    Site = (string)sqlDataReader["Site"],
                    Endereco = (string)sqlDataReader["Endereco"],
                    Usuario = (string)sqlDataReader["Usuario"],
                    Senha = (string)sqlDataReader["Senha"],
                });
            }

            await sqlConnection.CloseAsync();

            return assinaturas;
        }

        public async Task Inserir(Assinatura assinatura)
        {
            var comando = $"insert Assinaturas (Id, Url, Nome, Cargo, Empresa, Telefone, Email, Site, Endereco, Usuario, Senha) values (" +
                $"'{assinatura.Id}','{assinatura.Url}','{assinatura.Nome}','{assinatura.Cargo}','{assinatura.Empresa}','{assinatura.Telefone}','{assinatura.Email}','{assinatura.Site}','{assinatura.Endereco}','{assinatura.Usuario}','{assinatura.Senha}')";

            await sqlConnection.CloseAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Assinatura assinatura)
        {
            var comando = $"update Assinaturas set Url = '{assinatura.Url}','{assinatura.Nome}','{assinatura.Cargo}','{assinatura.Empresa}','{assinatura.Telefone}','{assinatura.Email}','{assinatura.Site}','{assinatura.Endereco}','{assinatura.Usuario}','{assinatura.Senha}'";

            await sqlConnection.CloseAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Assinaturas where Id = '{id}'";

            await sqlConnection.CloseAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
