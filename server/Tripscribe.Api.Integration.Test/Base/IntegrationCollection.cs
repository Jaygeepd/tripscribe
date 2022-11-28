using Xunit;

namespace Tripscribe.Api.Integration.Test.Base;

[CollectionDefinition("Integration")]
public class IntegrationCollection : ICollectionFixture<IntegrationClassFixture>
{ }