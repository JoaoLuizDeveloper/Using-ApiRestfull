using AutoMapper;
using NavitaAPI.Models;
using NavitaAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavitaAPI.Mapper
{
    public class NavitaMappings: Profile
    {
        public NavitaMappings()
        {
            CreateMap<Marca, MarcaDto>().ReverseMap();
            CreateMap<Patrimonio, PatrimonioDto>().ReverseMap();
            CreateMap<Patrimonio, PatrimonioCreateDto>().ReverseMap();
            CreateMap<Patrimonio, PatrimonioUpdateDto>().ReverseMap();
        }
    }
}
