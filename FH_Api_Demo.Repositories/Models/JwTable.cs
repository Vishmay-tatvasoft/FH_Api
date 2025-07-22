using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FH_Api_Demo.Repositories.Models;

[Table("jw_table")]
public partial class JwTable
{
    [Key]
    [Column("table_id")]
    [StringLength(128)]
    public string TableId { get; set; } = null!;

    [Column("source")]
    [StringLength(3)]
    public string? Source { get; set; }

    [Column("table_name")]
    [StringLength(128)]
    public string? TableName { get; set; }

    [Column("table_description")]
    [StringLength(128)]
    public string? TableDescription { get; set; }

    [Column("pk1_column")]
    [StringLength(128)]
    public string? Pk1Column { get; set; }

    [Column("pk2_column")]
    [StringLength(128)]
    public string? Pk2Column { get; set; }

    [Column("pk3_column")]
    [StringLength(128)]
    public string? Pk3Column { get; set; }

    [Column("pk4_column")]
    [StringLength(128)]
    public string? Pk4Column { get; set; }

    [Column("pk5_column")]
    [StringLength(128)]
    public string? Pk5Column { get; set; }

    [Column("pk_count")]
    public int? PkCount { get; set; }

    [Column("pk_constraint")]
    [StringLength(128)]
    public string? PkConstraint { get; set; }

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

    [InverseProperty("Table")]
    public virtual ICollection<JwField> JwFields { get; set; } = new List<JwField>();

    [InverseProperty("Table")]
    public virtual ICollection<JwZone> JwZones { get; set; } = new List<JwZone>();
}
