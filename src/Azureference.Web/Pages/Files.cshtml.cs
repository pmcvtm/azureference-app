using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Azureference.Web.Pages
{
    public class Files : PageModel
    {
        public Files(IConfiguration config)
        {
            ContainerUri = new Uri(config.GetValue<string>("Files:BlobContainerUri"));
        }

        public Uri ContainerUri { get; }
        public string ContainerName => ContainerUri.Segments.Last();
        public string ErrorMessage { get; private set; }
        public IEnumerable<BlobModel> Blobs { get; private set; }

        public async Task OnGetAsync()
        {
            try
            {
                var blobContainerClient = new BlobContainerClient(ContainerUri, new DefaultAzureCredential());

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
            catch (Exception e)
            {
                ErrorMessage = e.Message;
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
