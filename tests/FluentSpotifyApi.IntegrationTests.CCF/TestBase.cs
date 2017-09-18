using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class TestBase
    {
        protected IFluentSpotifyClient Client { get; private set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            this.Client = AssemblyInitializer.Client;
        }
    }
}
