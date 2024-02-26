
using LiberacaoCredito.Domain.Enums;
using LiberacaoCredito.Domain.Models;

namespace LiberacaoCredito.Testes
{
    [TestClass]
    public class SolicitacaoLiberacaoCreditoTests
    {
        [DataTestMethod]
        [DataRow("18043554021", "10000", 10, 20, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Aprovado, "")]
        [DataRow("18043554021", "1000010", 10, 20, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Recusado, "Intervalo de valor inválido.")]
        [DataRow("18043554021", "999999", 120, 20, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Recusado, "Número de parcelas inválido.")]
        [DataRow("18043554021", "999999", 50, 1, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Recusado, "Data primeiro vencimento inválida.")]
        [DataRow("10000000100", "10000", 10, 20, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Recusado, "Cpf/Cnpj inválido.")]
        [DataRow("44672272000103", "1000", 10, 20, "2", "1000000", "1000000", "1", "15000", 72, 5, 40, 15, StatusSolicitacaoLiberacaoCredito.Recusado, "Intervalo de valor inválido.")]
        public void QuandoEstanciarDeveAprovarOuReprovar(
                            string cpfCnpj,
                            string valorSolicitado,
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
                            string mensagemResultado
                            )
        {
            //Arrange
            
            var credito = new LinhaCredito()
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


            //Act
            var solicitacaoCredito = new SolicitacaoLiberacaoCredito(cpfCnpj, credito, Convert.ToDecimal(valorSolicitado), quantidadeParcelas, DateTime.Now.AddDays(addDaysDataPrimeiroVencimento));


            //Assert
            solicitacaoCredito.Should().NotBeNull();
            solicitacaoCredito.Status.Should().Be(status);

            if (solicitacaoCredito.Status == StatusSolicitacaoLiberacaoCredito.Recusado)
            {
                solicitacaoCredito.Mensagens.Should().NotBeNull();
                solicitacaoCredito.Mensagens.Should().Contain(mensagemResultado);
            }

        }
    }
}