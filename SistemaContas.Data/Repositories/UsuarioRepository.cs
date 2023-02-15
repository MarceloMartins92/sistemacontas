using Dapper;
using SistemaContas.Data.Configurations;
using SistemaContas.Data.Entities;
using SistemaContas.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Repositories
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        public void Add(Usuario entity)
        {
            var query = @"
                INSERT INTO USUARIO(ID, NOME, EMAIL, SENHA)
                VALUES(@Id, @Nome, @Email, @Senha)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Usuario entity)
        {
            var query = @"
                UPDATE USUARIO SET
                    NOME = @Nome, EMAIL = @Email, SENHA = @Senha
                WHERE ID = @Id
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Guid idUsuario, string novaSenha)
        {
            var query = @"
                UPDATE USUARIO SET
                    SENHA = @novaSenha
                WHERE ID = @idUsuario
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, new { idUsuario, novaSenha });
            }
        }

        public void Delete(Usuario entity)
        {
            var query = @"
                DELETE FROM USUARIO
                WHERE ID = @Id
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Usuario> GetAll()
        {
            var query = @"
                SELECT * FROM USUARIO
                ORDER BY NOME   
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Usuario>(query).ToList();
            }
        }

        public Usuario? GetByEmail(string email)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Usuario>(query, new { email }).FirstOrDefault();
            }
        }

        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email AND SENHA = @senha
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Usuario>(query, new { email, senha }).FirstOrDefault();
            }
        }

        public Usuario? GetById(Guid id)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE ID = @id
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Usuario>(query, new { id }).FirstOrDefault();
            }
        }
    }
}
