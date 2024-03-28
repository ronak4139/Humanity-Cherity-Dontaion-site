using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Humanity.Models
{
	public class AdminModel
	{
		SqlConnection con = new SqlConnection(@"Server=RONAK;Database=Humanity;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
		
		[Required(ErrorMessage = "Please enter Email")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "please enter Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool getadmin(AdminModel admin)
		{
			SqlCommand cmd = new SqlCommand("SELECT Email,Password FROM dbo.Admin WHERE Email=@em AND Password=@pw", con);
			cmd.Parameters.AddWithValue("@em", admin.Email);
			cmd.Parameters.AddWithValue("@pw", admin.Password);

			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();

			if (reader.Read())
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
