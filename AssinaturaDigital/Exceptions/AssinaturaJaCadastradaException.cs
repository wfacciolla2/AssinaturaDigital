using System;

namespace AssinaturaDigital.Exceptions
{
    public class AssinaturaJaCadastradaException : Exception
    {
        public AssinaturaJaCadastradaException()
            : base("Esta assinatura já está cadastrada") { }
    }
}
