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
    public class ContaRepository : IRepository<Conta>
    {
        public void Add(Conta entity)
        {
            var query = @"
                INSERT INTO CONTA(ID, NOME, VALOR, DATA, OBSERVACOES, TIPO, IDUSUARIO, IDCATEGORIA)
                VALUES(@Id, @Nome, @Valor, @Data, @Observacoes, @Tipo, @IdUsuario, @IdCategoria)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Conta entity)
        {
            var query = @"
                UPDATE CONTA SET 
                    NOME = @Nome, VALOR = @Valor, DATA = @Data, OBSERVACOES = @Observacoes,
                    TIPO = @Tipo, IDCATEGORIA = @IdCategoria
                WHERE ID = @Id
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Conta entity)
        {
            var query = @"
                DELETE FROM CONTA 
                WHERE ID = @Id
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Conta> GetAll()
        {
            var query = @"
                SELECT * FROM CONTA co
                INNER JOIN CATEGORIA ca
                ON ca.ID = co.IDCATEGORIA
                ORDER BY co.NOME
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query(query,
                    (Conta co, Categoria ca) => { co.Categoria = ca; return co; },
                    splitOn: "IdCategoria")
                    .ToList();
            }
        }

        public List<Conta> GetByUsuario(Guid idUsuario)
        {
            var query = @"
                SELECT * FROM CONTA co
                INNER JOIN CATEGORIA ca
                ON ca.ID = co.IDCATEGORIA
                WHERE co.IDUSUARIO = @idUsuario
                ORDER BY co.NOME
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query(query,
                    (Conta co, Categoria ca) => { co.Categoria = ca; return co; },
                    new { idUsuario },
                    splitOn: "IdCategoria")
                    .ToList();
            }
        }

        public List<Conta> GetByUsuarioAndDatas(Guid idUsuario, DateTime dataIni, DateTime dataFim)
        {
            var query = @"
                SELECT * FROM CONTA co
                INNER JOIN CATEGORIA ca
                ON ca.ID = co.IDCATEGORIA
                WHERE co.IDUSUARIO = @idUsuario AND co.DATA BETWEEN @dataIni AND @dataFim
                ORDER BY co.DATA DESC
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query(query,
                    (Conta co, Categoria ca) => { co.Categoria = ca; return co; },
                    new { idUsuario, dataIni, dataFim },
                    splitOn: "IdCategoria")
                    .ToList();
            }
        }

        public Conta? GetById(Guid id)
        {
            var query = @"
                SELECT * FROM CONTA co
                INNER JOIN CATEGORIA ca
                ON ca.ID = co.IDCATEGORIA
                WHERE co.ID = @Id
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query(query,
                   (Conta co, Categoria ca) => { co.Categoria = ca; return co; },
                   new { id },
                   splitOn: "IdCategoria")
                   .FirstOrDefault();
            }
        }
    }
}



