using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FH_Api_Demo.Repositories.Models;

[Table("jw_field")]
public partial class JwField
{
    [Key]
    [Column("field_id")]
    [StringLength(255)]
    public string FieldId { get; set; } = null!;

    [Column("source")]
    [StringLength(3)]
    public string? Source { get; set; }

    [Column("table_id")]
    [StringLength(128)]
    public string? TableId { get; set; }

    [Column("field_name")]
    [StringLength(128)]
    public string? FieldName { get; set; }

    [Column("field_description")]
    [StringLength(128)]
    public string? FieldDescription { get; set; }

    [Column("data_type")]
    [StringLength(256)]
    public string? DataType { get; set; }

    [Column("required_flag")]
    [StringLength(1)]
    public string? RequiredFlag { get; set; }

    [Column("max_length")]
    public int? MaxLength { get; set; }

    [Column("num_precision")]
    public short? NumPrecision { get; set; }

    [Column("num_scale")]
    public int? NumScale { get; set; }

    [Column("pk")]
    [StringLength(2)]
    public string? Pk { get; set; }

    [Column("fk")]
    [StringLength(514)]
    public string? Fk { get; set; }

    [Column("ordinal_position")]
    public int? OrdinalPosition { get; set; }

    [Column("net_type")]
    [StringLength(25)]
    public string? NetType { get; set; }

    [Column("display_field")]
    [StringLength(128)]
    public string? DisplayField { get; set; }

    [Column("label_text")]
    [StringLength(128)]
    public string? LabelText { get; set; }

    [Column("label_cssclass")]
    [StringLength(128)]
    public string? LabelCssclass { get; set; }

    [Column("label_style")]
    [StringLength(255)]
    public string? LabelStyle { get; set; }

    [Column("label_script")]
    [StringLength(512)]
    public string? LabelScript { get; set; }

    [Column("heading_text")]
    [StringLength(128)]
    public string? HeadingText { get; set; }

    [Column("heading_cssclass")]
    [StringLength(128)]
    public string? HeadingCssclass { get; set; }

    [Column("heading_style")]
    [StringLength(255)]
    public string? HeadingStyle { get; set; }

    [Column("heading_script")]
    [StringLength(512)]
    public string? HeadingScript { get; set; }

    [Column("tooltip")]
    [StringLength(2000)]
    public string? Tooltip { get; set; }

    [Column("edit_type")]
    [StringLength(25)]
    public string? EditType { get; set; }

    [Column("mask_type")]
    [StringLength(25)]
    public string? MaskType { get; set; }

    [Column("list_id")]
    [StringLength(15)]
    public string? ListId { get; set; }

    [Column("default_value")]
    [StringLength(512)]
    public string? DefaultValue { get; set; }

    [Column("edit_cssclass")]
    [StringLength(128)]
    public string? EditCssclass { get; set; }

    [Column("edit_style")]
    [StringLength(255)]
    public string? EditStyle { get; set; }

    [Column("edit_script")]
    [StringLength(512)]
    public string? EditScript { get; set; }

    [Column("map_id")]
    [StringLength(255)]
    public string? MapId { get; set; }

    [Column("display_rule_flag")]
    [StringLength(1)]
    public string DisplayRuleFlag { get; set; } = null!;

    [Column("checkout_flag")]
    [StringLength(1)]
    public string CheckoutFlag { get; set; } = null!;

    [Column("checkout_user")]
    [StringLength(15)]
    public string? CheckoutUser { get; set; }

    [Column("checkout_id")]
    [StringLength(255)]
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

    [InverseProperty("Field")]
    public virtual ICollection<JwItem> JwItems { get; set; } = new List<JwItem>();

    [ForeignKey("ListId")]
    [InverseProperty("JwFields")]
    public virtual JwList? List { get; set; }

    [ForeignKey("TableId")]
    [InverseProperty("JwFields")]
    public virtual JwTable? Table { get; set; }
}
