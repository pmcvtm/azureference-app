using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Azureference.Web.Infrastructure
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
            AddTokenToConnection().Wait();
        }

        private async Task AddTokenToConnection()
        {
            var connection = (Microsoft.Data.SqlClient.SqlConnection)Database.GetDbConnection();

            var tokenRequestContext = new TokenRequestContext(new[] { "https://database.windows.net//.default" });
            var tokenRequestResult = await new DefaultAzureCredential().GetTokenAsync(tokenRequestContext);

            connection.AccessToken = tokenRequestResult.Token;
        }

    }
}
