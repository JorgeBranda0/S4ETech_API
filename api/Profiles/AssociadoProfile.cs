using AutoMapper;
using api.Models;
using api.Context.Dtos.AssociadoDtos;

namespace api.Profiles
{
    public class AssociadoProfile : Profile
    {
        public AssociadoProfile()
        {
            CreateMap<CreateAssociadoDto, Associado>();
            CreateMap<Associado, ReadAssociadoDto>();
            CreateMap<UpdateAssociadoDto, Associado>();
        }
    }
}
