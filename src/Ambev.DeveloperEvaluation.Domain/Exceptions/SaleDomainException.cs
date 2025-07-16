using System;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions
{
    public class SaleDomainException : Exception
    {
        public SaleDomainException(string message) : base(message) { }
    }
}
