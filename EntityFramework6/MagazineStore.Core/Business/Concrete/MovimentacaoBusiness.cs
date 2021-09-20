using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MagazineStore.Core.Business.Abstract;
using MagazineStore.Core.Context;
using MagazineStore.Core.DTO;
using MagazineStore.Core.Entidades;

namespace MagazineStore.Core.Business.Concrete
{
    public sealed class MovimentacaoBusiness : BaseBusiness<Movimentacao>, IMovimentacaoBusiness
    {
        public IRevistaBusiness RevistaBuiness { get; set; }

        public MovimentacaoBusiness(MagazineStoreContext contextParam)
            : base(contextParam)
        {
        }

        public void Vender(Cliente cliente, List<Revista> revistas, Entidades.Enum.FormaPagamento formaPagamento)
        {
            var movimentacao = new Movimentacao
                                   {
                                       sClienteID = cliente.sID,
                                       dtVenda = DateTime.Now,
                                       FormaPagamento = formaPagamento,
                                       Revistas = revistas
                                   };

            using (var scope = new TransactionScope())
            {
                base.Inserir(movimentacao);

                revistas.ForEach(r =>
                {
                    RevistaBuiness.AtualizarEstoque(r.sID, -1);
                });
                scope.Complete();
            }
        }


        public IQueryable<RevistaDTO> RetornarRevistasCompradasPorCliente(string sClienteID)
        {
            var query = from m in Consulta
                        from r in m.Revistas
                        where m.sClienteID == sClienteID
                        select new RevistaDTO
                                   {
                                       sEditora = r.sEditora,
                                       sNome = r.sNome
                                   };

            return query;
        }
    }
}
