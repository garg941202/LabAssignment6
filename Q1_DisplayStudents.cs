
using System;
using Microsoft.Data.SqlClient;

class Q1_DisplayStudents
{
    public static void Display()
    {
        string cs = @"Server=(localdb)\MSSQLLocalDB;Database=CollegeDB;Integrated Security=True;TrustServerCertificate=True;";

        using (SqlConnection con = new SqlConnection(cs))
        {
            try
            {
                con.Open();

                Console.WriteLine("Database Connected Successfully!");

                string query = "SELECT * FROM Student";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("ID: " + dr["StudentID"]);
                    Console.WriteLine("Name: " + dr["StudentName"]);
                    Console.WriteLine("Course: " + dr["Course"]);
                    Console.WriteLine("Age: " + dr["Age"]);
                    Console.WriteLine("------------------");
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
