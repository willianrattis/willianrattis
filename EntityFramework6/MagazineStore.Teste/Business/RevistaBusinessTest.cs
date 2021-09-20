using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.Business.Concrete;
using MagazineStore.Core.Entidades;
using NUnit.Framework;

namespace MagazineStore.Teste.Business
{
    [TestFixture]
    public class RevistaBusinessTest : BaseBusinessTest
    {
        private RevistaBusiness revistaBusiness;

        public override void AntesDeCadaTeste()
        {
            base.AntesDeCadaTeste();
            revistaBusiness = new RevistaBusiness(magazineStoreContext);
        }

        [Test]
        public void PodeInserirRevistaTest()
        {
            //Ambiente
            var revista = new Revista
                              {
                                  iTotal = 10,
                                  sEditora = "Editora A",
                                  sNome = "Revista A"
                              };

            //Ação
            try
            {
                revistaBusiness.Inserir(revista);
            }
            catch (DbEntityValidationException exception)
            {
                var erros = exception.EntityValidationErrors;
                Assert.Fail(erros.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
            }

            //Assertivas
            var revistaPersistida = revistaBusiness.Consulta.FirstOrDefault(r => r.sID == revista.sID);
            Assert.IsNotNull(revistaPersistida);

        }

        [Test]
        public void PodeAlterarRevistaTest()
        {
            //Ambiente  
            var revista = new Revista
            {
                iTotal = 10,
                sEditora = "Editora A",
                sNome = "Revista A"
            };
            revistaBusiness.Inserir(revista);

            //Ação     
            var revistaParaAlteracao = revistaBusiness.Consulta.FirstOrDefault(r => r.sID == revista.sID);
            revistaParaAlteracao.sNome = "Revista B";
            revistaParaAlteracao.sEditora = "Editora B";
            revistaBusiness.Alterar(revistaParaAlteracao);

            //Assertivas
            var revistaPersistida = revistaBusiness.Consulta.FirstOrDefault(r => r.sID == revista.sID);
            Assert.IsNotNull(revistaPersistida);
            Assert.AreNotEqual("Revista A", revistaPersistida.sNome);
            Assert.AreNotEqual("Editora A", revistaPersistida.sEditora);
        }

        [Test]
        public void PodeExcluirRevistaTest()
        {
            //Ambiente  
            var revista = new Revista
            {
                iTotal = 10,
                sEditora = "Editora A",
                sNome = "Revista A"
            };
            revistaBusiness.Inserir(revista);

            //Ação     
            revistaBusiness.Excluir(revista.sID);

            //Assertivas
            var revistaPersistida = revistaBusiness.RetornarPorID(revista.sID);
            Assert.IsNull(revistaPersistida);            
        }
    }
}
