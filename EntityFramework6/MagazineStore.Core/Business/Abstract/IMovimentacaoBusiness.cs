using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.DTO;
using MagazineStore.Core.Entidades;
using MagazineStore.Core.Entidades.Enum;

namespace MagazineStore.Core.Business.Abstract
{
    public interface IMovimentacaoBusiness : IBusiness<Movimentacao>
    {
        void Vender(Cliente cliente, List<Revista> revistas, FormaPagamento formaPagamento);

        IQueryable<RevistaDTO> RetornarRevistasCompradasPorCliente(string sClienteID);
    }
}
