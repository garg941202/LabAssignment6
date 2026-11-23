
using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Q5_DataAdapter_Display
{
    public static void Display()
    {
        string cs = @"Server=(localdb)\MSSQLLocalDB;Database=CollegeDB;Integrated Security=True;TrustServerCertificate=True;";

        try
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Student";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    DataSet ds = new DataSet();

                    da.Fill(ds, "Student");

                    Console.WriteLine("Student Records:");
                    Console.WriteLine("---------------------------");

                    foreach (DataRow row in ds.Tables["Student"].Rows)
                    {
                        Console.WriteLine("ID: " + row["StudentID"]);
                        Console.WriteLine("Name: " + row["StudentName"]);
                        Console.WriteLine("Course: " + row["Course"]);
                        Console.WriteLine("Age: " + row["Age"]);
                        Console.WriteLine("---------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
