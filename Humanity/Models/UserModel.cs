using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.Models
{
	public class UserModel
	{
		SqlConnection con = new SqlConnection(@"Server=RONAK;Database=Humanity;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
		public string Id { get; set; }

		[Required(ErrorMessage = "Please enter Email")]
		[EmailAddress]
		public string Email { get; set; }			
		
		[Required(ErrorMessage = "Please enter Phone Number")]
		public string PhoneNumber { get; set; }
		
		[Required(ErrorMessage = "please enter Password")]
		[DataType(DataType.Password)]
		public string PasswordHash { get; set; }

		public List<UserModel> getData()
		{
			List<UserModel> lstEmp = new List<UserModel>();
			SqlDataAdapter apt = new SqlDataAdapter("select Id,Email,PhoneNumber,PasswordHash from dbo.AspNetUsers", con);
			DataSet ds = new DataSet();
			apt.Fill(ds);
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				lstEmp.Add(new UserModel
				{
					Id = dr["Id"].ToString(),
					Email = dr["Email"].ToString(),
					PhoneNumber = dr["PhoneNumber"].ToString(),
					PasswordHash = dr["PasswordHash"].ToString()
				});
			}
			return lstEmp;
		}

		
		//public UserModel getData(string Id)
		//{
		//	UserModel usr = new UserModel();
		//	SqlCommand cmd = new SqlCommand("select Id,Email,PhoneNumber,PasswordHash from dbo.AspNetUsers where id=''" + Id + "'", con);
		//	con.Open();
		//	SqlDataReader dr = cmd.ExecuteReader();
		//	if (dr.HasRows)
		//	{
		//		while (dr.Read())
		//		{
		//			usr.Id = dr["Id"].ToString();
		//			usr.Email = dr["Email"].ToString();
		//			usr.PhoneNumber = dr["PhoneNumber"].ToString();
		//			usr.PasswordHash = dr["PasswordHash"].ToString();
		//		}
		//	}
		//	con.Close();
		//	return usr;
		//}
		////Insert a record into a database table
		//public bool insert(UserModel usr)
		//{
		//	SqlCommand cmd = new SqlCommand("insert into dbo.AspNetUsers (Email,PhoneNumber,PasswordHash) values(@name, @phon, @psd)", con);

		//	cmd.Parameters.AddWithValue("@name", usr.Email);
		//	cmd.Parameters.AddWithValue("@phon", usr.PhoneNumber);
		//	cmd.Parameters.AddWithValue("@psd", usr.PasswordHash);
		//	con.Open();
		//	int i = cmd.ExecuteNonQuery();
		//	if (i >= 1)
		//	{
		//		return true;
		//	}
		//	return false;
		//}
		////Update a record into a database table
		//public bool update(UserModel usr)
		//{
		//	SqlCommand cmd = new SqlCommand("update dbo.AspNetUsers set Email=@email,PhoneNumber = @phone, PasswordHash = @psd where Id = @id", con);

		//	cmd.Parameters.AddWithValue("@email", usr.Email);
		//	cmd.Parameters.AddWithValue("@phone", usr.PhoneNumber);
		//	cmd.Parameters.AddWithValue("@psd", usr.PasswordHash);
		//	cmd.Parameters.AddWithValue("@id", usr.Id);
		//	con.Open();
		//	int i = cmd.ExecuteNonQuery();
		//	if (i >= 1)
		//	{
		//		return true;
		//	}
		//	return false;
		//}
		////delete a record from a database table
		//public bool delete(UserModel usr)
		//{
		//	SqlCommand cmd = new SqlCommand("delete dbo.AspNetUsers where Id = @id", con);
		//	cmd.Parameters.AddWithValue("@id", usr.Id);
		//	con.Open();
		//	int i = cmd.ExecuteNonQuery();
		//	if (i >= 1)
		//	{
		//		return true;
		//	}
		//	return false;
		//}
	}
}
