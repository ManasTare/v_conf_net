using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v_conf_net.Models;

[Table("model")]
[Index("MfgId", Name = "mfg_id")]
[Index("SegId", Name = "seg_id")]
public partial class Model
{
    [Key]
    [Column("model_id")]
    public int ModelId { get; set; }

    [Column("model_name")]
    [StringLength(255)]
    public string? ModelName { get; set; }

    [Column("mfg_id")]
    public int? MfgId { get; set; }

    [Column("seg_id")]
    public int? SegId { get; set; }

    [Column("min_qty")]
    public int? MinQty { get; set; }

    [Column("price")]
    public double Price { get; set; }

    [Column("img_path")]
    [StringLength(255)]
    public string? ImgPath { get; set; }

    [InverseProperty("Model")]
    public virtual ICollection<AlternateComponentMaster> AlternateComponentMasters { get; set; } = new List<AlternateComponentMaster>();

    [InverseProperty("Model")]
    public virtual ICollection<InvoiceHeader> InvoiceHeaders { get; set; } = new List<InvoiceHeader>();

    [ForeignKey("MfgId")]
    [InverseProperty("Models")]
    public virtual Manufacturer? Mfg { get; set; }

    [ForeignKey("SegId")]
    [InverseProperty("Models")]
    public virtual Segment? Seg { get; set; }

    [InverseProperty("Model")]
    public virtual ICollection<VehicleDefaultConfig> VehicleDefaultConfigs { get; set; } = new List<VehicleDefaultConfig>();

    [InverseProperty("Model")]
    public virtual ICollection<VehicleDetail> VehicleDetails { get; set; } = new List<VehicleDetail>();
}
