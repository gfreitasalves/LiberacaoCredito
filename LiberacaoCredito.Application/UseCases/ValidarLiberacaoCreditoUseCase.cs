using LiberacaoCredito.Application.Abstractions.Commands;
using LiberacaoCredito.Application.Abstractions.Queries;
using LiberacaoCredito.Application.InputPorts;
using LiberacaoCredito.Application.UseCases.Interfaces;
using LiberacaoCredito.Domain.Enums;
using LiberacaoCredito.Domain.Models;

namespace LiberacaoCredito.Application.UseCases
{
    public class ValidarLiberacaoCreditoUseCase : IValidarLiberacaoCreditoUseCase
    {
        private readonly IObterLinhaCreditoQuery _obterCredito;
        private readonly ISolicitacaoLiberacaoCreditoRepository _solicitacaoLiberacaoCreditoRepository;

        public ValidarLiberacaoCreditoUseCase(ISolicitacaoLiberacaoCreditoRepository solicitacaoLiberacaoCreditoRepository, IObterLinhaCreditoQuery obterCredito)
        {
            _obterCredito = obterCredito;
            _solicitacaoLiberacaoCreditoRepository = solicitacaoLiberacaoCreditoRepository;
        }

        public async Task<SolicitacaoLiberacaoCreditoOutput> ValidarAsync(SolicitacaoLiberacaoCreditoInput liberacaoCreditoInput)
        {
            var credito = await _obterCredito.SelecionarPorIdAsync(liberacaoCreditoInput.IdCredito);

            if (credito ==null)
            {
                return BuildOutput(StatusSolicitacaoLiberacaoCredito.Recusado, 0, 0, new List<string> { "Linha de crédito inválida" });
            }

            var solicitacaoLiberacaoCredito = new SolicitacaoLiberacaoCredito(liberacaoCreditoInput.CpfCnpj,credito, liberacaoCreditoInput.ValorSolicitado, liberacaoCreditoInput.QuantidadeParcelas, liberacaoCreditoInput.DataPrimeiroVencimento);
            
            var solicitacaoLiberacaoCreditoNew = await _solicitacaoLiberacaoCreditoRepository.InserirAsync(solicitacaoLiberacaoCredito);

            return BuildOutput(solicitacaoLiberacaoCreditoNew.Status, solicitacaoLiberacaoCreditoNew.ValorFinal, solicitacaoLiberacaoCreditoNew.ValorJuros, solicitacaoLiberacaoCreditoNew.Mensagens);
        }               

        private SolicitacaoLiberacaoCreditoOutput BuildOutput(StatusSolicitacaoLiberacaoCredito status, decimal valorFinal, decimal valorJuros, ICollection<string> mensagens) 
        {
            return new SolicitacaoLiberacaoCreditoOutput(status, valorFinal, valorJuros, mensagens);

        }
    }
}
