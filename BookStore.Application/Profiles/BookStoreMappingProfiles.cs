using AutoMapper;
using BookStore.Contracts.BookStoreIslemleri.Dtos;
using BookStore.Contracts.KullaniciIslemleri.Dtos;
using BookStore.Contracts.TenantIslemleri.Dtos;
using Shared.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Profiles;
public class BookStoreMappingProfiles : Profile
{
    public BookStoreMappingProfiles()
    {
        CreateMap<SaveTenantRequestDto, Tenant>().ReverseMap();
        CreateMap<SaveKullaniciRequestDto, Kullanici>()
        .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Aktif))
        .ReverseMap()
        .ForMember(dest => dest.Aktif, opt => opt.MapFrom(src => src.IsActive));

        CreateMap<SaveBookRequestDto, BookList>().ReverseMap();
        CreateMap<BookList, GetAllBookResponseDto>().ReverseMap();
        


    }
}
