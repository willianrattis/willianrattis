using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.Entidades;
using MagazineStore.Core.Entidades.Enum;
using NUnit.Framework;

namespace MagazineStore.Teste.Entidade
{
    [TestFixture]
    public class ClienteEntidadeTest : BaseEntidadeTest
    {
        [Test]
        public void PodeInserirClienteTest()
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

            //Ação
            var resultadoValidacao = ValidarEntidade(cliente);


            //Assertivas
            Assert.IsTrue(resultadoValidacao.Key,
                resultadoValidacao.Value.Any() ?
                resultadoValidacao.Value.First().ErrorMessage : string.Empty);
        }

        [Test]
        public void NaoPodeInserirSemInformarEnderecoTest()
        {
            //Ambiente
            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Pedro"
            };

            //Ação
            var resultadoValidacao = ValidarEntidade(cliente);


            //Assertivas
            Assert.IsFalse(resultadoValidacao.Key);
            Assert.AreEqual(1, resultadoValidacao.Value.Count);
            Assert.IsTrue(resultadoValidacao.Value.Any(r => r.MemberNames.Contains("Endereco")));
            Assert.IsTrue(resultadoValidacao.Value.Where(r => r.MemberNames.Contains("Endereco")).Any(r => r.ErrorMessage == "Endereço é obrigatório"));
        }

        [Test]
        public void NaoPodeInserirCPFInvalidoTest()
        {
            //Ambiente
            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Pedro",
                sCPF = "72377641872",
                Endereco = new Endereco
                {
                    sBairro = "Centro"
                }
            };

            //Ação
            var resultadoValidacao = ValidarEntidade(cliente);


            //Assertivas
            Assert.IsFalse(resultadoValidacao.Key);
            Assert.AreEqual(1, resultadoValidacao.Value.Count);
            Assert.IsTrue(resultadoValidacao.Value.Any(r => r.MemberNames.Contains("sCPF")));
            Assert.IsTrue(resultadoValidacao.Value.Where(r => r.MemberNames.Contains("sCPF")).Any(r => r.ErrorMessage == "Informe um CPF válido"));
        }

        [Test]
        public void NaoPodeInserirClienteComNomeIgualAoNomeDeTratamentoTest()
        {
            //Ambiente
            var cliente = new Cliente
            {
                Sexo = Sexo.Masculino,
                sNome = "Pedro",
                sNomeTratamento = "Pedro",
                Endereco = new Endereco
                {
                    sBairro = "Centro"
                }
            };

            //Ação
            var resultadoValidacao = ValidarEntidade(cliente);


            //Assertivas            
            Assert.IsFalse(resultadoValidacao.Key);
            Assert.AreEqual(1, resultadoValidacao.Value.Count);
            Assert.IsTrue(resultadoValidacao.Value.Any(r => r.MemberNames.Contains("sNome")));
            Assert.IsTrue(resultadoValidacao.Value.Any(r => r.MemberNames.Contains("sNomeTratamento")));
            Assert.IsTrue(resultadoValidacao.Value.Where(r => r.MemberNames.Contains("sNome")).Any(r => r.ErrorMessage == "Nome não pode ser igual ao Nome de tratamento"));
        }
    }
}
