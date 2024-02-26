using LiberacaoCredito.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LiberacaoCredito.Infrastructure.Database
{
    public class LiberacaoCreditoDbContext : DbContext
    {
        public LiberacaoCreditoDbContext(DbContextOptions<LiberacaoCreditoDbContext> options)
          : base(options)
        {
            AdicionarDadosDeTeste();
        }

        public DbSet<LinhaCredito> LinhaCreditos { get; set; }
        public DbSet<SolicitacaoLiberacaoCredito> SolicitacaoLiberacaoCreditos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LinhaCredito>().HasKey(l => l.Id);
            modelBuilder.Entity<SolicitacaoLiberacaoCredito>().HasKey(s => s.Id);            

            base.OnModelCreating(modelBuilder);
        }

        public void AdicionarDadosDeTeste() 
        {
            if (!LinhaCreditos.Any())
            {
                LinhaCreditos.Add(new LinhaCredito()
                {
                    Descricao= "Crédito Direto Taxa de 2%",
                    MaximoDiasPrimeiroVencimento = 40,
                    MaximoParcelas = 72,
                    MinimoDiasPrimeiroVencimento=15,
                    MinimoParcelas = 5,
                    Taxa =2,
                    ValorMaximoPessoaFisica = 1000000,
                    ValorMaximoPessoaJuridica = 1000000,
                    ValorMinimoPessoaFisica =1,
                    ValorMinimoPessoaJuridica = 15000
                });
                LinhaCreditos.Add(new LinhaCredito()
                {
                    Descricao = "Crédito Consignado Taxa de 1%",
                    MaximoDiasPrimeiroVencimento = 40,
                    MaximoParcelas = 72,
                    MinimoDiasPrimeiroVencimento = 15,
                    MinimoParcelas = 5,
                    Taxa = 1,
                    ValorMaximoPessoaFisica = 1000000,
                    ValorMaximoPessoaJuridica = 0,
                    ValorMinimoPessoaFisica = 1,
                    ValorMinimoPessoaJuridica = 0
                });
                LinhaCreditos.Add(new LinhaCredito()
                {
                    Descricao = "Crédito Pessoa Jurídica Taxa de 5%",
                    MaximoDiasPrimeiroVencimento = 40,
                    MaximoParcelas = 72,
                    MinimoDiasPrimeiroVencimento = 15,
                    MinimoParcelas = 5,
                    Taxa = 5,
                    ValorMaximoPessoaFisica = 0,
                    ValorMaximoPessoaJuridica = 1000000,
                    ValorMinimoPessoaFisica = 0,
                    ValorMinimoPessoaJuridica = 15000
                });
                LinhaCreditos.Add(new LinhaCredito()
                {
                    Descricao = "Crédito Pessoa Física Taxa de 3%",
                    MaximoDiasPrimeiroVencimento = 40,
                    MaximoParcelas = 72,
                    MinimoDiasPrimeiroVencimento = 15,
                    MinimoParcelas = 5,
                    Taxa = 3,
                    ValorMaximoPessoaFisica = 1000000,
                    ValorMaximoPessoaJuridica = 0,
                    ValorMinimoPessoaFisica = 1,
                    ValorMinimoPessoaJuridica = 0
                });
                LinhaCreditos.Add(new LinhaCredito()
                {
                    Descricao = "Crédito Imobiliário Taxa de 9%",
                    MaximoDiasPrimeiroVencimento = 40,
                    MaximoParcelas = 72,
                    MinimoDiasPrimeiroVencimento = 15,
                    MinimoParcelas = 5,
                    Taxa = 9,
                    ValorMaximoPessoaFisica = 1000000,
                    ValorMaximoPessoaJuridica = 1000000,
                    ValorMinimoPessoaFisica = 1,
                    ValorMinimoPessoaJuridica = 15000
                });

                SaveChanges();
            }
        }
    }
}
