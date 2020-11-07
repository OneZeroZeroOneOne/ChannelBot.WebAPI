using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using AutoMapper;
using ChannelBot.DAL.Models;
using ChannelBot.DAL.ViewModel.Response;

namespace ChannelBot.DAL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Category, CategoryResponseViewModel>().ForMember(x => x.Groups, x => x.MapFrom(y => y.Group.ToList()));
            CreateMap<Source, SourceResponseViewModel>();
            CreateMap<Group, GroupResponseViewModel>().ForMember(x => x.Sources, x => x.MapFrom(y => y.GroupSource.Select(t => t.Source)));

        }
    }
}
