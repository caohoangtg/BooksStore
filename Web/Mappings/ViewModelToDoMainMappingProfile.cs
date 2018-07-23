using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.ViewModels;

namespace Web.Mappings
{
    public class ViewModelToDoMainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
        public ViewModelToDoMainMappingProfile()
        {
            CreateMap<CategoryViewModel, Categories>();
            CreateMap<CommentViewModel, Comments>();
            CreateMap<MarkViewModel, Marks>();
            CreateMap<PostViewModel, Posts>();
            CreateMap<ProfileViewModel, Profiles>();
            CreateMap<ReplyViewModel, Replys>();
            CreateMap<RoleViewModel, Roles>();
            CreateMap<TopicViewModel, Topics>();
            CreateMap<UserViewModel, Users>();
        }
    }
}