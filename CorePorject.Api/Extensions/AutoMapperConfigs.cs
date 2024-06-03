using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorePorject.Model;
using CorePorject.DTO.System;
using CorePorject.DTO.Member;

namespace CorePorject.Api
{
    public class AutoMapperConfigs : Profile
    {
        //添加你的实体Model和DTO映射关系.
        public AutoMapperConfigs()
        {
            //System
            SystemDto();

            //Member
            MemberDto();

            //CreateMap<GoodType, GoodTypeDto>().ReverseMap();
            //CreateMap<GoodSpecKey, GoodSpecKeyDto>().ReverseMap();
            //CreateMap<GoodSpecValue, GoodSpecValueDto>().ReverseMap();

            ////Entity转Dto.
            //CreateMap<Good, GoodDto>()
            //    .ForMember(
            //    dto=>dto.TypeName,
            //    opt=>opt.MapFrom(src=>src.TidNavigation.Name)
            //    );

            //CreateMap<GoodPropertyKey, GoodPropertyKeyDto>()
            //    .ForMember(
            //    dto => dto.GskName,
            //    opt => opt.MapFrom(src => src.Gsk.Name)
            //    );

            //CreateMap<GoodPropertyValue, GoodPropertyValueDto>()
            // .ForMember(
            // dto => dto.GsvValue,
            // opt => opt.MapFrom(src => src.Gsv.Value)
            // );


            //CreateMap<GoodDto, Good>();
            //CreateMap<GoodDto, Good>();


            //.ForMember(
            //    dest => dest.TypeName,
            //    opt => opt.MapFrom(src => src.TidNavigation.Name))
            //    //映射发生之前
            //    .BeforeMap((source, dto) => {
            //        //可以较为精确的控制输出数据格式
            //        dto.CreateTime = Convert.ToDateTime(source.CreateTime).ToString("yyyy-MM-dd");
            //    })
            //    //映射发生之后
            //    .AfterMap((source, dto) => {
            //        //code ...
            //    });

            //UserDto转UserEntity.
            //CreateMap();
        }

        public void SystemDto() {

            CreateMap<Menu, MenuDto>().ReverseMap();
        }

        public void MemberDto() { 
            CreateMap<Archive,ArchiveDto>().ReverseMap();
        }
    }
}
