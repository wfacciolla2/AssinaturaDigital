using System;

namespace AssinaturaDigital.Exceptions
{
    public class AssinaturaNaoCadastradaException : Exception
    {
        public AssinaturaNaoCadastradaException()
            : base("Esta assinatura não esta cadastrada") { }
    }
}
