
using LiberacaoCredito.Domain.Enums;

namespace LiberacaoCredito.Domain.Models
{
    public class SolicitacaoLiberacaoCredito : IEntity
    {
        public SolicitacaoLiberacaoCredito()
        {
            Credito = new LinhaCredito();
        }
        public SolicitacaoLiberacaoCredito(string cpfCnpj, LinhaCredito credito, decimal valorSolicitado, int quantidadeParcelas, DateTime dataPrimeiroVencimento)
        {
            CpfCnpj = cpfCnpj;
            Credito = credito;
            Status = StatusSolicitacaoLiberacaoCredito.Aprovado;
            ValorSolicitado = valorSolicitado;
            QuantidadeParcelas = quantidadeParcelas;
            DataPrimeiroVencimento = dataPrimeiroVencimento;

            ValidarRegras();

            CalcularValores();
        }

        public Guid Id { get; }
        public string CpfCnpj { get; private set; } = string.Empty;
        public TipoCpfCnpjEnum TipoCpfCnpj { get; private set; }
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
                if (TipoCpfCnpj == TipoCpfCnpjEnum.Invalido)
                {
                    Mensagens.Add("Cpf/Cnpj inválido.");
                }

                Status =
                    TipoCpfCnpj != TipoCpfCnpjEnum.Invalido &&
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

        private void CalcularValores()
        {
            ValorJuros = (ValorSolicitado * (((Credito.Taxa/100) / 12) * QuantidadeParcelas));
            ValorFinal = ValorSolicitado + ValorJuros;
        }

        private bool ValidarIntervaloValor()
        {
            var isValid = false;

            if (TipoCpfCnpj == TipoCpfCnpjEnum.CPF)
            {
                isValid = ValorSolicitado >= Credito.ValorMinimoPessoaFisica && ValorSolicitado <= Credito.ValorMaximoPessoaFisica;
            }
            else if (TipoCpfCnpj == TipoCpfCnpjEnum.CNPJ)
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

        public void ValidateCpfCnpj(string number)
        {
            if (IsCpf(number))
                TipoCpfCnpj = TipoCpfCnpjEnum.CPF;
            else if (IsCnpj(number))
                TipoCpfCnpj = TipoCpfCnpjEnum.CNPJ;
        }
        private static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}
