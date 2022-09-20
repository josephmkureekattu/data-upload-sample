﻿using AutoMapper;
using Azure;
using Azure.Storage.Blobs;
using Core.DTO;
using Core.Entity;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedKernal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Commands.UploadFile
{
    public class UploadFileCommandhandler : IRequestHandler<UploadFileCommand, BatchDTO>
    {
        private readonly IConfiguration configuration;
        private readonly IRepository<Batch> repository;
        private readonly IMapper mapper;
        private readonly ILogger<UploadFileCommandhandler> logger;
        public UploadFileCommandhandler(IConfiguration configuration, IRepository<Batch> repository, IMapper mapper, ILogger<UploadFileCommandhandler> logger)
        {
            this.configuration = configuration;
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<BatchDTO> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {

            throw new Exception("exception from handler start");
            BlobClient blob = new BlobClient(new Uri(configuration["DataUploadBlob:Uri"] + "upload/" + request.Files[0].FileName), new AzureSasCredential(configuration["DataUploadBlob:SASToken"]));
            using (var stream = new MemoryStream())
            {
                await request.Files[0].CopyToAsync(stream);
                await blob.UploadAsync(stream);
            }

            UploadLog uploadLog = new UploadLog { Message = "testmessage" };

            Entity.File f = new Entity.File { FileIdentifier = Guid.NewGuid(), FileName = request.Files[0].FileName, uploadLogs = new UploadLog[] { uploadLog }  };

            Batch b = new Batch { BatchIdentifier = Guid.NewGuid(), files = new Entity.File[] { f } };

            var resp =  await this.repository.AddAsync(b);
           throw new Exception("insert done");
            return this.mapper.Map<BatchDTO>(resp);
        }
    }
}
