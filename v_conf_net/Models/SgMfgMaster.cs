using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v_conf_net.Models;

[Table("sg_mfg_master")]
[Index("MfgId", Name = "mfg_id")]
[Index("SegId", Name = "seg_id")]
public partial class SgMfgMaster
{
    [Key]
    [Column("sgmf_id")]
    public int SgmfId { get; set; }

    [Column("mfg_id")]
    public int? MfgId { get; set; }

    [Column("seg_id")]
    public int? SegId { get; set; }

    [ForeignKey("MfgId")]
    [InverseProperty("SgMfgMasters")]
    public virtual Manufacturer? Mfg { get; set; }

    [ForeignKey("SegId")]
    [InverseProperty("SgMfgMasters")]
    public virtual Segment? Seg { get; set; }
}
