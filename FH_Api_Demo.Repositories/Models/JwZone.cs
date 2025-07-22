using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FH_Api_Demo.Repositories.Models;

[Table("jw_zone")]
public partial class JwZone
{
    [Key]
    [Column("zone_id")]
    [StringLength(15)]
    public string ZoneId { get; set; } = null!;

    [Column("source")]
    [StringLength(3)]
    public string? Source { get; set; }

    [Column("source_value")]
    [StringLength(25)]
    public string? SourceValue { get; set; }

    [Column("zone_name")]
    [StringLength(128)]
    public string? ZoneName { get; set; }

    [Column("table_id")]
    [StringLength(128)]
    public string? TableId { get; set; }

    [Column("query_id")]
    [StringLength(15)]
    public string? QueryId { get; set; }

    [Column("zone_type")]
    [StringLength(2)]
    public string? ZoneType { get; set; }

    [Column("default_mode")]
    [StringLength(1)]
    public string? DefaultMode { get; set; }

    [Column("zone_width")]
    public int? ZoneWidth { get; set; }

    [Column("zone_height")]
    public int? ZoneHeight { get; set; }

    [Column("zone_layout")]
    [StringLength(4000)]
    public string? ZoneLayout { get; set; }

    [Column("zone_script")]
    [StringLength(4000)]
    public string? ZoneScript { get; set; }

    [Column("group_list1")]
    [StringLength(1024)]
    public string? GroupList1 { get; set; }

    [Column("group_list2")]
    [StringLength(1024)]
    public string? GroupList2 { get; set; }

    [Column("menu_type")]
    [StringLength(1)]
    public string? MenuType { get; set; }

    [Column("published_flag")]
    [StringLength(1)]
    public string PublishedFlag { get; set; } = null!;

    [Column("checkout_flag")]
    [StringLength(1)]
    public string CheckoutFlag { get; set; } = null!;

    [Column("checkout_user")]
    [StringLength(15)]
    public string? CheckoutUser { get; set; }

    [Column("checkout_id")]
    [StringLength(15)]
    public string? CheckoutId { get; set; }

    [Column("html_id")]
    [StringLength(255)]
    public string? HtmlId { get; set; }

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

    [InverseProperty("Zone")]
    public virtual ICollection<JwItem> JwItems { get; set; } = new List<JwItem>();

    [ForeignKey("QueryId")]
    [InverseProperty("JwZones")]
    public virtual JwQuery? Query { get; set; }

    [ForeignKey("TableId")]
    [InverseProperty("JwZones")]
    public virtual JwTable? Table { get; set; }
}
