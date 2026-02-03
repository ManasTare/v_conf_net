using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v_conf_net.Models;

[Table("user")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("add1")]
    [StringLength(255)]
    public string? Add1 { get; set; }

    [Column("add2")]
    [StringLength(255)]
    public string? Add2 { get; set; }

    [Column("auth_name")]
    [StringLength(255)]
    public string AuthName { get; set; } = null!;

    [Column("auth_tel")]
    [StringLength(255)]
    public string AuthTel { get; set; } = null!;

    [Column("cell")]
    [StringLength(255)]
    public string? Cell { get; set; }

    [Column("city")]
    [StringLength(255)]
    public string City { get; set; } = null!;

    [Column("company_name")]
    [StringLength(255)]
    public string CompanyName { get; set; } = null!;

    [Column("company_st_no")]
    [StringLength(255)]
    public string CompanyStNo { get; set; } = null!;

    [Column("company_vat_no")]
    [StringLength(255)]
    public string CompanyVatNo { get; set; } = null!;

    [Column("designation")]
    [StringLength(255)]
    public string Designation { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column("fax")]
    [StringLength(255)]
    public string? Fax { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string? Password { get; set; }

    [Column("pin")]
    [StringLength(255)]
    public string? Pin { get; set; }

    [Column("state")]
    [StringLength(255)]
    public string State { get; set; } = null!;

    [Column("tax_pan")]
    [StringLength(255)]
    public string? TaxPan { get; set; }

    [Column("tel")]
    [StringLength(255)]
    public string? Tel { get; set; }

    [Column("holding_type")]
    [StringLength(255)]
    public string? HoldingType { get; set; }

    [Column("phone")]
    [StringLength(255)]
    public string? Phone { get; set; }

    [Column("role")]
    [StringLength(255)]
    public string? Role { get; set; }

    [Column("username")]
    [StringLength(255)]
    public string? Username { get; set; }

    [Column("failed_attempts")]
    public int? FailedAttempts { get; set; }

    [Column("is_blocked", TypeName = "bit(1)")]
    public ulong? IsBlocked { get; set; }

    [Column("registration_no")]
    [StringLength(255)]
    public string? RegistrationNo { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<InvoiceHeader> InvoiceHeaders { get; set; } = new List<InvoiceHeader>();
}
