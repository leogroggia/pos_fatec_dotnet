using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVendaDireta.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public bool Vendido { get; set; } = false;

        [Required]
        public int? UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}