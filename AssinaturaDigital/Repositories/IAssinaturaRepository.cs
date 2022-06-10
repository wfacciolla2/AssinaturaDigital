using AssinaturaDigital.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssinaturaDigital.Repositories
{
    public interface IAssinaturaRepository : IDisposable
    {
        Task<List<Assinatura>> Obter(int pagina, int quantidade);
        Task<Assinatura> Obter(Guid id);
        Task<List<Assinatura>> Obter(string Usuario, string Email);
        Task Inserir(Assinatura assinatura);
        Task Atualizar(Assinatura assinatura);
        Task Remover(Guid id);
    }
}
