using LiberacaoCredito.Domain.Models;

namespace LiberacaoCredito.Application.Abstractions.Queries
{
    public interface IObterLinhaCredito
    {
        IEnumerable<LinhaCredito> SelecionarTodos();
        LinhaCredito SelecionarPorId(Guid id);
    }
}
