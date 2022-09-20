using AutoMapper;
using Core.DTO;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Batch, BatchDTO>()
                .ForMember(destination => destination.BatchIdentifier, opt => opt.MapFrom(x => x.BatchIdentifier))
                .ForMember(destination => destination.FileNames, opt => opt.MapFrom(x => x.files.Select(x => x.FileName)));
        }
    }
}
