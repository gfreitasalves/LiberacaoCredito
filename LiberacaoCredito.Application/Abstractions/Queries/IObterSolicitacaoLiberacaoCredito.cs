using LiberacaoCredito.Domain.Models;

namespace LiberacaoCredito.Application.Abstractions.Queries
{
    public interface IObterSolicitacaoLiberacaoCredito
    {
        IEnumerable<SolicitacaoLiberacaoCredito> SelecionarTodos();
    }
}
