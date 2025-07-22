using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FH_Api_Demo.Repositories.Models;

[Table("jw_query")]
public partial class JwQuery
{
    [Key]
    [Column("query_id")]
    [StringLength(15)]
    public string QueryId { get; set; } = null!;

    [Column("parent_id")]
    [StringLength(15)]
    public string? ParentId { get; set; }

    [Column("source")]
    [StringLength(3)]
    public string? Source { get; set; }

    [Column("query_name")]
    [StringLength(128)]
    public string? QueryName { get; set; }

    [Column("query_sql")]
    public string? QuerySql { get; set; }

    [Column("query_parms")]
    [StringLength(4000)]
    public string? QueryParms { get; set; }

    [Column("query_sort_column")]
    [StringLength(255)]
    public string? QuerySortColumn { get; set; }

    [Column("query_sort_direction")]
    [StringLength(5)]
    public string? QuerySortDirection { get; set; }

    [Column("use_type")]
    [StringLength(1)]
    public string UseType { get; set; } = null!;

    [Column("display_count")]
    public int DisplayCount { get; set; }

    [Column("checkout_flag")]
    [StringLength(1)]
    public string CheckoutFlag { get; set; } = null!;

    [Column("checkout_user")]
    [StringLength(15)]
    public string? CheckoutUser { get; set; }

    [Column("checkout_id")]
    [StringLength(15)]
    public string? CheckoutId { get; set; }

    [Column("enabled_flag")]
    [StringLength(1)]
    public string EnabledFlag { get; set; } = null!;

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

    [InverseProperty("Query")]
    public virtual ICollection<JwItem> JwItems { get; set; } = new List<JwItem>();

    [InverseProperty("Query")]
    public virtual ICollection<JwZone> JwZones { get; set; } = new List<JwZone>();
}
