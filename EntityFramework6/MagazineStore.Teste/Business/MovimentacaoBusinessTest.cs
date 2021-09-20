using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.Business.Abstract;
using MagazineStore.Core.Business.Concrete;
using MagazineStore.Core.Entidades;
using MagazineStore.Core.Entidades.Enum;
using Moq;
using NUnit.Framework;

namespace MagazineStore.Teste.Business
{
    [TestFixture]
    public class MovimentacaoBusinessTest : BaseBusinessTest
    {
        private IMovimentacaoBusiness movimentacaoBusiness;
        private IRevistaBusiness revistaBusiness;
        private IBusiness<Cliente> clienteBusiness;

        public override void AntesDeCadaTeste()
        {
            base.AntesDeCadaTeste();

            revistaBusiness = new RevistaBusiness(magazineStoreContext);
            clienteBusiness = new ClienteBusiness(magazineStoreContext, enviarEmail);
            movimentacaoBusiness = new MovimentacaoBusiness(magazineStoreContext);
            ((MovimentacaoBusiness)movimentacaoBusiness).RevistaBuiness = revistaBusiness;
        }

        [Test]
        public void PodeRealizarVendaTest()
        {
            //Ambiente

            #region Inserindo revista
            var revista = new Revista
                              {
                                  iTotal = 10,
                                  sEditora = "Editora A",
                                  sNome = "Revista A"
                              };
            revistaBusiness.Inserir(revista);
            #endregion

            var cliente = new Cliente
                              {
                                  Sexo = Sexo.Masculino,
                                  sNome = "Mateus",
                                  sEmail = "teste@teste.com.br",
                                  Endereco = new Endereco
                                                 {
                                                     sBairro = "Centro"
                                                 }
                              };
            clienteBusiness.Inserir(cliente);
            var formaPagamento = FormaPagamento.Cheque;

            //Ação
            movimentacaoBusiness.Vender(cliente, new List<Revista> { revista }, formaPagamento);

            //Assertivas
            var movimentacaoPersistida = movimentacaoBusiness.Consulta.FirstOrDefault(m => m.sClienteID == cliente.sID);
            var estoqueRevista = revistaBusiness.Consulta.Select(r => r.iTotal).FirstOrDefault();
            Assert.IsNotNull(movimentacaoPersistida);
            Assert.AreEqual(9, estoqueRevista);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Não há estoque para finalizar essa venda")]
        public void NaoPodeRealizarVendaSemRevistaNoEstoqueTest()
        {
            //Ambiente

            #region Inserindo revista
            var revista = new Revista
            {
                iTotal = 1,
                sEditora = "Editora A",
                sNome = "Revista A"
            };
            revistaBusiness.Inserir(revista);
            #endregion

            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Mateus",
                sEmail = "teste@teste.com.br",
                Endereco = new Endereco
                {
                    sBairro = "Centro"
                }
            };
            clienteBusiness.Inserir(cliente);
            var formaPagamento = FormaPagamento.Cheque;
            movimentacaoBusiness.Vender(cliente, new List<Revista> { revista }, formaPagamento);

            //Ação
            try
            {
                movimentacaoBusiness.Vender(cliente, new List<Revista> { revista }, formaPagamento);
            }
            catch (Exception)
            {
                //Assertivas
                var movimentacaoPersistida = movimentacaoBusiness.Consulta.Where(m => m.sClienteID == cliente.sID);
                var estoqueRevista = revistaBusiness.Consulta.Select(r => r.iTotal).FirstOrDefault();
                Assert.AreEqual(1, movimentacaoPersistida.Count());
                Assert.AreEqual(0, estoqueRevista);
                throw;
            }
        }

        [Test]
        public void NaoPodeRealizarVendaSeExceptionNaAtualizacaoEstoqueTest()
        {
            //Ambiente
            var mockRevistaBusiness = new Mock<IRevistaBusiness>();
            mockRevistaBusiness.Setup(r => r.AtualizarEstoque(It.IsAny<string>(), It.IsAny<int>())).Throws<InvalidOperationException>();
            var revistaBusinessMock = mockRevistaBusiness.Object;
            ((MovimentacaoBusiness)movimentacaoBusiness).RevistaBuiness = revistaBusinessMock;


            #region Inserindo revista
            var revista = new Revista
            {
                iTotal = 1,
                sEditora = "Editora A",
                sNome = "Revista A"
            };
            revistaBusiness.Inserir(revista);
            #endregion

            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Mateus",
                sEmail = "teste@teste.com.br",
                Endereco = new Endereco
                {
                    sBairro = "Centro"
                }
            };
            clienteBusiness.Inserir(cliente);
            var formaPagamento = FormaPagamento.Cheque;

            //Ação
            try
            {
                movimentacaoBusiness.Vender(cliente, new List<Revista> { revista }, formaPagamento);
            }
            catch (Exception)
            {
                //Assertivas
                var movimentacaoPersistida = movimentacaoBusiness.Consulta.Where(m => m.sClienteID == cliente.sID);
                var estoqueRevista = revistaBusiness.Consulta.Select(r => r.iTotal).FirstOrDefault();
                Assert.AreEqual(0, movimentacaoPersistida.Count());
                Assert.AreEqual(1, estoqueRevista);
            }
        }

        [Test]
        public void PodeRetornarRevistasCompradasPorClienteTest()
        {
            //Ambiente

            #region Inserindo revista
            var revista = new Revista
            {
                iTotal = 3,
                sEditora = "Editora A",
                sNome = "Revista A"
            };
            revistaBusiness.Inserir(revista);
            #endregion

            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Mateus",
                sEmail = "teste@teste.com.br",
                Endereco = new Endereco
                {
                    sBairro = "Centro"
                }
            };
            clienteBusiness.Inserir(cliente);
            var formaPagamento = FormaPagamento.Cheque;
            movimentacaoBusiness.Vender(cliente, new List<Revista> { revista }, formaPagamento);
            movimentacaoBusiness.Vender(cliente, new List<Revista> { revista }, formaPagamento);
            movimentacaoBusiness.Vender(cliente, new List<Revista> { revista }, formaPagamento);

            //Ação            
            var revistasCompradas = movimentacaoBusiness.RetornarRevistasCompradasPorCliente(cliente.sID);

            //Assertivas
            Assert.AreEqual(3, revistasCompradas.Count());
        }

        [Test]
        public void PodePaginarRevistasCompradasPorClienteTest()
        {
            //Ambiente

            #region Inserindo revista
            var revista = new Revista
            {
                iTotal = 3,
                sEditora = "Editora A",
                sNome = "Revista A"
            };
            revistaBusiness.Inserir(revista);
            #endregion

            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Mateus",
                sEmail = "teste@teste.com.br",
                Endereco = new Endereco
                {
                    sBairro = "Centro"
                }
            };
            clienteBusiness.Inserir(cliente);
            var formaPagamento = FormaPagamento.Cheque;
            movimentacaoBusiness.Vender(cliente, new List<Revista> { revista }, formaPagamento);
            movimentacaoBusiness.Vender(cliente, new List<Revista> { revista }, formaPagamento);
            movimentacaoBusiness.Vender(cliente, new List<Revista> { revista }, formaPagamento);

            //Ação            
            var revistasCompradas = movimentacaoBusiness.RetornarRevistasCompradasPorCliente(cliente.sID);

            //Assertivas
            //Pagina 1
            var totalItensPorPagina = 2;
            Assert.AreEqual(2, revistasCompradas.OrderBy(r=>r.sNome).Skip(0).Take(totalItensPorPagina).Count());

            //Pagina 2            
            Assert.AreEqual(1, revistasCompradas.OrderBy(r => r.sNome).Skip(2).Take(totalItensPorPagina).Count());

            //Pagina 3            
            Assert.AreEqual(0, revistasCompradas.OrderBy(r => r.sNome).Skip(4).Take(totalItensPorPagina).Count());
            
        }

    }
}
