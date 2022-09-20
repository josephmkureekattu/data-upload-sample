using Azure;
using Azure.Storage.Blobs;
using Core.Entity;
using MediatR;
using Microsoft.Extensions.Configuration;
using SharedKernal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Commands.UploadFile
{
    public class UploadFileCommandhandler : IRequestHandler<UploadFileCommand, Batch>
    {
        private readonly IConfiguration configuration;
        private readonly IRepository<Batch> repository;
        public UploadFileCommandhandler(IConfiguration configuration, IRepository<Batch> repository)
        {
            this.configuration = configuration;
            this.repository = repository;
        }

        public async Task<Batch> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            BlobClient blob = new BlobClient(new Uri(configuration["DataUploadBlob:Uri"] + "upload/" + request.Files[0].FileName), new AzureSasCredential(configuration["DataUploadBlob:SASToken"]));
            using (var stream = new MemoryStream())
            {
                await request.Files[0].CopyToAsync(stream);
                await blob.UploadAsync(stream);
            }

            UploadLog uploadLog = new UploadLog { Message = "testmessage" };

            Entity.File f = new Entity.File { FileIdentifier = Guid.NewGuid(), FileName = request.Files[0].FileName, uploadLogs = new UploadLog[] { uploadLog }  };

            Batch b = new Batch { BatchIdentifier = Guid.NewGuid(), files = new Entity.File[] { f } };

            return await this.repository.AddAsync(b);
        }
    }
}
