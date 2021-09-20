using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.Entidades;

namespace MagazineStore.Core.Business.Abstract
{
    public interface IRevistaBusiness : IBusiness<Revista>
    {
        void AtualizarEstoque(string sRevistaID, int incrementoDecremento);
    }
}
