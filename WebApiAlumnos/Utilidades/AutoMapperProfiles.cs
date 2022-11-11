using WebApiAlumnos.DTOs;
using WebApiAlumnos.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;


namespace WebApiAlumnos.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EmpresaCreacionDTO, Empresa>();
            CreateMap<Empresa, EmpresaDTO>();
            CreateMap<Empresa, EmpresaDTOConPais>().ForMember(EmpresaDTO => EmpresaDTO.Pais, opciones => opciones.MapFrom(MapEmpresaDTOPais));

            CreateMap<PaisCreacionDTO, Pais>().ForMember(Pais => Pais.EmpresaPais, opciones => opciones.MapFrom(MapEmpresaPais));

            CreateMap<Pais, PaisDTO>();
            CreateMap<Pais, PaisesDTOConEmpresas>().ForMember(PaisDTO => PaisDTO.Empresas, opciones => opciones.MapFrom(MapPaisDTOEmpresa));

            CreateMap<OpinionesCreacionDTO, Opiniones>();
            CreateMap<Opiniones, OpinionesDTO>();
        }

        private List<PaisDTO> MapEmpresaDTOPais(Empresa empresa, EmpresaDTO empresaDTO)
        {
            var resultado = new List<PaisDTO>();
            if(empresa.EmpresasPais == null)
            {
                return resultado;
            }

            foreach( var empresaPais in empresa.EmpresasPais)
            {
                resultado.Add(new PaisDTO()
                {
                    Id = empresaPais.PaisId,
                    Name = empresaPais.Pais.Nombre
                });
            }
            return resultado;
        }

       
        private List<EmpresaDTO> MapPaisDTOEmpresa(Pais pais, PaisDTO paisDTO)
        {
            var resultado = new List<EmpresaDTO>();
            if(pais.EmpresaPais == null)
            {
                return resultado;
            }

            foreach( var empresaPais in pais.EmpresaPais)
            {
                resultado.Add(new EmpresaDTO()
                {
                    Id = empresaPais.EmpresaId,
                    Name = empresaPais.Pais.Nombre
                }); ;
            }
            return resultado;
        }

        private List<EmpresaPais> MapEmpresaPais(PaisCreacionDTO paisCreacionDTO, Pais pais)
        {
            var resultado = new List<EmpresaPais>();
            if (paisCreacionDTO.EmpresasIds == null)
            {
                return resultado;
            }

            foreach (var empresaid in paisCreacionDTO.EmpresasIds)
            {
                resultado.Add(new EmpresaPais()
                {
                    EmpresaId = empresaid
                });
            }
            return resultado;
        }



    }
}
