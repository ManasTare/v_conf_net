using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v_conf_net.Models;

[Table("component")]
public partial class Component
{
    [Key]
    [Column("comp_id")]
    public int CompId { get; set; }

    [Column("comp_name")]
    [StringLength(255)]
    public string? CompName { get; set; }

    [Column("comp_type")]
    [StringLength(255)]
    public string? CompType { get; set; }

    [Column("price")]
    public double? Price { get; set; }

    [InverseProperty("AltComp")]
    public virtual ICollection<AlternateComponentMaster> AlternateComponentMasterAltComps { get; set; } = new List<AlternateComponentMaster>();

    [InverseProperty("Comp")]
    public virtual ICollection<AlternateComponentMaster> AlternateComponentMasterComps { get; set; } = new List<AlternateComponentMaster>();

    [InverseProperty("Comp")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [InverseProperty("Comp")]
    public virtual ICollection<VehicleDefaultConfig> VehicleDefaultConfigs { get; set; } = new List<VehicleDefaultConfig>();

    [InverseProperty("Comp")]
    public virtual ICollection<VehicleDetail> VehicleDetails { get; set; } = new List<VehicleDetail>();
}
