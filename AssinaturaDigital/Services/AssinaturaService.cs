using AssinaturaDigital.Entities;
using AssinaturaDigital.Exceptions;
using AssinaturaDigital.Model;
using AssinaturaDigital.Repositories;
using AssinaturaDigital.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssinaturaDigital.Services
{
    public class AssinaturaService : IAssinaturaService
    {
        private readonly IAssinaturaRepository _assinaturaRepository;

        public AssinaturaService(IAssinaturaRepository assinaturaRepository)
        {
            _assinaturaRepository = assinaturaRepository;
        }

        public async Task<List<AssinaturaViewModel>> Obter(int pagina, int quantidade)
        {
            var assinaturas = await _assinaturaRepository.Obter(pagina, quantidade);
            return assinaturas.Select(assinatura => new AssinaturaViewModel
            {
                Id = assinatura.Id,
                Url = assinatura.Url,
                Nome = assinatura.Nome,
                Cargo = assinatura.Cargo,
                Empresa = assinatura.Empresa,
                Telefone = assinatura.Telefone,
                Email = assinatura.Email,
                Site = assinatura.Site,
                Endereco = assinatura.Endereco,
                Usuario = assinatura.Usuario,
                Senha = assinatura.Senha,

            }).ToList();
        }
        public async Task<AssinaturaViewModel> Obter(Guid id)
        {
            var assinatura = await _assinaturaRepository.Obter(id);

            if (assinatura == null)
                return null;

            return new AssinaturaViewModel
            {
                Id = assinatura.Id,
                Url = assinatura.Url,
                Nome = assinatura.Nome,
                Cargo = assinatura.Cargo,
                Empresa = assinatura.Empresa,
                Telefone = assinatura.Telefone,
                Email = assinatura.Email,
                Site = assinatura.Site,
                Endereco = assinatura.Endereco,
                Usuario = assinatura.Usuario,
                Senha = assinatura.Senha,
            };
        }
        public async Task<AssinaturaViewModel> Inserir(AssinaturaInputModel assinatura)
        {
            var entidadeAssinatura = await _assinaturaRepository.Obter(assinatura.Email, assinatura.Usuario);

            if (entidadeAssinatura.Count > 0)
                throw new AssinaturaJaCadastradaException();

            var assinaturaInsert = new Assinatura
            {
                Id = Guid.NewGuid(),
                Nome = assinatura.Nome,
                Telefone = assinatura.Telefone,
                Url = assinatura.Url,
                Cargo = assinatura.Cargo,
                Empresa = assinatura.Empresa,
                Endereco = assinatura.Endereco,
                Email = assinatura.Email,
                Site = assinatura.Site,
                Usuario = assinatura.Usuario,
                Senha = assinatura.Senha,
            };

            await _assinaturaRepository.Inserir(assinaturaInsert);

            return new AssinaturaViewModel
            {
                Id = assinaturaInsert.Id,
                Nome = assinatura.Nome,
                Telefone = assinatura.Telefone,
                Url = assinatura.Url,
                Cargo = assinatura.Cargo,
                Empresa = assinatura.Empresa,
                Endereco = assinatura.Endereco,
                Email = assinatura.Email,
                Site = assinatura.Site,
                Usuario = assinatura.Usuario,
                Senha = assinatura.Senha,
            };

        }

        public async Task Atualizar(Guid id, AssinaturaInputModel assinatura)
        {
            var entidadeAssinatura = await _assinaturaRepository.Obter(id);

            if (entidadeAssinatura == null)
                throw new AssinaturaNaoCadastradaException();

            entidadeAssinatura.Nome = assinatura.Nome;
            entidadeAssinatura.Telefone = assinatura.Telefone;
            entidadeAssinatura.Url = assinatura.Url;
            entidadeAssinatura.Cargo = assinatura.Cargo;
            entidadeAssinatura.Empresa = assinatura.Empresa;
            entidadeAssinatura.Endereco = assinatura.Endereco;
            entidadeAssinatura.Email = assinatura.Email;
            entidadeAssinatura.Site = assinatura.Site;
            entidadeAssinatura.Usuario = assinatura.Usuario;
            entidadeAssinatura.Senha = assinatura.Senha;

            await _assinaturaRepository.Atualizar(entidadeAssinatura);

        }

        public async Task Atualizar(Guid id, string url)
        {
            var entidadeAssinatura = await _assinaturaRepository.Obter(id);

            if (entidadeAssinatura == null)
                throw new AssinaturaNaoCadastradaException();

            entidadeAssinatura.Url = url;

            await _assinaturaRepository.Atualizar(entidadeAssinatura);
        }

        public async Task Remover(Guid id)
        {
            var assinatura = _assinaturaRepository.Obter(id);

            if (assinatura == null)
                throw new AssinaturaNaoCadastradaException();

            await _assinaturaRepository.Remover(id);
        }

        public void Dispose()
        {
            _assinaturaRepository.Dispose();
        }
    }
}
