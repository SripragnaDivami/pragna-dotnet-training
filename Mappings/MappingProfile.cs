
using AutoMapper;
using capstone_policy_management.DTOs;
using capstone_policy_management.DTOs.PolicyEnrollmentDTOs;
using capstone_policy_management.Entities;

namespace capstone_policy_management.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        
            CreateMap<PolicyEnrollment, PolicyEnrollmentResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : string.Empty))
                .ForMember(dest => dest.PolicyName, opt => opt.MapFrom(src => src.Policy != null ? src.Policy.Name : string.Empty));
            
            CreateMap<PolicyEnrollmentCreateDto, PolicyEnrollment>();
            CreateMap<PolicyEnrollmentUpdateDto, PolicyEnrollment>();
        }
    }
}
