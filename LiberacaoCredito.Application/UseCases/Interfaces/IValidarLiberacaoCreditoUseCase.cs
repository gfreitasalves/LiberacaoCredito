using LiberacaoCredito.Application.InputPorts;

namespace LiberacaoCredito.Application.UseCases.Interfaces
{
    public interface IValidarLiberacaoCreditoUseCase
    {
        SolicitacaoLiberacaoCreditoOutput Validar(SolicitacaoLiberacaoCreditoInput liberacaoCreditoInput);
    }
}
