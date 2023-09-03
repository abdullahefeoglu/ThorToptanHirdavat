using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataModel
    {
        SqlConnection con; SqlCommand cmd;

        public DataModel()
        {
            con = new SqlConnection(ConnectionStrings.ConStr);
            cmd = con.CreateCommand();
        }

        public Manager GetManager(int id)
        {
            try
            {
                Manager model = new Manager();
                cmd.CommandText = "SELECT [ID],[Name],[Surname],[Nickname],[Mail],[Password],[Active] FROM Manager WHERE ID = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    model.ID = reader.GetInt32(0);
                    model.Name = reader.GetString(1);
                    model.Surname = reader.GetString(2);
                    model.Nickname = reader.GetString(3);
                    model.Mail = reader.GetString(4);
                    model.Password = reader.GetString(5);
                    model.Active = reader.GetBoolean(6);
                }
                return model;
            }
            catch
            {
                return null;
            }
            finally { con.Close(); }
        }
        public Manager ManagerLogin(string nickname, string password)
        {
            Manager model = new Manager();
            try
            {
                cmd.CommandText = "SELECT ID FROM Manager WHERE Nickname = @nickname AND Password = @password";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nickname", nickname);
                cmd.Parameters.AddWithValue("@password", password);
                con.Open();
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                if (id > 0)
                {
                    model = GetManager(id);
                }
                return model;
            }
            catch
            {
                return null;
            }
            finally { con.Close(); }
        }
    }
}
