using System;
using System.Threading.Tasks;
using Azureference.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azureference.Web.Pages
{
    public class Database : PageModel
    {
        private readonly DemoContext _db;

        public Database(DemoContext db)
        {
            _db = db;
        }

        public bool CanConnect { get; private set; }
        public string ErrorMessage { get; private set; }

        public async Task OnGetAsync()
        {
            try
            {
                CanConnect = await _db.Database.CanConnectAsync();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
    }
}
