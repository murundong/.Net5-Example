using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreEntityFramework
{
    public class MapperConfigProfile : Profile
    {

        Func<DateTime?, string> ConvertTimeToString => 
            s =>
         {
             if (s.HasValue) return Convert.ToDateTime(s).ToString("yyyy-MM-dd HH:mm:ss");
             return null;
         };
        public MapperConfigProfile()
        {
            //CreateMap<A, B>()
            //   .ForMember(des => des.date_time, opt => opt.MapFrom(src => ConvertTimeToString(src.date_time)));

        }
    }
}
