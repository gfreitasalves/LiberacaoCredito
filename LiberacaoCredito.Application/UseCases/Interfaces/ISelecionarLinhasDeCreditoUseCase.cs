using LiberacaoCredito.Domain.Models;

namespace LiberacaoCredito.Application.UseCases.Interfaces
{
    public interface ISelecionarLinhasDeCreditoUseCase
    {
        Task<IEnumerable<LinhaCredito>> SelecionarAsync();
    }
}
