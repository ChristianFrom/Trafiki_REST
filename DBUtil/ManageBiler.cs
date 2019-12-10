using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            string queryString = "SELECT * FROM BilerTest";

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
                        bil.Dato = reader.GetString(1);
                        bil.Tid = reader.GetString(2);
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


        public IEnumerable<Bil> SearchBiler(QueryCar qcar)
        {
            List<Bil> bilListe = getAllBiler();
            List<Bil> result = new List<Bil>();

            string dateString, dateFormat;
            string timeString, timeFormat;
            CultureInfo provider = CultureInfo.InvariantCulture;

            foreach (Bil bil in bilListe)
            {
                dateString = bil.Dato;
                dateFormat = "d";

                timeString = bil.Tid;
                timeFormat = "HH:mm";
                
                DateTime date = DateTime.ParseExact(dateString, "dd-MM-yyyy", provider);
                DateTime time = DateTime.ParseExact(timeString, timeFormat, provider);

                bool datoRange = date <= qcar.MaxDate && date >= qcar.MinDate;
                bool timeRange = time <= qcar.MaxTime && time >= qcar.MinTime;


                if (datoRange&timeRange)
                {
                    result.Add(bil);
                }
            }
            return result;
        }


        //public IEnumerable<Bil> SearchBilerSpecifik(QueryCar qcar)
        //{
        //    List<Bil> bilListe = getAllBiler().ToList();
        //    List<Bil> result = new List<Bil>();


        //}

        public int CountBiler()
        {
            List<Bil> bilListe = getAllBiler().ToList();

            int antal = bilListe.Count;

            return antal;
        }

        //public Bil GetBilFromId(int id)
        //{
        //    Bil bil = new Bil();
        //    string queryString = "SELECT * FROM BilerTest WHERE Nummer=" + id;

        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        SqlCommand command = new SqlCommand(queryString, connection);
        //        command.Connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        try
        //        {
        //            while (reader.Read())
        //            {
        //                bil.Nummer = reader.GetInt32(0);
        //                bil.Dato = reader.GetDateTime(1);
        //                bil.Tid = reader.GetDateTime(2);
        //            }
        //        }
        //        finally
        //        {
        //            reader.Close();
        //        }
        //    }
        //    return bil;
        //}

        
        public bool CreateBil(Bil bil)
        {
            string queryString = "INSERT INTO BilerTest (Dato, Tid) VALUES (@Dato, @Tid)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Dato", bil.Dato = DateTime.Now.Date.ToShortDateString());
                    command.Parameters.AddWithValue("@Tid", bil.Tid = DateTime.Now.ToShortTimeString());                    

                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (SqlException)
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
            string queryString = "TRUNCATE TABLE BilerTest";

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
