using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Entities
{
    public class Usuario
    {
        private Guid _id;
        private string? _nome;
        private string? _email;
        private string? _senha;
        private List<Conta>? _contas;
        private List<Categoria>? _categorias;

        public Guid Id { get => _id; set => _id = value; }
        public string? Nome { get => _nome; set => _nome = value; }
        public string? Email { get => _email; set => _email = value; }
        public string? Senha { get => _senha; set => _senha = value; }
        public List<Conta>? Contas { get => _contas; set => _contas = value; }
        public List<Categoria>? Categorias { get => _categorias; set => _categorias = value; }
    }
}
