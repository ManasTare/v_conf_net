using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v_conf_net.Models;

[Table("alternate_component_master")]
[Index("AltCompId", Name = "acm_fk_alt_comp")]
[Index("CompId", Name = "acm_fk_comp")]
[Index("ModelId", Name = "acm_fk_model")]
public partial class AlternateComponentMaster
{
    [Key]
    [Column("alt_id")]
    public int AltId { get; set; }

    [Column("model_id")]
    public int ModelId { get; set; }

    [Column("comp_id")]
    public int CompId { get; set; }

    [Column("alt_comp_id")]
    public int AltCompId { get; set; }

    [Column("delta_price")]
    public double DeltaPrice { get; set; }

    [ForeignKey("AltCompId")]
    [InverseProperty("AlternateComponentMasterAltComps")]
    public virtual Component AltComp { get; set; } = null!;

    [ForeignKey("CompId")]
    [InverseProperty("AlternateComponentMasterComps")]
    public virtual Component Comp { get; set; } = null!;

    [ForeignKey("ModelId")]
    [InverseProperty("AlternateComponentMasters")]
    public virtual Model Model { get; set; } = null!;
}
