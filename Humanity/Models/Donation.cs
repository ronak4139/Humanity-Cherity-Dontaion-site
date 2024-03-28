// Donation.cs
using System;

public class Donation
{
    public int Id { get; set; } // Primary key
    public string Name { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public decimal DonationAmount { get; set; }
    public string OrderId { get; set; }
    public DateTime DonationDate { get; set; }
}
