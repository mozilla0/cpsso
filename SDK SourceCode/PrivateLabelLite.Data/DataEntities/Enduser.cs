
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
    
public partial class Enduser
{

    public decimal EnduserId { get; set; }

    public Nullable<System.Guid> SAPEnduserId { get; set; }

    public Nullable<decimal> CompanyId { get; set; }

    public Nullable<System.DateTime> Created { get; set; }

    public string CreatedBy { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }



    public virtual Company Company { get; set; }

}

}