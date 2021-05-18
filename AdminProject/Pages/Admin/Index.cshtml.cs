using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdminProject.Data;
using AdminProject.Models;
using Microsoft.Data.SqlClient;

namespace AdminProject.Pages.Members
{
    public class IndexModel : PageModel
    {
        public List<AllMembers> UserList { get; set; } //List to hold the records
        [BindProperty(SupportsGet = true)]
        public string SearchData { get; set; } //Stores the searched term from the browser

        public void OnGet()
        {
            if(SearchData != null)
            {
                string dbconnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AllMembers;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                SqlConnection conn = new SqlConnection(dbconnection);
                conn.Open(); //Database connection

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;

                    command.CommandText = @"SELECT Id, Username, FirstName, LastName, Email, Role FROM UsersTable WHERE Username = '" + SearchData + "'";
                    //Select the records that match the username entered by the user

                    SqlDataReader read = command.ExecuteReader();

                    UserList = new List<AllMembers>();

                    while (read.Read())
                    {
                        AllMembers record = new AllMembers();
                        record.Id = read.GetInt32(0);
                        record.Username = read.GetString(1);
                        record.FirstName = read.GetString(2); //Read all the information 
                        record.LastName = read.GetString(3);
                        record.Email = read.GetString(4);
                        record.Role = read.GetString(5);

                         UserList.Add(record); //Add it to the list
                    }
                    read.Close();
                }
            }
            else if (SearchData == null) //If the user searches for nothing
            {
                string dbconnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AllMembers;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


                SqlConnection conn = new SqlConnection(dbconnection);
                conn.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = @"SELECT * FROM UsersTable WHERE Role = 'Customer' OR Role = 'Librarian'";

                    SqlDataReader read = command.ExecuteReader(); //Select all customers and librarians

                    UserList = new List<AllMembers>();

                    while (read.Read())
                    {
                        AllMembers record = new AllMembers();
                        record.Id = read.GetInt32(0);
                        record.Username = read.GetString(1);
                        record.FirstName = read.GetString(2);
                        record.LastName = read.GetString(3); //Read records
                        record.Email = read.GetString(4);
                        record.Password = read.GetString(5);
                        record.Role = read.GetString(6);

                        UserList.Add(record); //Add to the list 
                    }
                    read.Close();  
                }
            }
        }
    }
}

