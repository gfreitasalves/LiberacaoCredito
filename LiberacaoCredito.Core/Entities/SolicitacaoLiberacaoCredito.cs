
using LiberacaoCredito.Domain.Enums;
using LiberacaoCredito.Domain.ValueObjects;

namespace LiberacaoCredito.Domain.Models

{
    public class SolicitacaoLiberacaoCredito : IEntity
    {
        public SolicitacaoLiberacaoCredito(string cpfCnpj, LinhaCredito credito, decimal valorSolicitado, int quantidadeParcelas, DateTime dataPrimeiroVencimento)
        {
            CpfCnpj = new CpfCnpjVo(cpfCnpj);
            Credito = credito;
            Status = StatusSolicitacaoLiberacaoCredito.Aprovado;
            ValorSolicitado = valorSolicitado;
            QuantidadeParcelas = quantidadeParcelas;
            DataPrimeiroVencimento = dataPrimeiroVencimento;

            ValidarRegras();
        }

        public Guid Id { get; }
        public CpfCnpjVo CpfCnpj { get; private set; }
        public StatusSolicitacaoLiberacaoCredito Status { get; private set; }
        public decimal ValorSolicitado { get; private set; }
        public int QuantidadeParcelas { get; private set; }
        public DateTime DataPrimeiroVencimento { get; private set; }
        public decimal ValorFinal { get; private set; }
        public decimal ValorJuros { get; private set; }
        public LinhaCredito Credito { get; private set; }

        public ICollection<string> Mensagens { get; private set; } = new List<string>();

        private void ValidarRegras()
        {
            if (Credito != null)
            {
                if (CpfCnpj.Tipo == CpfCnpjVo.CpfCnpj.Invalido)
                {
                    Mensagens.Add("Cpf/Cnpj inválido.");
                }

                Status =
                    CpfCnpj.Tipo != CpfCnpjVo.CpfCnpj.Invalido &&
                    Credito != null &&
                    ValidarIntervaloValor() &&
                    ValidarParcelas() &&
                    ValidarVencimento()
                    ?
                    StatusSolicitacaoLiberacaoCredito.Aprovado
                    :
                    StatusSolicitacaoLiberacaoCredito.Recusado;
            }
            else
            {
                Status = StatusSolicitacaoLiberacaoCredito.Recusado;
                Mensagens.Add("Linha de credito inválida.");
            }
        }

        private bool ValidarIntervaloValor()
        {
            var isValid = false;

            if (CpfCnpj.Tipo == CpfCnpjVo.CpfCnpj.CPF)
            {
                isValid = ValorSolicitado >= Credito.ValorMinimoPessoaFisica && ValorSolicitado <= Credito.ValorMaximoPessoaFisica;
            }
            else if (CpfCnpj.Tipo == CpfCnpjVo.CpfCnpj.CNPJ)
            {
                isValid = ValorSolicitado >= Credito.ValorMinimoPessoaJuridica && ValorSolicitado <= Credito.ValorMaximoPessoaJuridica;
            }

            if (!isValid)
                Mensagens.Add("Intervalo de valor inválido.");

            return isValid;
        }

        private bool ValidarParcelas()
        {
            var isValid = QuantidadeParcelas >= Credito.MinimoParcelas && QuantidadeParcelas <= Credito.MaximoParcelas;

            if (!isValid)
                Mensagens.Add("Número de parcelas inválido.");

            return isValid;
        }

        private bool ValidarVencimento()
        {
            var isValid = DataPrimeiroVencimento >= DateTime.Now.AddDays(Credito.MinimoDiasPrimeiroVencimento) && DataPrimeiroVencimento <= DateTime.Now.AddDays(Credito.MaximoDiasPrimeiroVencimento);

            if (!isValid)
                Mensagens.Add("Data primeiro vencimento inválida.");

            return isValid;
        }
    }
}
