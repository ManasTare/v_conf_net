using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v_conf_net.Models;

[Table("vehicle_detail")]
[Index("CompId", Name = "idx_vehicle_detail_comp")]
[Index("ModelId", Name = "idx_vehicle_detail_model")]
public partial class VehicleDetail
{
    [Key]
    [Column("config_id")]
    public int ConfigId { get; set; }

    [Column("comp_type")]
    [StringLength(255)]
    public string? CompType { get; set; }

    [Column("is_config")]
    [StringLength(255)]
    public string? IsConfig { get; set; }

    [Column("comp_id")]
    public int? CompId { get; set; }

    [Column("model_id")]
    public int? ModelId { get; set; }

    [ForeignKey("CompId")]
    [InverseProperty("VehicleDetails")]
    public virtual Component? Comp { get; set; }

    [ForeignKey("ModelId")]
    [InverseProperty("VehicleDetails")]
    public virtual Model? Model { get; set; }
}
