using LiberacaoCredito.Application.Abstractions.Queries;
using LiberacaoCredito.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LiberacaoCredito.Infrastructure.Database
{
    public class ObterLinhaCreditoQuery : IObterLinhaCreditoQuery
    {
        private readonly LiberacaoCreditoDbContext _liberacaoCreditoDbContext;

        public ObterLinhaCreditoQuery(LiberacaoCreditoDbContext liberacaoCreditoDbContext)
        {
            _liberacaoCreditoDbContext= liberacaoCreditoDbContext;
        }
        
        public async Task<LinhaCredito?> SelecionarPorIdAsync(Guid id)
        {
            return await _liberacaoCreditoDbContext.LinhaCreditos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<LinhaCredito>> SelecionarTodosAsync()
        {
            return await _liberacaoCreditoDbContext.LinhaCreditos.ToListAsync();
        }
    }
}
