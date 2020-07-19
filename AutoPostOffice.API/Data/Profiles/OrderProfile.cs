using AutoMapper;
using AutoPostOffice.API.Data.Entities;
using AutoPostOffice.API.Data.Repositories;
using AutoPostOffice.API.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPostOffice.API.Data.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderModel>()
                .ForMember(
                    d => d.Status,
                    o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(
                    d => d.RecipientFullName,
                    o => o.MapFrom(s => $"{s.RecipientLastName} {s.RecipientFirstName} {s.RecipientMiddleName}".Trim()));

            CreateMap<OrderCreationModel, Order>();
            CreateMap<OrderAlterModel, Order>();
            CreateMap<Order, OrderTracking>();


            CreateMap<OrderTracking, OrderModel>()
                .ForMember(
                    d => d.Status,
                    o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(
                    d => d.RecipientFullName,
                    o => o.MapFrom(s => $"{s.RecipientLastName} {s.RecipientFirstName} {s.RecipientMiddleName}".Trim()))
                .ForMember(
                    d => d.OrderNumber,
                    o => o.MapFrom(s => s.OrderNumber));

            CreateMap<OrderTracking, OrderTrackingModel>()
                .ForMember(
                    d => d.ChangeType,
                    o => o.MapFrom(s => s.ChangeType.ToString()));
        }
    }
}
