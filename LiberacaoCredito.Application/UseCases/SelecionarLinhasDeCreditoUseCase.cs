using LiberacaoCredito.Application.Abstractions.Queries;
using LiberacaoCredito.Application.UseCases.Interfaces;
using LiberacaoCredito.Domain.Models;

namespace LiberacaoCredito.Application.UseCases
{
    public class SelecionarLinhasDeCreditoUseCase : ISelecionarLinhasDeCreditoUseCase
    {
        private readonly IObterLinhaCreditoQuery _obterCredito;
        
        public SelecionarLinhasDeCreditoUseCase(IObterLinhaCreditoQuery obterCredito)
        {
            _obterCredito = obterCredito;            
        }

        public async Task<IEnumerable<LinhaCredito>> SelecionarAsync()
        {
            return await _obterCredito.SelecionarTodosAsync();          
        }               
    }
}
