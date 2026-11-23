
using System;
using Microsoft.Data.SqlClient;

class Q4_DeleteStudent
{
    public static void Delete()
    {
        string cs = @"Server=(localdb)\MSSQLLocalDB;Database=CollegeDB;Integrated Security=True;TrustServerCertificate=True;";

        using (SqlConnection con = new SqlConnection(cs))
        {
            try
            {
                con.Open();

                Console.WriteLine("Database Connected Successfully!");

                Console.Write("Enter Student ID to Delete: ");
                int id = Convert.ToInt32(Console.ReadLine());

                string query = "DELETE FROM Student WHERE StudentID = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        Console.WriteLine("Student Deleted Successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Student ID Not Found!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
