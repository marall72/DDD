using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ValueObject
{
    public class Email : ValueObject
    {
        public string Value { get; private set; }

        private Email() { }

        public Email(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be null or empty.");

            if (!string.IsNullOrEmpty(value) && value.Length > 255)
                throw new ArgumentException("Email cannot be longer than 255 characters.");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
