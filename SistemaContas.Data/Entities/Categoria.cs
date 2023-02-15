using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Entities
{
    public class Categoria
    {
        private Guid _id;
        private string? _nome;
        private Guid _idUsuario;
        private Usuario? _usuario;
        private List<Conta>? _contas;

        public Guid Id { get => _id; set => _id = value; }
        public string? Nome { get => _nome; set => _nome = value; }
        public Guid IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public Usuario? Usuario { get => _usuario; set => _usuario = value; }
        public List<Conta>? Contas { get => _contas; set => _contas = value; }
    }
}
