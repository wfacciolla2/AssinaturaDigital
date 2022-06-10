using AssinaturaDigital.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssinaturaDigital.Repositories
{
    public class AssinaturaRepository : IAssinaturaRepository
    {

        private static Dictionary<Guid, Assinatura> assinaturas = new Dictionary<Guid, Assinatura>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Assinatura{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Url = "www.imagemdainternet.com.br", Nome = "Wellington Facciolla Lopes", Cargo = "Programador .Net", Email = "facciollacorp@gmail.com", Empresa = "Microsoft", Endereco = "Santa Branca", Site = "facciollacorp.com", Telefone = "12988830267", Usuario = "wfacciolla", Senha = "segredo123"} },
        };

        public Task<List<Assinatura>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(assinaturas.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Assinatura> Obter(Guid id)
        {
            if (!assinaturas.ContainsKey(id))
                return null;

            return Task.FromResult(assinaturas[id]);
        }

        public Task<List<Assinatura>> Obter(string usuario, string email)
        {
            return Task.FromResult(assinaturas.Values.Where(assinatura => assinatura.Usuario.Equals(usuario) && assinatura.Email.Equals(email)).ToList());
        }

        public Task<List<Assinatura>> ObterSemLambda(string usuario, string email)
        {
            var retorno = new List<Assinatura>();

            foreach (var assinatura in assinaturas.Values)
            {
                if (assinatura.Usuario.Equals(usuario) && assinatura.Email.Equals(email))
                    retorno.Add(assinatura);
            }
            return Task.FromResult(retorno);
        }

        public Task Inserir(Assinatura assinatura)
        {
            assinaturas.Add(assinatura.Id, assinatura);
            return Task.CompletedTask;
        }

        public Task Atualizar(Assinatura assinatura)
        {
            assinaturas[assinatura.Id] = assinatura;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            assinaturas.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
