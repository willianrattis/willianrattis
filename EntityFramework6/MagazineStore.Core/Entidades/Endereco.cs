using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineStore.Core.Entidades
{
    [Table("tbCliente")]
    public class Endereco : EntidadeBase
    {
        [StringLength(100, ErrorMessage = "Tamanho máximo excedido")]
        public string sLogradouro { get; set; }

        [StringLength(100, ErrorMessage = "Tamanho máximo excedido")]
        public string sBairro { get; set; }

        [StringLength(100, ErrorMessage = "Tamanho máximo excedido")]
        public string sCidade { get; set; }

        [StringLength(2, ErrorMessage = "Tamanho máximo excedido")]
        public string sUF { get; set; }

        [StringLength(50, ErrorMessage = "Tamanho máximo excedido")]
        public string sComplemento { get; set; }
        [StringLength(10, ErrorMessage = "Tamanho máximo excedido")]
        public string sNumero { get; set; }

        public Cliente Cliente { get; set; }
    }
}
