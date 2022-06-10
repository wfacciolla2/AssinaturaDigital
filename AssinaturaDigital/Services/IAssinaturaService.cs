using AssinaturaDigital.Model;
using AssinaturaDigital.View;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssinaturaDigital.Services
{
    public interface IAssinaturaService : IDisposable
    {
        Task<List<AssinaturaViewModel>> Obter(int pagina, int quantidade);
        Task<AssinaturaViewModel> Obter(Guid id);
        Task<AssinaturaViewModel> Inserir(AssinaturaInputModel assinatura);
        Task Atualizar(Guid id, AssinaturaInputModel assinatura);
        Task Atualizar(Guid id, string url);
        Task Remover(Guid id);
    }
}
