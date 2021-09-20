using System.Linq;
using MagazineStore.Core.Entidades;

namespace MagazineStore.Core.Business.Abstract
{
    public interface IBusiness<T> where T : EntidadeBase
    {
        IQueryable<T> Consulta { get; }

        void Inserir(T entidade);

        void Alterar(T entidade);

        void Excluir(string id);

        T RetornarPorID(string id);

        IQueryable<T> RetornarTodos();

    }
}
