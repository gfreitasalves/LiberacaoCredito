using LiberacaoCredito.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberacaoCredito.Application.Abstractions.Commands
{
    public interface ISolicitacaoLiberacaoCreditoRepository
    {
        Task<SolicitacaoLiberacaoCredito> InserirAsync(SolicitacaoLiberacaoCredito solicitacaoLiberacaoCredito);
    }
}
