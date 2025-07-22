using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FH_Api_Demo.Repositories.Models;

[Table("jw_list")]
public partial class JwList
{
    [Key]
    [Column("list_id")]
    [StringLength(15)]
    public string ListId { get; set; } = null!;

    [Column("parent_id")]
    [StringLength(15)]
    public string? ParentId { get; set; }

    [Column("source")]
    [StringLength(3)]
    public string? Source { get; set; }

    [Column("list_name")]
    [StringLength(128)]
    public string? ListName { get; set; }

    [Column("list_type")]
    [StringLength(1)]
    public string? ListType { get; set; }

    [Column("list_parms")]
    [StringLength(4000)]
    public string? ListParms { get; set; }

    [Column("list_sql")]
    [StringLength(4000)]
    public string? ListSql { get; set; }

    [Column("list_options")]
    [StringLength(8000)]
    public string? ListOptions { get; set; }

    [Column("checkout_flag")]
    [StringLength(1)]
    public string CheckoutFlag { get; set; } = null!;

    [Column("checkout_user")]
    [StringLength(15)]
    public string? CheckoutUser { get; set; }

    [Column("checkout_id")]
    [StringLength(15)]
    public string? CheckoutId { get; set; }

    [Column("add_date", TypeName = "timestamp without time zone")]
    public DateTime? AddDate { get; set; }

    [Column("add_user")]
    [StringLength(15)]
    public string? AddUser { get; set; }

    [Column("update_date", TypeName = "timestamp without time zone")]
    public DateTime? UpdateDate { get; set; }

    [Column("update_user")]
    [StringLength(15)]
    public string? UpdateUser { get; set; }

    [InverseProperty("List")]
    public virtual ICollection<JwField> JwFields { get; set; } = new List<JwField>();

    [InverseProperty("List")]
    public virtual ICollection<JwItem> JwItems { get; set; } = new List<JwItem>();
}
