using LiberacaoCredito.Domain.Models;

namespace LiberacaoCredito.Application.Abstractions.Queries
{
    public interface IObterLinhaCreditoQuery
    {
        Task<IEnumerable<LinhaCredito>> SelecionarTodosAsync();
        Task<LinhaCredito?> SelecionarPorIdAsync(Guid id);
    }
}
