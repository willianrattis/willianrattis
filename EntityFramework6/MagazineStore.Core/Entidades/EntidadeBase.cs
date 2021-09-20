using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineStore.Core.Entidades
{
    public abstract class EntidadeBase
    {       
        [Key]
        [StringLength(40)]
        public string sID { get; set; }
    }
}
