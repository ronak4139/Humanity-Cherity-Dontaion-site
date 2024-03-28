// DonationController.cs
using Humanity.Data;
using Microsoft.AspNetCore.Mvc;
using System;

public class DonationController : Controller
{
    private readonly HumanityContext _context;

    public DonationController(HumanityContext context)
    {
        _context = context;
    }

    // GET: Donation/PaymentSuccess
    public IActionResult PaymentSuccess()
    {
        return View();
    }

    // POST: Donation/PaymentSuccess
    [HttpPost]
    public IActionResult PaymentSuccess(DonationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var donation = new Donation
            {
                Name = model.Name,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                DonationAmount = model.DonationAmount,
                OrderId = GenerateOrderId(), // Generate or retrieve order ID
                DonationDate = DateTime.Now
            };

            _context.Donations.Add(donation); // Add donation to the context
            _context.SaveChanges(); // Save changes to the database

            // Redirect to success page or perform other actions
            return RedirectToAction("PaymentSuccess");
        }

        // If model is not valid, add error messages to the model state
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            // Add each error message to the model state
            ModelState.AddModelError(string.Empty, error.ErrorMessage);
        }

        // Return to the donation page with errors
        return RedirectToAction("DonationNow", "UserControlller", model);
    }


    // Method to generate a unique order ID (you can implement your own logic)
    private string GenerateOrderId()
    {
        // Implement your logic to generate a unique order ID here
        return Guid.NewGuid().ToString();
    }
}
