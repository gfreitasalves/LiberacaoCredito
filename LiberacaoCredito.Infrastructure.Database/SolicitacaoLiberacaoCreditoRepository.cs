using LiberacaoCredito.Application.Abstractions.Commands;
using LiberacaoCredito.Domain.Models;

namespace LiberacaoCredito.Infrastructure.Database
{
    public class SolicitacaoLiberacaoCreditoRepository : ISolicitacaoLiberacaoCreditoRepository
    {
        private readonly LiberacaoCreditoDbContext _liberacaoCreditoDbContext;

        public SolicitacaoLiberacaoCreditoRepository(LiberacaoCreditoDbContext liberacaoCreditoDbContext)
        {
            _liberacaoCreditoDbContext= liberacaoCreditoDbContext;
        }
        public async Task<SolicitacaoLiberacaoCredito> InserirAsync(SolicitacaoLiberacaoCredito solicitacaoLiberacaoCredito)
        {
            _liberacaoCreditoDbContext.SolicitacaoLiberacaoCreditos.Add(solicitacaoLiberacaoCredito);

            await _liberacaoCreditoDbContext.SaveChangesAsync();

            return solicitacaoLiberacaoCredito;
        }
    }
}
