using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v_conf_net.Models;

[Table("invoice_detail")]
[Index("CompId", Name = "id_fk_component")]
[Index("InvId", Name = "id_fk_invoice")]
public partial class InvoiceDetail
{
    [Key]
    [Column("inv_dtl_id")]
    public int InvDtlId { get; set; }

    [Column("inv_id")]
    public int InvId { get; set; }

    [Column("comp_id")]
    public int CompId { get; set; }

    [Column("comp_price")]
    public double CompPrice { get; set; }

    [ForeignKey("CompId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Component Comp { get; set; } = null!;

    [ForeignKey("InvId")]
    [InverseProperty("InvoiceDetails")]
    public virtual InvoiceHeader Inv { get; set; } = null!;
}
