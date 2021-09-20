using MagazineStore.Utils.Mensagem;
using Moq;
using NUnit.Framework;

namespace MagazineStore.Teste.Business
{
    public abstract class BaseBusinessTest
    {
        protected MagazineStore.Core.Context.MagazineStoreContext magazineStoreContext;

        protected IEnviarEmail enviarEmail;


        [TestFixtureSetUp]
        public virtual void AntesDeTodosOsTestes()
        {            
        }

        [SetUp]
        public virtual void AntesDeCadaTeste()
        {
            magazineStoreContext = new MagazineStore.Core.Context.MagazineStoreContext();
            if (magazineStoreContext.Database.Exists())
            {
                magazineStoreContext.Database.Delete();
            }
            magazineStoreContext.Database.Create();

            var mockEnviarEmail = new Mock<IEnviarEmail>();
            mockEnviarEmail.Setup(e => e.notificarCriacaoConta(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            enviarEmail = mockEnviarEmail.Object;
        }
    }
}
