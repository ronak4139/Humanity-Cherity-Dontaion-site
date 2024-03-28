using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Humanity.Models
{
    public class ContactusDataModel
    {
        SqlConnection con = new SqlConnection(@"Server=RONAK;Database=Humanity;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		//insert contactus datamodel
		[Required(ErrorMessage = "Please enter Name")]
        public string contactusName { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress]
        public string contactusEmail { get; set; }

        [Required(ErrorMessage = "Please enter Phone Number")]
        public string contactusPhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter Message")]
        public string contactusMessage { get; set; }

        //Insert a record into a database table
        public bool insertcontactus(ContactusDataModel usr)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Contactus VALUES (@name,@phon,@email,@msg);", con);

            cmd.Parameters.AddWithValue("@name", usr.contactusName);
            cmd.Parameters.AddWithValue("@phon", usr.contactusPhoneNumber);
            cmd.Parameters.AddWithValue("@email", usr.contactusEmail);
            cmd.Parameters.AddWithValue("@msg", usr.contactusMessage);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }            
            return false;
        }

		public List<ContactusDataModel> getDatacontactus()
		{
			List<ContactusDataModel> lstEmp = new List<ContactusDataModel>();
			SqlDataAdapter apt = new SqlDataAdapter("SELECT * FROM dbo.Contactus", con);
			DataSet ds = new DataSet();
			apt.Fill(ds);
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				lstEmp.Add(new ContactusDataModel
				{
                    Id = Convert.ToInt32(dr["Id"].ToString()),
					contactusName = dr["Name"].ToString(),
					contactusPhoneNumber = dr["Phonenumber"].ToString(),
					contactusEmail = dr["Email"].ToString(),
					contactusMessage = dr["Message"].ToString()
				});
			}
			return lstEmp;
		}
	}
}
