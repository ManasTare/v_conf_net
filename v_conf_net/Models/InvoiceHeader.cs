using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v_conf_net.Models;

[Table("invoice_header")]
[Index("ModelId", Name = "ih_fk_model")]
[Index("UserId", Name = "ih_fk_user")]
public partial class InvoiceHeader
{
    [Key]
    [Column("inv_id")]
    public int InvId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("model_id")]
    public int ModelId { get; set; }

    [Column("qty")]
    public int Qty { get; set; }

    [Column("base_amt")]
    public double BaseAmt { get; set; }

    [Column("tax")]
    public double Tax { get; set; }

    [Column("total_amt")]
    public double TotalAmt { get; set; }

    [Column("inv_date")]
    public DateOnly? InvDate { get; set; }

    [Column("status", TypeName = "enum('Pending','Confirmed','Cancelled')")]
    public string? Status { get; set; }

    [Column("customer_detail")]
    [StringLength(255)]
    public string? CustomerDetail { get; set; }

    [InverseProperty("Inv")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [ForeignKey("ModelId")]
    [InverseProperty("InvoiceHeaders")]
    public virtual Model Model { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("InvoiceHeaders")]
    public virtual User User { get; set; } = null!;
}
