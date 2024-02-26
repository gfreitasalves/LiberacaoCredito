
namespace LiberacaoCredito.Domain.Models

{
    public class LinhaCredito : IEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Descricao { get; set; } = string.Empty;
        public decimal Taxa { get; set; }
        public decimal ValorMaximoPessoaFisica { get; set; }
        public decimal ValorMaximoPessoaJuridica { get; set; }
        public decimal ValorMinimoPessoaFisica { get; set; }
        public decimal ValorMinimoPessoaJuridica { get; set; }
        public int MaximoParcelas { get; set; }
        public int MinimoParcelas { get; set; }
        public int MaximoDiasPrimeiroVencimento { get; set; }
        public int MinimoDiasPrimeiroVencimento { get; set; }        
    }
}
