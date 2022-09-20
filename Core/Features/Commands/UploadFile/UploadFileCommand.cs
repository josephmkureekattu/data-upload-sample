using Core.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Commands.UploadFile
{
    public class UploadFileCommand : IRequest<Batch>
    {
        public IFormFileCollection Files { get; set; }
    }
}
