
using System;
using Microsoft.Data.SqlClient;

class Q2_InsertStudent
{
    public static void Insert()
    {
        string cs = @"Server=(localdb)\MSSQLLocalDB;Database=CollegeDB;Integrated Security=True;TrustServerCertificate=True;";

        using (SqlConnection con = new SqlConnection(cs))
        {
            try
            {
                con.Open();

                Console.WriteLine("Database Connected Successfully!");

                Console.Write("Enter Student ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Student Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Course: ");
                string course = Console.ReadLine();

                Console.Write("Enter Age: ");
                int age = Convert.ToInt32(Console.ReadLine());

                string query = "INSERT INTO Student (StudentID, StudentName, Course, Age) VALUES (@id, @name, @course, @age)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@course", course);
                    cmd.Parameters.AddWithValue("@age", age);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        Console.WriteLine("Student Inserted Successfully!");
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
