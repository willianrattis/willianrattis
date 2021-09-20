using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MagazineStore.Core.Context;
using MagazineStore.Core.Entidades;
using MagazineStore.Utils.Mensagem;

namespace MagazineStore.Core.Business.Concrete
{
    public sealed class ClienteBusiness : BaseBusiness<Cliente>
    {
        private IEnviarEmail enviarEmail;

        public ClienteBusiness(MagazineStoreContext contextParam, IEnviarEmail enviarEmailParam)
            : base(contextParam)
        {
            enviarEmail = enviarEmailParam;
        }

        public override void Inserir(Cliente entidade)
        {
            using (var scope = new TransactionScope())
            {
                base.Inserir(entidade);
                if (enviarEmail.notificarCriacaoConta(entidade.sEmail, entidade.sNomeTratamento))
                    scope.Complete();
            }
        }

        public override IQueryable<Cliente> Consulta
        {
            get
            {
                return context.Clientes.Include(c => c.Endereco);
            }
        }
    }
}
