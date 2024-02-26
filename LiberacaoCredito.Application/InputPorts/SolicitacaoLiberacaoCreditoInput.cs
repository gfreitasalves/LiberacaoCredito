namespace LiberacaoCredito.Application.InputPorts
{
    public class SolicitacaoLiberacaoCreditoInput
    {
        public Guid IdCredito { get; set; }
        public string CpfCnpj { get; set; } = string.Empty;
        public decimal ValorSolicitado { get; set; }
        public int QuantidadeParcelas { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }        
    }
}
