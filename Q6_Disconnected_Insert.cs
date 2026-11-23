
using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Q6_Disconnected_Insert
{
    public static void Insert()
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

                    Console.Write("Enter Student ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Student Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Course: ");
                    string course = Console.ReadLine();

                    Console.Write("Enter Age: ");
                    int age = Convert.ToInt32(Console.ReadLine());

                    DataRow row = ds.Tables["Student"].NewRow();

                    row["StudentID"] = id;
                    row["StudentName"] = name;
                    row["Course"] = course;
                    row["Age"] = age;

                    ds.Tables["Student"].Rows.Add(row);

                    using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                    {
                        da.Update(ds, "Student");
                    }

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
