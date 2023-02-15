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
    public class CategoriaRepository : IRepository<Categoria>
    {
        public void Add(Categoria entity)
        {
            var query = @"
                INSERT INTO CATEGORIA(ID, NOME, IDUSUARIO)
                VALUES(@Id, @Nome, @IdUsuario)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Categoria entity)
        {
            var query = @"
                UPDATE CATEGORIA SET NOME = @Nome
                WHERE ID = @Id
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Categoria entity)
        {
            var query = @"
                DELETE FROM CATEGORIA
                WHERE ID = @Id
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Categoria> GetAll()
        {
            var query = @"
                SELECT * FROM CATEGORIA
                ORDER BY NOME
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Categoria>(query).ToList();
            }
        }

        public List<Categoria> GetByUsuario(Guid idUsuario)
        {
            var query = @"
                SELECT * FROM CATEGORIA
                WHERE IDUSUARIO = @idUsuario
                ORDER BY NOME
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Categoria>(query, new { idUsuario }).ToList();
            }
        }

        public Categoria? GetById(Guid id)
        {
            var query = @"
                SELECT * FROM CATEGORIA
                WHERE ID = @id
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Categoria>(query, new { id }).FirstOrDefault();
            }
        }

        public int CountContasByIdCategoria(Guid id)
        {
            var query = @"
                SELECT COUNT(*) FROM CONTA
                WHERE IDCATEGORIA = @id
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<int>(query, new { id }).FirstOrDefault();
            }
        }
    }
}
