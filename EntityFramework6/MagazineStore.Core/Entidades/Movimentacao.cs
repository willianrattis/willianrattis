using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using MagazineStore.Core.Entidades.Enum;

namespace MagazineStore.Core.Entidades
{
    [Table("tbMovimentacao")]
    public class Movimentacao : EntidadeBase
    {
        public Movimentacao()
        {
            sID = Guid.NewGuid().ToString();
        }

        [ForeignKey("sClienteID")]
        public Cliente Cliente { get; set; }

        public string sClienteID { get; set; }

        public DateTime dtVenda { get; set; }

        public ICollection<Revista> Revistas { get; set; }

        public FormaPagamento FormaPagamento { get; set; }        
    }
}
