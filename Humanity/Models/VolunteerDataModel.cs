using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Humanity.Models
{
    public class VolunteerDataModel
    {
        SqlConnection con = new SqlConnection(@"Server=RONAK;Database=Humanity;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

        //insert contactus datamodel
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Name")]
        public string volunteername { get; set; }
        
        [Required(ErrorMessage = "Please enter Phone Number")]
        public string volunteernumber { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress]
        public string volunteeremail { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string volunteeraddress { get; set; }

        [Required(ErrorMessage = "Please enter about section")]
        public string volunteerabout { get; set; }

        //Insert a record into a database table
        public bool insertvolunteer(VolunteerDataModel vlt)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Volunteer VALUES (@name,@phon,@email,@adr,@abt);", con);

            cmd.Parameters.AddWithValue("@name", vlt.volunteername);
            cmd.Parameters.AddWithValue("@phon", vlt.volunteernumber);
            cmd.Parameters.AddWithValue("@email", vlt.volunteeremail);
            cmd.Parameters.AddWithValue("@adr", vlt.volunteeraddress);
            cmd.Parameters.AddWithValue("@abt", vlt.volunteerabout);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }

        public List<VolunteerDataModel> getDatavolunteer()
        {
            List<VolunteerDataModel> lstEmp = new List<VolunteerDataModel>();
            SqlDataAdapter apt = new SqlDataAdapter("SELECT * FROM dbo.Volunteer", con);
            DataSet ds = new DataSet();
            apt.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lstEmp.Add(new VolunteerDataModel
                {
                    Id = Convert.ToInt32(dr["Id"].ToString()),
                    volunteername = dr["Name"].ToString(),
                    volunteernumber = dr["Number"].ToString(),
                    volunteeremail = dr["Email"].ToString(),
                    volunteeraddress= dr["Address"].ToString(),
                    volunteerabout = dr["About"].ToString()
                });
            }
            return lstEmp;
        }
    }
}
