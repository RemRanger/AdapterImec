using System.Collections.Generic;

namespace AdapterImec.Domain.ValueObjects
{
    public class CompanyId : ValueObject
    {
        public string Scheme { get; private set; }
        public string Value { get; private set; }

        public CompanyId() {}

        public CompanyId(string scheme, string value)
        {
            Scheme = scheme;
            Value = value;
        }

        public override string ToString()
        {
            return $"{this.Scheme}-{this.Value}";
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Scheme;
            yield return Value;
        }
    }
}
