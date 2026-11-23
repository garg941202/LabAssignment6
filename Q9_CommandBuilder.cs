
using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Q9_CommandBuilder
{
    public static void CRUD()
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

                    using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                    {
                        Console.WriteLine("1. Insert Student");
                        Console.WriteLine("2. Update Student");
                        Console.WriteLine("3. Delete Student");

                        Console.Write("Choose Operation: ");
                        int choice = Convert.ToInt32(Console.ReadLine());

                        if (choice == 1)
                        {
                            DataRow row = ds.Tables["Student"].NewRow();

                            Console.Write("Enter Student ID: ");
                            row["StudentID"] = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Student Name: ");
                            row["StudentName"] = Console.ReadLine();

                            Console.Write("Enter Course: ");
                            row["Course"] = Console.ReadLine();

                            Console.Write("Enter Age: ");
                            row["Age"] = Convert.ToInt32(Console.ReadLine());

                            ds.Tables["Student"].Rows.Add(row);

                            da.Update(ds, "Student");

                            Console.WriteLine("Student Inserted Successfully!");
                        }
                        else if (choice == 2)
                        {
                            Console.Write("Enter Student ID to Update: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            DataRow row = ds.Tables["Student"].Rows.Find(id);

                            if (row != null)
                            {
                                Console.Write("Enter New Name: ");
                                row["StudentName"] = Console.ReadLine();

                                Console.Write("Enter New Course: ");
                                row["Course"] = Console.ReadLine();

                                Console.Write("Enter New Age: ");
                                row["Age"] = Convert.ToInt32(Console.ReadLine());

                                da.Update(ds, "Student");

                                Console.WriteLine("Student Updated Successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Student ID Not Found!");
                            }
                        }
                        else if (choice == 3)
                        {
                            Console.Write("Enter Student ID to Delete: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            DataRow row = ds.Tables["Student"].Rows.Find(id);

                            if (row != null)
                            {
                                row.Delete();

                                da.Update(ds, "Student");

                                Console.WriteLine("Student Deleted Successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Student ID Not Found!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice!");
                        }
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
