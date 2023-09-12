using AutoMapper;
using HomeApiRestful.Contracts.Home;
using HomeApiRestful.Models;

namespace HomeApiRestful.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressInfo>();
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(m => m.AddressInfo,
                    opt => opt.MapFrom(src => src.Address));
        }

    }
}
