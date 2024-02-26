using LiberacaoCredito.Application.InputPorts;

namespace LiberacaoCredito.Application.UseCases.Interfaces
{
    public interface IValidarLiberacaoCreditoUseCase
    {
        Task<SolicitacaoLiberacaoCreditoOutput> ValidarAsync(SolicitacaoLiberacaoCreditoInput liberacaoCreditoInput);
    }
}
