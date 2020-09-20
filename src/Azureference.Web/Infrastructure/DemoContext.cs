using Microsoft.EntityFrameworkCore;

namespace Azureference.Web.Infrastructure
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }
    }
}
