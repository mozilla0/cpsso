
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace PrivateLabelLite.Data.DataEntities
{

using System;
    using System.Collections.Generic;
    
public partial class OrderLine
{

    public decimal LineId { get; set; }

    public string OrderNumber { get; set; }

    public string Sku { get; set; }

    public string SkuName { get; set; }

    public string ManufacturerPartNumber { get; set; }

    public Nullable<decimal> UnitPrice { get; set; }

    public string Quantity { get; set; }

    public string LineStatus { get; set; }

    public string CurrencySymbol { get; set; }

    public string CurrencyCode { get; set; }



    public virtual OrderHeader OrderHeader { get; set; }

}

}
