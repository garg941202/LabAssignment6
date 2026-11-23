
using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Q8_Disconnected_Delete
{
    public static void Delete()
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

                    Console.Write("Enter Student ID to Delete: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    DataRow row = ds.Tables["Student"].Rows.Find(id);

                    if (row != null)
                    {
                        row.Delete();

                        using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                        {
                            da.Update(ds, "Student");
                        }

                        Console.WriteLine("Student Deleted Successfully!");
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
