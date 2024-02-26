using LiberacaoCredito.Domain.Enums;

namespace LiberacaoCredito.Application.InputPorts
{
    public class SolicitacaoLiberacaoCreditoOutput
    {
        public SolicitacaoLiberacaoCreditoOutput(StatusSolicitacaoLiberacaoCredito status, decimal valorFinal, decimal valorJuros, ICollection<string> mensagens)
        {
            Status = status.ToString();
            ValorFinal = valorFinal;
            ValorJuros = valorJuros;
            Mensagens = mensagens;
        }

        public string Status { get; private set; }
        public decimal ValorFinal { get; private set; }
        public decimal ValorJuros { get; private set; }

        public ICollection<string> Mensagens { get; private set; } = new List<string>();
    }
}
