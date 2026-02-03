using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v_conf_net.Models;

[Table("manufacturer")]
public partial class Manufacturer
{
    [Key]
    [Column("mfg_id")]
    public int MfgId { get; set; }

    [Column("mfg_name")]
    [StringLength(255)]
    public string? MfgName { get; set; }

    [InverseProperty("Mfg")]
    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    [InverseProperty("Mfg")]
    public virtual ICollection<SgMfgMaster> SgMfgMasters { get; set; } = new List<SgMfgMaster>();
}
