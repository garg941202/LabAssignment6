
using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Q10_CompleteCRUD
{
    static string cs = @"Server=(localdb)\MSSQLLocalDB;Database=CollegeDB;Integrated Security=True;TrustServerCertificate=True;";

    public static void Run()
    {
        Console.WriteLine("===== COMPLETE CRUD OPERATIONS =====");
        Console.WriteLine("1. Connected Architecture");
        Console.WriteLine("2. Disconnected Architecture");
        Console.WriteLine("3. Compare Both Architectures");

        Console.Write("Choose Architecture: ");
        int architecture = Convert.ToInt32(Console.ReadLine());

        if (architecture == 1)
        {
            ConnectedCRUD();
        }
        else if (architecture == 2)
        {
            DisconnectedCRUD();
        }
        else if (architecture == 3)
        {
            CompareArchitecture();
        }
        else
        {
            Console.WriteLine("Invalid Choice!");
        }
    }

    // CONNECTED ARCHITECTURE
    static void ConnectedCRUD()
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            try
            {
                con.Open();

                Console.WriteLine("\nCONNECTED ARCHITECTURE");
                Console.WriteLine("1. Insert");
                Console.WriteLine("2. Display");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");

                Console.Write("Choose Operation: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Write("Enter ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Course: ");
                    string course = Console.ReadLine();

                    Console.Write("Enter Age: ");
                    int age = Convert.ToInt32(Console.ReadLine());

                    string query = "INSERT INTO Student VALUES (@id,@name,@course,@age)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@course", course);
                        cmd.Parameters.AddWithValue("@age", age);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Student Inserted Successfully!");
                    }
                }
                else if (choice == 2)
                {
                    string query = "SELECT * FROM Student";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Console.WriteLine(
                                dr["StudentID"] + " | " +
                                dr["StudentName"] + " | " +
                                dr["Course"] + " | " +
                                dr["Age"]);
                        }
                    }
                }
                else if (choice == 3)
                {
                    Console.Write("Enter Student ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter New Name: ");
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

                        Console.WriteLine(rows > 0 ?
                            "Student Updated Successfully!" :
                            "Student ID Not Found!");
                    }
                }
                else if (choice == 4)
                {
                    Console.Write("Enter Student ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    string query = "DELETE FROM Student WHERE StudentID=@id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        int rows = cmd.ExecuteNonQuery();

                        Console.WriteLine(rows > 0 ?
                            "Student Deleted Successfully!" :
                            "Student ID Not Found!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Operation!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    // DISCONNECTED ARCHITECTURE
    static void DisconnectedCRUD()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(cs))
            using (SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM Student", con))
            {
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                DataSet ds = new DataSet();

                da.Fill(ds, "Student");

                DataTable table = ds.Tables["Student"];

                using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                {
                    Console.WriteLine("\nDISCONNECTED ARCHITECTURE");
                    Console.WriteLine("1. Insert");
                    Console.WriteLine("2. Display");
                    Console.WriteLine("3. Update");
                    Console.WriteLine("4. Delete");

                    Console.Write("Choose Operation: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1)
                    {
                        DataRow row = table.NewRow();

                        Console.Write("Enter ID: ");
                        row["StudentID"] = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Name: ");
                        row["StudentName"] = Console.ReadLine();

                        Console.Write("Enter Course: ");
                        row["Course"] = Console.ReadLine();

                        Console.Write("Enter Age: ");
                        row["Age"] = Convert.ToInt32(Console.ReadLine());

                        table.Rows.Add(row);

                        da.Update(ds, "Student");

                        Console.WriteLine("Student Inserted Successfully!");
                    }
                    else if (choice == 2)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Console.WriteLine(
                                row["StudentID"] + " | " +
                                row["StudentName"] + " | " +
                                row["Course"] + " | " +
                                row["Age"]);
                        }
                    }
                    else if (choice == 3)
                    {
                        Console.Write("Enter Student ID: ");
                        int id = Convert.ToInt32(Console.ReadLine());

                        DataRow row = table.Rows.Find(id);

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
                    else if (choice == 4)
                    {
                        Console.Write("Enter Student ID: ");
                        int id = Convert.ToInt32(Console.ReadLine());

                        DataRow row = table.Rows.Find(id);

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
                        Console.WriteLine("Invalid Operation!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // COMPARISON
    static void CompareArchitecture()
    {
        Console.WriteLine("\n===== ARCHITECTURE COMPARISON =====");

        Console.WriteLine("\nCONNECTED ARCHITECTURE");
        Console.WriteLine("1. Uses SqlConnection and SqlCommand.");
        Console.WriteLine("2. Uses SqlDataReader to read records.");
        Console.WriteLine("3. Connection remains open during operations.");
        Console.WriteLine("4. Suitable for real-time database operations.");

        Console.WriteLine("\nDISCONNECTED ARCHITECTURE");
        Console.WriteLine("1. Uses SqlDataAdapter and DataSet.");
        Console.WriteLine("2. Data is stored in memory.");
        Console.WriteLine("3. Connection is not required while editing DataSet.");
        Console.WriteLine("4. Suitable for offline data processing.");

        Console.WriteLine("\nBoth architectures support CRUD operations.");
    }
}
