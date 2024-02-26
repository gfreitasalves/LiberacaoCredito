using LiberacaoCredito.Application.Abstractions.Commands;
using LiberacaoCredito.Application.Abstractions.Queries;
using LiberacaoCredito.Application.InputPorts;
using LiberacaoCredito.Application.UseCases;
using LiberacaoCredito.Domain.Enums;
using LiberacaoCredito.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberacaoCredito.Testes.Application
{
    [TestClass]
    public class ValidarLiberacaoCreditoUseCaseTests
    {
        [DataTestMethod]
        [DataRow("18043554021", "10000", 10, 20, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Aprovado, "")]
        [DataRow("18043554021", "1000010", 10, 20, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Recusado, "Intervalo de valor inválido.")]
        [DataRow("18043554021", "999999", 120, 20, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Recusado, "Número de parcelas inválido.")]
        [DataRow("18043554021", "999999", 50, 1, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Recusado, "Data primeiro vencimento inválida.")]
        [DataRow("10000000100", "10000", 10, 20, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Recusado, "Cpf/Cnpj inválido.")]
        [DataRow("44672272000103", "1000", 10, 20, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Recusado, "Intervalo de valor inválido.")]
        public void QuanadoValidarDeveRetornarAprovadoReprovado(
                            string cpfCnpj,
                            string valorSolicitadoStr,
                            int quantidadeParcelas,
                            int addDaysDataPrimeiroVencimento,
                            string taxa,
                            string valorMaximoPessoaFisica,
                            string valorMaximoPessoaJuridica,
                            string valorMinimoPessoaFisica,
                            string valorMinimoPessoaJuridica,
                            int maximoParcelas,
                            int minimoParcelas,
                            int maximoDiasPrimeiroVencimento,
                            int minimoDiasPrimeiroVencimento,
                            StatusSolicitacaoLiberacaoCredito status,
                            string mensagemResultado)
        {
            //Arrange
            var linhaCredito = new LinhaCredito()
            {
                Descricao = "Linha de Testes.",
                Taxa = Convert.ToDecimal(taxa),
                ValorMaximoPessoaFisica = Convert.ToDecimal(valorMaximoPessoaFisica),
                ValorMaximoPessoaJuridica = Convert.ToDecimal(valorMaximoPessoaJuridica),
                ValorMinimoPessoaFisica = Convert.ToDecimal(valorMinimoPessoaFisica),
                ValorMinimoPessoaJuridica = Convert.ToDecimal(valorMinimoPessoaJuridica),
                MaximoParcelas = maximoParcelas,
                MinimoParcelas = minimoParcelas,
                MaximoDiasPrimeiroVencimento = maximoDiasPrimeiroVencimento,
                MinimoDiasPrimeiroVencimento = minimoDiasPrimeiroVencimento
            };

            var valorSolicitado = Convert.ToDecimal(valorSolicitadoStr);

            DateTime dataPrimeiroVencimento = DateTime.Now.AddDays(addDaysDataPrimeiroVencimento);

            var solicitacaoLiberacaoCredito = new SolicitacaoLiberacaoCredito(cpfCnpj,linhaCredito, valorSolicitado, quantidadeParcelas,dataPrimeiroVencimento);

            var solicitacaoLiberacaoCreditoRepositoryMock = new Mock<ISolicitacaoLiberacaoCreditoRepository>();
            var obterLinhaCreditoMock = new Mock<IObterLinhaCredito>();

            solicitacaoLiberacaoCreditoRepositoryMock.Setup(x => x.Inserir(It.IsAny<SolicitacaoLiberacaoCredito>()))
                .Returns(solicitacaoLiberacaoCredito);
            obterLinhaCreditoMock.Setup(x => x.SelecionarPorId(It.IsAny<Guid>())).Returns(linhaCredito);

        var validarLiberacaoCreditoUseCase = new ValidarLiberacaoCreditoUseCase(solicitacaoLiberacaoCreditoRepositoryMock.Object, obterLinhaCreditoMock.Object);

            //Act

            var input = new SolicitacaoLiberacaoCreditoInput()
            {
                CpfCnpj = cpfCnpj,
                ValorSolicitado = valorSolicitado,
                IdCredito = linhaCredito.Id,
                QuantidadeParcelas = quantidadeParcelas,
                DataPrimeiroVencimento = dataPrimeiroVencimento
            };

            var result = validarLiberacaoCreditoUseCase.Validar(input);

            //Assert            
            result.Should().NotBeNull();
            result.Status.Should().Be(status);

            if (result.Status == StatusSolicitacaoLiberacaoCredito.Recusado)
            {
                result.Mensagens.Should().NotBeNull();
                result.Mensagens.Should().Contain(mensagemResultado);
            }
        }
    }
}
