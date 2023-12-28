using api.Context.Dtos.AfiliacaoDtos;
using api.Models;
using AutoMapper;

namespace api.Profiles
{
    public class AfiliacaoProfile : Profile
    {
        public AfiliacaoProfile() 
        {
            CreateMap<CreateAfiliacaoDto, Afiliacao>();
            CreateMap<Afiliacao, ReadAfiliacaoDto>();
            CreateMap<UpdateAfiliacaoDto, Afiliacao>();
        }
    }
}
