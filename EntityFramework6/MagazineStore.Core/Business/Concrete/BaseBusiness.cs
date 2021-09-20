using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.Business.Abstract;
using MagazineStore.Core.Context;
using MagazineStore.Core.Entidades;

namespace MagazineStore.Core.Business.Concrete
{
    public class BaseBusiness<T> : IBusiness<T> where T : EntidadeBase
    {
        protected MagazineStoreContext context;

        public BaseBusiness(MagazineStoreContext contextParam)
        {
            context = contextParam;
        }


        public virtual IQueryable<T> Consulta
        {
            get { return from o in context.Set<T>() select o; }
        }

        public virtual void Inserir(T entidade)
        {
            context.Entry(entidade).State = EntityState.Added;
            context.SaveChanges();
        }

        public virtual void Alterar(T entidade)
        {
            context.Entry(entidade).State = EntityState.Modified;
            context.SaveChanges();
        }

        public virtual void Excluir(string id)
        {
            var entidade = Consulta.FirstOrDefault(o => o.sID == id);
            context.Set<T>().Remove(entidade);
            context.SaveChanges();
        }

        public virtual T RetornarPorID(string id)
        {
            return Consulta.FirstOrDefault(c => c.sID == id);
        }

        public virtual IQueryable<T> RetornarTodos()
        {
            return Consulta;
        }
    }
}
