using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Ice4
{
    internal class UserSQL
    {

            private static string conString = "Server=tcp:gitgladiatorsdb.database.windows.net,1433;Initial Catalog=GitGladiatorsICE4DB;Persist Security Info=False;User ID=GitGladiatorsAdmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            public static int Login(User user)
            {
                string query = "Select UserID from UserTable where UserName=@Username and UserPassword=@UserPassword";

                using (SqlConnection conn = new SqlConnection(conString))
                {

                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@UserName", user.UserName);

                        cmd.Parameters.AddWithValue("@UserPassword", user.UserPassword);


                        Object userID = cmd.ExecuteScalar();

                        if (userID != null)
                        {
                            int iUserId = (int)userID;

                            return iUserId;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }

            public static int checkUserName(Users users)
            {
                string query = "Select Count(*) from UserTable where UserName=@Username";

                using (SqlConnection conn = new SqlConnection(conString))
                {

                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@UserName", users.UserName);

                        int userID = (int)cmd.ExecuteScalar();

                        if (userID > 0)
                        {

                            return -1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }

            public static int Register(Users users)
            {
                int iCheck = checkUserName(users);

                if (iCheck == -1)
                {

                    return iCheck;

                }

                string query = "Insert into Users (UserName, UserPassword) Values (@UserName, @UserPassword)";

                using (SqlConnection conn = new SqlConnection(conString))
                {

                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@UserName", users.UserName);
                        cmd.Parameters.AddWithValue("@UserPassword", users.UserPassword);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Login(users);
                        }
                        else
                        {
                            return -1;
                        }

                    }

                }
            }
        }
    }


