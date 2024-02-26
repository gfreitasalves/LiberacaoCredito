using LiberacaoCredito.Application.Abstractions.Commands;
using LiberacaoCredito.Application.Abstractions.Queries;
using LiberacaoCredito.Application.InputPorts;
using LiberacaoCredito.Application.UseCases.Interfaces;
using LiberacaoCredito.Domain.Models;

namespace LiberacaoCredito.Application.UseCases
{
    public class ValidarLiberacaoCreditoUseCase : IValidarLiberacaoCreditoUseCase
    {
        private readonly IObterLinhaCredito _obterCredito;
        private readonly ISolicitacaoLiberacaoCreditoRepository _solicitacaoLiberacaoCreditoRepository;

        public ValidarLiberacaoCreditoUseCase(ISolicitacaoLiberacaoCreditoRepository solicitacaoLiberacaoCreditoRepository, IObterLinhaCredito obterCredito)
        {
            _obterCredito = obterCredito;
            _solicitacaoLiberacaoCreditoRepository = solicitacaoLiberacaoCreditoRepository;
        }

        public SolicitacaoLiberacaoCreditoOutput Validar(SolicitacaoLiberacaoCreditoInput liberacaoCreditoInput)
        {
            var credito = _obterCredito.SelecionarPorId(liberacaoCreditoInput.IdCredito);

            var solicitacaoLiberacaoCredito = new SolicitacaoLiberacaoCredito(liberacaoCreditoInput.CpfCnpj,credito, liberacaoCreditoInput.ValorSolicitado, liberacaoCreditoInput.QuantidadeParcelas, liberacaoCreditoInput.DataPrimeiroVencimento);
            
            var solicitacaoLiberacaoCreditoNew = _solicitacaoLiberacaoCreditoRepository.Inserir(solicitacaoLiberacaoCredito);

            return new SolicitacaoLiberacaoCreditoOutput(solicitacaoLiberacaoCreditoNew.Status, solicitacaoLiberacaoCreditoNew.ValorFinal, solicitacaoLiberacaoCreditoNew.ValorJuros, solicitacaoLiberacaoCredito.Mensagens);

        }               
    }
}
