using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ValueObject
{
    public sealed class Price : ValueObject
    {
        public decimal Amount { get; private set; }

        public string Currency { get; private set; }

        private Price() { }

        public Price(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentException("Price cannot be negative");

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required");

            Amount = amount;
            Currency = currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public static Price Create(decimal amount, string currency)
            => new Price(amount, currency);

        public override string ToString()
            => $"{Amount} {Currency}";
    }
}
