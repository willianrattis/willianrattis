using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.Business.Concrete;
using MagazineStore.Core.Entidades;
using MagazineStore.Core.Entidades.Enum;
using MagazineStore.Utils.Mensagem;
using Moq;
using NUnit.Framework;

namespace MagazineStore.Teste.Business
{
    [TestFixture]
    public class ClienteBusinessTest : BaseBusinessTest
    {
        private ClienteBusiness clienteBusiness;

        public override void AntesDeCadaTeste()
        {
            base.AntesDeCadaTeste();
            clienteBusiness = new ClienteBusiness(magazineStoreContext, enviarEmail);
        }

        [Test]
        public void PodeInserirClienteTest()
        {
            //Ambiente             
            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Pedro",
                sEmail = "teste@teste.com.br",
                Endereco = new Endereco
                {
                    sBairro = "Centro"
                }
            };            

            //Ação            
            clienteBusiness.Inserir(cliente);

            //Assertivas
            var clientePersistido = clienteBusiness.Consulta.FirstOrDefault(c => c.sID == cliente.sID);
            Assert.IsNotNull(clientePersistido);
            Assert.IsNotNull(clientePersistido.Endereco);
            Assert.AreEqual(cliente.Endereco.sBairro, clientePersistido.Endereco.sBairro);
        }

        [Test]
        public void NaoPodeInserirClienteQuandoErroNoEnvioDeEmailTest()
        {
            //Ambiente 
            var mockEnviarEmail = new Mock<IEnviarEmail>();
            mockEnviarEmail.Setup(e => e.notificarCriacaoConta(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            enviarEmail = mockEnviarEmail.Object;

            clienteBusiness = new ClienteBusiness(magazineStoreContext, enviarEmail);

            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Pedro",
                sEmail = "teste@teste.com.br",
                Endereco = new Endereco
                {
                    sBairro = "Centro"
                }
            };

            //Ação            
            clienteBusiness.Inserir(cliente);

            //Assertivas
            var clientePersistido = clienteBusiness.Consulta.FirstOrDefault(c => c.sID == cliente.sID);
            Assert.IsNull(clientePersistido);
        }

        [Test]
        public void PodeAlterarClienteTest()
        {
            //Ambiente  
            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Pedro",
                Endereco = new Endereco
                {
                    sBairro = "Centro"
                }
            };
            clienteBusiness.Inserir(cliente);            

            //Ação     
            var clienteParaAlteracao = clienteBusiness.Consulta.FirstOrDefault(c => c.sID == cliente.sID);
            clienteParaAlteracao.sNome = "Maria";
            clienteParaAlteracao.Sexo = Sexo.Feminino;
            clienteParaAlteracao.Endereco.sCidade = "Fortaleza";
            clienteBusiness.Alterar(clienteParaAlteracao);


            //Assertivas
            var clientePersistido = clienteBusiness.Consulta.FirstOrDefault(c => c.sID == cliente.sID);
            Assert.IsNotNull(clientePersistido);
            Assert.IsNotNull(clientePersistido.Endereco);
            Assert.AreNotEqual("Pedro", clientePersistido.sNome);
            Assert.AreNotEqual(Sexo.Masculino, clientePersistido.Sexo);
            Assert.AreEqual("Fortaleza", clientePersistido.Endereco.sCidade);

        }

        [Test]
        public void PodeExcluirClienteTest()
        {
            //Ambiente            
            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Pedro",
                Endereco = new Endereco
                {
                    sBairro = "Centro"
                }
            };
            clienteBusiness.Inserir(cliente);


            //Ação              
            clienteBusiness.Excluir(cliente.sID);                        

            //Assertivas
            var clientePersistido = clienteBusiness.Consulta.FirstOrDefault(c => c.sID == cliente.sID);
            Assert.IsNull(clientePersistido);
        }
    }
}
