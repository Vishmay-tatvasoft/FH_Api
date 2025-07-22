using System.ComponentModel.DataAnnotations;

namespace FH_Api_Demo.Services.ViewModels;

public class SignUpVM
{
    [StringLength(30)]
    public string EmailAddress { get; set; } = null!;
    [StringLength(15)]
    public string RoleId { get; set; } = null!;
    [StringLength(25)]
    public string LastName { get; set; } = null!;
    [StringLength(25)]
    public string FirstName { get; set; } = null!;
    [StringLength(15)]
    public string UserName { get; set; } = null!;
}
