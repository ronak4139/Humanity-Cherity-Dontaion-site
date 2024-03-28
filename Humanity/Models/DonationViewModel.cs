// DonationViewModel.cs
using System.ComponentModel.DataAnnotations;

public class DonationViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string MobileNumber { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Donation amount must be greater than zero")]
    public decimal DonationAmount { get; set; }
}
