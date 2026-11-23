
using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Q7_Disconnected_Update
{
    public static void Update()
    {
        string cs = @"Server=(localdb)\MSSQLLocalDB;Database=CollegeDB;Integrated Security=True;TrustServerCertificate=True;";

        try
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Student";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    DataSet ds = new DataSet();

                    da.Fill(ds, "Student");

                    Console.WriteLine("Student records loaded into DataSet.");

                    Console.Write("Enter Student ID to Update: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    DataRow row = ds.Tables["Student"].Rows.Find(id);

                    if (row != null)
                    {
                        Console.Write("Enter New Student Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter New Course: ");
                        string course = Console.ReadLine();

                        Console.Write("Enter New Age: ");
                        int age = Convert.ToInt32(Console.ReadLine());

                        row["StudentName"] = name;
                        row["Course"] = course;
                        row["Age"] = age;

                        using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                        {
                            da.Update(ds, "Student");
                        }

                        Console.WriteLine("Student Updated Successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Student ID Not Found!");
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
