
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
    
public partial class Company
{

    public Company()
    {

        this.CompanyOrder = new HashSet<CompanyOrder>();

        this.Enduser = new HashSet<Enduser>();

    }


    public decimal CompanyId { get; set; }

    public string Name { get; set; }

    public System.DateTime Created { get; set; }

    public string CreatedBy { get; set; }



    public virtual ICollection<CompanyOrder> CompanyOrder { get; set; }

    public virtual ICollection<Enduser> Enduser { get; set; }

}

}
