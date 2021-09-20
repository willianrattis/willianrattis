using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.Business.Abstract;
using MagazineStore.Core.Context;
using MagazineStore.Core.Entidades;

namespace MagazineStore.Core.Business.Concrete
{
    public class RevistaBusiness : BaseBusiness<Revista>, IRevistaBusiness
    {
        public RevistaBusiness(MagazineStoreContext contextParam)
            : base(contextParam)
        {
        }

        public void AtualizarEstoque(string sRevistaID, int incrementoDecremento)
        {
            var revista = RetornarPorID(sRevistaID);
            revista.iTotal += incrementoDecremento;
            if (revista.iTotal < 0)
                throw new InvalidOperationException("Não há estoque para finalizar essa venda");
            Alterar(revista);
        }
    }
}
