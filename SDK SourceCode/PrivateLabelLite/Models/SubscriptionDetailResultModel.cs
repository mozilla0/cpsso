using PrivateLabelLite.Entities.Subsciptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Heroic.AutoMapper;
using PrivateLabelLite.Entities;
using AutoMapper;

namespace PrivateLabelLite.Models
{
    public class SubscriptionDetailResultModel
    {
        public List<Dictionary<Guid, SubscriptionDetail>> Subscriptions { get; set; }
        public int totalPages { get; set; }
        public int totalRecords { get; set; }
    }
    public class SubscriptionSummaryModel
    {
        public List<SubscriptionDetailModel> SubscriptionList { get; set; }
        public List<CompanyDetail> Companies { get; set; }
        public CompanyOrderFilter Filter { get; set; }

    }

    public class SubscriptionDetailModel : IMapFrom<SubscriptionDetail>, IHaveCustomMappings
    {
        public string OrderNumber { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Article { get; set; }
        public string PaymentMethod { get; set; }
        public string EndCustomerName { get; set; }
        public string EndCustomerEmail { get; set; }
        public string Company { get; set; }
        public string OrderSource { get; set; }
        public string UnitPrice { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string LineStatus { get; set; }
        public List<SubscriptionHistory> SubscriptionHistory { get; set; }
        public AdditionalOrderLineData AdditionalData { get; set; }
        public int Totalrecords { get; set; }
        public Guid SubscriptionId { get; set; }
        public string MappingStatus { get; set; }
        public string PONumber { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? MarkUpPercentage { get; set; }
        public double? SalesPrice { get; set; }
        public double? SeatLimit { get; set; }
        public string TaxStatus { get; set; }
        public DateTime? SeatLimitStartTime { get; set; }
        public DateTime? SeatLimitEndTime { get; set; }
        public int? SeatCounter { get; set; }

        void IHaveCustomMappings.CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<SubscriptionDetailModel, SubscriptionDetail>()
                .ForMember(i => i.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(i => i.OrderNumber, opt => opt.MapFrom(src => src.OrderNumber))
                .ForMember(i => i.VendorId, opt => opt.MapFrom(src => src.VendorId))
                .ForMember(i => i.VendorName, opt => opt.MapFrom(src => src.VendorName))
                .ForMember(i => i.SKU, opt => opt.MapFrom(src => src.SKU))
                .ForMember(i => i.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(i => i.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(i => i.EndCustomerName, opt => opt.MapFrom(src => src.EndCustomerName))
                .ForMember(i => i.EndCustomerEmail, opt => opt.MapFrom(src => src.EndCustomerEmail))
                .ForMember(i => i.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(i => i.OrderSource, opt => opt.MapFrom(src => src.OrderSource))
                .ForMember(i => i.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(i => i.CurrencyCode, opt => opt.MapFrom(src => src.CurrencyCode))
                .ForMember(i => i.CurrencySymbol, opt => opt.MapFrom(src => src.CurrencySymbol))
                .ForMember(i => i.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(i => i.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(i => i.LineStatus, opt => opt.MapFrom(src => src.LineStatus))
                .ForMember(i => i.TotalRecords, opt => opt.MapFrom(src => src.Totalrecords))
                .ForMember(i => i.SubscriptionId, opt => opt.MapFrom(src => src.SubscriptionId))
                .ForMember(i => i.MappingStatus, opt => opt.MapFrom(src => src.MappingStatus))
                .ForMember(i => i.PONumber, opt => opt.MapFrom(src => src.PONumber))
                .ForMember(i => i.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
                .ForMember(i => i.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(i => i.MarkUpPercentage, opt => opt.MapFrom(src => src.MarkUpPercentage))
                .ForMember(i => i.SalesPrice, opt => opt.MapFrom(src => src.SalesPrice))
                .ForMember(i => i.SeatLimit, opt => opt.MapFrom(src => src.SeatLimit))
                .ForMember(i => i.TaxStatus, opt => opt.MapFrom(src => src.TaxStatus))
                .ForMember(i => i.SeatCounter, opt => opt.MapFrom(src => src.SeatCounter))
                .ForMember(i => i.SeatLimitStartTime, opt => opt.MapFrom(src => src.SeatLimitStartTime))
                .ForMember(i => i.SeatLimitEndTime, opt => opt.MapFrom(src => src.SeatLimitEndTime))
                .ForMember(i => i.SubscriptionHistory, opt => opt.MapFrom(src => src.SubscriptionHistory))
                .ForMember(i => i.AdditionalData, opt => opt.MapFrom(src => src.AdditionalData));

        }
    }
}