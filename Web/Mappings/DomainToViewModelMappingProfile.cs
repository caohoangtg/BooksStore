using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.ViewModels;

namespace Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Categories, CategoryViewModel>();
            CreateMap<Comments, CommentViewModel>();
            CreateMap<Marks, MarkViewModel>();
            CreateMap<Posts, PostViewModel>();
            CreateMap<Profiles, ProfileViewModel>();
            CreateMap<Replys, ReplyViewModel>();
            CreateMap<Roles, RoleViewModel>();
            CreateMap<Topics, TopicViewModel>();
            CreateMap<Users, UserViewModel>();
        }
    }
}