using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Skills_International
{
    internal class SQL
    {
        string connectionString = @"Data Source=SHEHAN-HP15S\SQLEXPRESS; Initial Catalog=STUDENTREGISTRATION; Integrated Security=True;";

        public bool CheckLogin(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM logins WHERE username=@username AND password=@password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                try
                {
                    connection.Open();
                    int result = (int)command.ExecuteScalar();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while checking login credentials.", ex);
                }
            }
        }

        public int[] GetRegistrationNumbers()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT regNo FROM registration";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while fetching registration numbers.", ex);
                }
            }

            int[] regNumbers = new int[dataTable.Rows.Count];
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                regNumbers[i] = Convert.ToInt32(dataTable.Rows[i]["regNo"]);
            }
            return regNumbers;
        }

        public DataRow GetRegistrationDetails(int regNo)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM registration WHERE regNo = @regNo";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@regNo", regNo);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while fetching registration details.", ex);
                }
            }

            return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
        }

        public void RegisterStudent(string firstName, string lastName, DateTime dateOfBirth, string gender, string address, string email, int? mobilePhone, int? homePhone, string parentName, string nic, int? contactNo)
        {
            string query = "INSERT INTO registration (firstName, lastName, dateOfBirth, gender, address, email, mobilePhone, homePhone, parentName, nic, contactNo) " +
                           "VALUES (@firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @nic, @contactNo)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@mobilePhone", mobilePhone.HasValue ? (object)mobilePhone.Value : DBNull.Value);
                command.Parameters.AddWithValue("@homePhone", homePhone.HasValue ? (object)homePhone.Value : DBNull.Value);
                command.Parameters.AddWithValue("@parentName", parentName);
                command.Parameters.AddWithValue("@nic", nic);
                command.Parameters.AddWithValue("@contactNo", contactNo.HasValue ? (object)contactNo.Value : DBNull.Value);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while registering the student.", ex);
                }
            }
        }

        public void UpdateStudent(int regNo, string firstName, string lastName, DateTime dateOfBirth, string gender, string address, string email, int? mobilePhone, int? homePhone, string parentName, string nic, int? contactNo)
        {
            string query = "UPDATE registration SET firstName = @firstName, lastName = @lastName, dateOfBirth = @dateOfBirth, gender = @gender, address = @address, email = @email, " +
                           "mobilePhone = @mobilePhone, homePhone = @homePhone, parentName = @parentName, nic = @nic, contactNo = @contactNo WHERE regNo = @regNo";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@regNo", regNo);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@mobilePhone", mobilePhone.HasValue ? (object)mobilePhone.Value : DBNull.Value);
                command.Parameters.AddWithValue("@homePhone", homePhone.HasValue ? (object)homePhone.Value : DBNull.Value);
                command.Parameters.AddWithValue("@parentName", parentName);
                command.Parameters.AddWithValue("@nic", nic);
                command.Parameters.AddWithValue("@contactNo", contactNo.HasValue ? (object)contactNo.Value : DBNull.Value);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the student.", ex);
                }
            }
        }

        public void DeleteStudent(int regNo)
        {
            string query = "DELETE FROM registration WHERE regNo = @regNo";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@regNo", regNo);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while deleting the student.", ex);
                }
            }
        }
    }
}
