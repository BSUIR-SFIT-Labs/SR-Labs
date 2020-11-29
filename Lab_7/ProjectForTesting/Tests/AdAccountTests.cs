using FluentAssertions;
using NUnit.Framework;
using ProjectForTesting.Domain.Exceptions;
using ProjectForTesting.Domain.ValueObjects;

namespace Tests
{
    internal class AdAccountTests
    {
        [Test]
        public void ShouldHaveCorrectDomainAndName()
        {
            const string accountString = "MyCompany\\Shulga";

            var account = AdAccount.For(accountString);

            account.Domain.Should().Be("MyCompany");
            account.Name.Should().Be("Shulga");
        }

        [Test]
        public void ToStringReturnsCorrectFormat()
        {
            const string accountString = "MyCompany\\Shulga";

            var account = AdAccount.For(accountString);

            string result = account.ToString();

            result.Should().Be(accountString);
        }

        [Test]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            const string accountString = "MyCompany\\Shulga";

            var account = AdAccount.For(accountString);

            string result = account;

            result.Should().Be(accountString);
        }

        [Test]
        public void ExplicitConversionFromStringSetsDomainAndName()
        {
            const string accountString = "MyCompany\\Shulga";

            var account = (AdAccount)accountString;

            account.Domain.Should().Be("MyCompany");
            account.Name.Should().Be("Shulga");
        }

        [Test]
        public void ShouldThrowAdAccountInvalidExceptionForInvalidAdAccount()
        {
            FluentActions.Invoking(() => (AdAccount)"MyCompanyShulga")
                .Should().Throw<AdAccountInvalidException>();
        }
    }
}