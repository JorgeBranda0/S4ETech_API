using api.Context.Dtos.EmpresaDtos;
using api.Models;
using AutoMapper;

namespace api.Profiles
{
    public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            CreateMap<CreateEmpresaDto, Empresa>();
            CreateMap<Empresa, ReadEmpresaDto>();
            CreateMap<UpdateEmpresaDto, Empresa>();
        }
    }
}
