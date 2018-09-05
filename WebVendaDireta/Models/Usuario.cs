using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVendaDireta.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo email.")]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        public decimal Receita { get; set; }
    }
}