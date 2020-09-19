using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Azureference.Web.Pages
{
    public class Files : PageModel
    {
        private readonly IConfiguration _config;

        public Files(IConfiguration config)
        {
            _config = config;
            ContainerName = _config.GetValue<string>("Files:BlobContainerName");
        }

        public string ContainerName { get; }
        public IEnumerable<BlobModel> Blobs { get; private set; }

        public async Task OnGetAsync()
        {
            var connectionString = _config.GetValue<string>("Files:ConnectionString");
            var blobContainerClient = new BlobContainerClient(connectionString, ContainerName);

            if (!blobContainerClient.Exists())
            {
                Blobs = Enumerable.Empty<BlobModel>();
            }
            else
            {
                Blobs = await blobContainerClient.GetBlobsAsync()
                    .Select(b => new BlobModel(b.Name))
                    .ToListAsync();
            }
        }


        public class BlobModel
        {
            public string Name { get; }

            public BlobModel(string name)
            {
                Name = name;
            }
        }
    }
}
