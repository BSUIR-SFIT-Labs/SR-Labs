using ProjectForTesting.Domain.Common;
using ProjectForTesting.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ProjectForTesting.Domain.ValueObjects
{
    public class AdAccount : ValueObject
    {
        private AdAccount()
        {
        }

        public string Domain { get; private set; }

        public string Name { get; private set; }

        public static AdAccount For(string accountString)
        {
            var account = new AdAccount();

            try
            {
                int index = accountString.IndexOf("\\", StringComparison.Ordinal);
                account.Domain = accountString.Substring(0, index);
                account.Name = accountString.Substring(index + 1);
            }
            catch (Exception ex)
            {
                throw new AdAccountInvalidException(accountString, ex);
            }

            return account;
        }

        public static implicit operator string(AdAccount account)
        {
            return account.ToString();
        }

        public static explicit operator AdAccount(string accountString)
        {
            return For(accountString);
        }

        public override string ToString()
        {
            return $"{Domain}\\{Name}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Domain;
            yield return Name;
        }
    }
}