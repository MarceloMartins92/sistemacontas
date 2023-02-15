using SistemaContas.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Entities
{
    public class Conta
    {
        private Guid _id;
        private string? _nome;
        private decimal _valor;
        private DateTime _data;
        private string? _observacoes;
        private TipoConta? _tipo;
        private Guid _idUsuario;
        private Guid _idCategoria;
        private Usuario? _usuario;
        private Categoria? _categoria;

        public Guid Id { get => _id; set => _id = value; }
        public string? Nome { get => _nome; set => _nome = value; }
        public decimal Valor { get => _valor; set => _valor = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public string? Observacoes { get => _observacoes; set => _observacoes = value; }
        public TipoConta? Tipo { get => _tipo; set => _tipo = value; }
        public Guid IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public Guid IdCategoria { get => _idCategoria; set => _idCategoria = value; }
        public Usuario? Usuario { get => _usuario; set => _usuario = value; }
        public Categoria? Categoria { get => _categoria; set => _categoria = value; }
    }
}
