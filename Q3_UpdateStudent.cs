
using System;
using Microsoft.Data.SqlClient;

class Q3_UpdateStudent
{
    public static void Update()
    {
        string cs = @"Server=(localdb)\MSSQLLocalDB;Database=CollegeDB;Integrated Security=True;TrustServerCertificate=True;";

        using (SqlConnection con = new SqlConnection(cs))
        {
            try
            {
                con.Open();

                Console.WriteLine("Database Connected Successfully!");

                Console.Write("Enter Student ID to Update: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter New Student Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter New Course: ");
                string course = Console.ReadLine();

                Console.Write("Enter New Age: ");
                int age = Convert.ToInt32(Console.ReadLine());

                string query = "UPDATE Student SET StudentName=@name, Course=@course, Age=@age WHERE StudentID=@id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@course", course);
                    cmd.Parameters.AddWithValue("@age", age);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                        Console.WriteLine("Student Updated Successfully!");
                    else
                        Console.WriteLine("Student ID Not Found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
