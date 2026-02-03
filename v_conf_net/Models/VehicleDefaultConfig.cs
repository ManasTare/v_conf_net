using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v_conf_net.Models;

[Table("vehicle_default_config")]
[Index("CompId", Name = "vdc_fk_component")]
[Index("ModelId", Name = "vdc_fk_model")]
public partial class VehicleDefaultConfig
{
    [Key]
    [Column("config_id")]
    public int ConfigId { get; set; }

    [Column("model_id")]
    public int ModelId { get; set; }

    [Column("comp_id")]
    public int CompId { get; set; }

    [ForeignKey("CompId")]
    [InverseProperty("VehicleDefaultConfigs")]
    public virtual Component Comp { get; set; } = null!;

    [ForeignKey("ModelId")]
    [InverseProperty("VehicleDefaultConfigs")]
    public virtual Model Model { get; set; } = null!;
}
