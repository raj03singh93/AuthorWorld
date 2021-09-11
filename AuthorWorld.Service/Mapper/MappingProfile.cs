using AuthorWorld.Domain.Data.Model;
using AuthorWorld.DTOs.DTOs;
using AutoMapper;

namespace AuthorWorld.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Books, BookDTO>().ReverseMap();
        }
    }
}
