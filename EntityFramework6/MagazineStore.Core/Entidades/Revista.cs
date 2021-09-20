using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineStore.Core.Entidades
{
    [Table("tbRevista")]
    public class Revista : EntidadeBase
    {
        public Revista()
        {
            sID = Guid.NewGuid().ToString();
        }

        [StringLength(100)]
        public string sNome { get; set; }

        [StringLength(100)]
        public string sEditora { get; set; }

        public int iTotal { get; set; }

        public ICollection<Movimentacao> Movimentacoes { get; set; }
    }
}
