using AutoMapper;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Domain.Enitites;

namespace CleanArchitecture.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping() {
            CreateMap<Blog, BlogCreateDto>().ReverseMap();
            CreateMap<Blog, BlogUpdateDto>().ReverseMap();
        }
    }
}
