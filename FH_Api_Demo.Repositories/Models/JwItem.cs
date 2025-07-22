using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FH_Api_Demo.Repositories.Models;

[Table("jw_item")]
public partial class JwItem
{
    [Key]
    [Column("item_id")]
    [StringLength(15)]
    public string ItemId { get; set; } = null!;

    [Column("zone_id")]
    [StringLength(15)]
    public string? ZoneId { get; set; }

    [Column("field_id")]
    [StringLength(255)]
    public string? FieldId { get; set; }

    [Column("item_type")]
    [StringLength(1)]
    public string? ItemType { get; set; }

    [Column("required_flag")]
    [StringLength(1)]
    public string? RequiredFlag { get; set; }

    [Column("display_flag")]
    [StringLength(1)]
    public string? DisplayFlag { get; set; }

    [Column("edit_flag")]
    [StringLength(1)]
    public string? EditFlag { get; set; }

    [Column("sort_flag")]
    [StringLength(1)]
    public string? SortFlag { get; set; }

    [Column("sort_columns")]
    [StringLength(2000)]
    public string? SortColumns { get; set; }

    [Column("group1")]
    [StringLength(25)]
    public string? Group1 { get; set; }

    [Column("group2")]
    [StringLength(25)]
    public string? Group2 { get; set; }

    [Column("section")]
    [Precision(9, 0)]
    public decimal? Section { get; set; }

    [Column("row")]
    [Precision(9, 0)]
    public decimal? Row { get; set; }

    [Column("col")]
    [Precision(9, 0)]
    public decimal? Col { get; set; }

    [Column("pos")]
    [Precision(9, 0)]
    public decimal? Pos { get; set; }

    [Column("parent_id")]
    [StringLength(15)]
    public string? ParentId { get; set; }

    [Column("display_field")]
    [StringLength(128)]
    public string? DisplayField { get; set; }

    [Column("label_name")]
    [StringLength(128)]
    public string? LabelName { get; set; }

    [Column("label_cssclass")]
    [StringLength(128)]
    public string? LabelCssclass { get; set; }

    [Column("label_style")]
    [StringLength(255)]
    public string? LabelStyle { get; set; }

    [Column("label_script")]
    [StringLength(4000)]
    public string? LabelScript { get; set; }

    [Column("label_width")]
    public int? LabelWidth { get; set; }

    [Column("label_height")]
    public int? LabelHeight { get; set; }

    [Column("label_attributes")]
    [StringLength(4000)]
    public string? LabelAttributes { get; set; }

    [Column("tooltip")]
    [StringLength(2000)]
    public string? Tooltip { get; set; }

    [Column("value_text")]
    [StringLength(2000)]
    public string? ValueText { get; set; }

    [Column("value_cssclass")]
    [StringLength(128)]
    public string? ValueCssclass { get; set; }

    [Column("value_style")]
    [StringLength(255)]
    public string? ValueStyle { get; set; }

    [Column("value_script")]
    [StringLength(4000)]
    public string? ValueScript { get; set; }

    [Column("value_width")]
    public int? ValueWidth { get; set; }

    [Column("value_height")]
    public int? ValueHeight { get; set; }

    [Column("value_attributes")]
    [StringLength(4000)]
    public string? ValueAttributes { get; set; }

    [Column("value_parms")]
    [StringLength(4000)]
    public string? ValueParms { get; set; }

    [Column("edit_type")]
    [StringLength(25)]
    public string? EditType { get; set; }

    [Column("mask_type")]
    [StringLength(25)]
    public string? MaskType { get; set; }

    [Column("list_id")]
    [StringLength(15)]
    public string? ListId { get; set; }

    [Column("empty_value_text")]
    [StringLength(255)]
    public string? EmptyValueText { get; set; }

    [Column("default_value")]
    [StringLength(512)]
    public string? DefaultValue { get; set; }

    [Column("edit_instructions")]
    [StringLength(4000)]
    public string? EditInstructions { get; set; }

    [Column("view_instructions")]
    [StringLength(4000)]
    public string? ViewInstructions { get; set; }

    [Column("search_operator")]
    [StringLength(15)]
    public string? SearchOperator { get; set; }

    [Column("value_action")]
    [StringLength(2000)]
    public string? ValueAction { get; set; }

    [Column("target")]
    [StringLength(255)]
    public string? Target { get; set; }

    [Column("max_length")]
    public int? MaxLength { get; set; }

    [Column("query_id")]
    [StringLength(15)]
    public string? QueryId { get; set; }

    [Column("page_id")]
    [StringLength(255)]
    public string? PageId { get; set; }

    [Column("display_rule_text")]
    [StringLength(4000)]
    public string? DisplayRuleText { get; set; }

    [Column("validation_rule_text")]
    [StringLength(4000)]
    public string? ValidationRuleText { get; set; }

    [Column("workflow_rules")]
    [StringLength(1000)]
    public string? WorkflowRules { get; set; }

    [Column("default_view_flag")]
    [StringLength(1)]
    public string DefaultViewFlag { get; set; } = null!;

    [Column("default_edit_flag")]
    [StringLength(1)]
    public string DefaultEditFlag { get; set; } = null!;

    [Column("vor_view_flag")]
    [StringLength(255)]
    public string VorViewFlag { get; set; } = null!;

    [Column("vor_edit_flag")]
    [StringLength(255)]
    public string VorEditFlag { get; set; } = null!;

    [Column("workbench_flag")]
    [StringLength(1)]
    public string WorkbenchFlag { get; set; } = null!;

    [Column("html_id")]
    [StringLength(255)]
    public string? HtmlId { get; set; }

    [Column("enabled_flag")]
    [StringLength(1)]
    public string EnabledFlag { get; set; } = null!;

    [Column("sb_action_hint")]
    [StringLength(255)]
    public string? SbActionHint { get; set; }

    [Column("exago_report_id")]
    [StringLength(15)]
    public string? ExagoReportId { get; set; }

    [Column("exage_field_name")]
    [StringLength(255)]
    public string? ExageFieldName { get; set; }

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

    [Column("standard_id")]
    [StringLength(15)]
    public string? StandardId { get; set; }

    [Column("page_access_flag")]
    [StringLength(1)]
    public string PageAccessFlag { get; set; } = null!;

    [Column("checkout_id")]
    [StringLength(15)]
    public string? CheckoutId { get; set; }

    [ForeignKey("FieldId")]
    [InverseProperty("JwItems")]
    public virtual JwField? Field { get; set; }

    [ForeignKey("ListId")]
    [InverseProperty("JwItems")]
    public virtual JwList? List { get; set; }

    [ForeignKey("QueryId")]
    [InverseProperty("JwItems")]
    public virtual JwQuery? Query { get; set; }

    [ForeignKey("ZoneId")]
    [InverseProperty("JwItems")]
    public virtual JwZone? Zone { get; set; }
}
