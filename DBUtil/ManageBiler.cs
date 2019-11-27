using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Trafiki_ModelLib;

namespace Trafiki_REST.DBUtil
{
    public class ManageBiler
    {
        private const string ConnectionString = "Server=tcp:cfdatabase.database.windows.net,1433;Initial Catalog=trafikiDatabase;" +
                                                "Persist Security Info=False;User ID=christianfrom;Password=Rasmussen1;" +
                                                "MultipleActiveResultSets=False;Encrypt=True;" +
                                                "TrustServerCertificate=False;Connection Timeout=30;";


        public List<Bil> getAllBiler()
        {
            List<Bil> BilList = new List<Bil>();
            string queryString = "SELECT * FROM Biler";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Bil bil = new Bil();
                        bil.Nummer = reader.GetInt32(0);
                        bil.Tidspunkt = reader.GetDateTime(1);
                        BilList.Add(bil);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return BilList;
        }

        public Bil GetBilFromId(int id)
        {
            Bil bil = new Bil();
            string queryString = "SELECT * FROM Biler WHERE Nummer=" + id;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        bil.Nummer = reader.GetInt32(0);
                        bil.Tidspunkt = reader.GetDateTime(1);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return bil;
        }



        public bool CreateBil(Bil bil)
        {
            string queryString = "INSERT INTO Biler (Nummer,Tidspunkt) VALUES (@Nummer,@Tidspunkt)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);     
                    command.Parameters.AddWithValue("@Nummer", bil.Nummer);                    
                    command.Parameters.AddWithValue("@Tidspunkt", bil.Tidspunkt);                    
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (SqlException e)
                {
                   throw new ArgumentException("500");
                }
            }
            return true;
        }

        public bool deleteBil(int id)
        {
            string queryString = "DELETE FROM Biler WHERE Nummer=@Nummer";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Nummer", id);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (SqlException)
                {

                    return false;
                }
            }

            return true;

        }

        public bool deleteAllBiler()
        {
            string queryString = "TRUNCATE TABLE Biler";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (SqlException)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
