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


        public IEnumerable<Bil> SearchBilerBegge(QueryCar qcar)
        {
            List<Bil> bilListe = getAllBiler();
            List<Bil> result = new List<Bil>();

            string dateString, dateFormat;
            string timeString, timeFormat;
            string maxDateString;
            string minDateString;
            string maxTimeString;
            string minTimeString;

            CultureInfo provider = new CultureInfo("da-DK");

            maxDateString = qcar.MaxDateString;
            minDateString = qcar.MinDateString;
            maxTimeString = qcar.MaxTimeString;
            minTimeString = qcar.MinTimeString;

            timeFormat = "HH:mm";
            dateFormat = "dd-MM-yyyy";


            DateTime maxDate = DateTime.ParseExact(maxDateString, dateFormat, provider);
            DateTime minDate = DateTime.ParseExact(minDateString, dateFormat, provider);
            DateTime maxTime = DateTime.ParseExact(maxTimeString, timeFormat, provider);
            DateTime minTime = DateTime.ParseExact(minTimeString, timeFormat, provider);
            

            foreach (Bil bil in bilListe)
            {
                dateString = bil.Dato;
                timeString = bil.Tid;

                DateTime date = DateTime.ParseExact(dateString, dateFormat, provider);
                DateTime time = DateTime.ParseExact(timeString, timeFormat, provider);

                bool datoRange = date <= maxDate && date >= minDate;
                bool timeRange = time <= maxTime && time >= minTime;


                if (datoRange&&timeRange)
                {
                    result.Add(bil);
                }
            }
            return result;
        }

        public IEnumerable<Bil> SearchBilerTid(QueryCar qcar)
        {
            List<Bil> bilListe = getAllBiler();
            List<Bil> result = new List<Bil>();


            string timeString, timeFormat;
            string maxTimeString;
            string minTimeString;

            CultureInfo provider = new CultureInfo("da-DK");


            maxTimeString = qcar.MaxTimeString;
            minTimeString = qcar.MinTimeString;

            timeFormat = "HH:mm";


            DateTime maxTime = DateTime.ParseExact(maxTimeString, timeFormat, provider);
            DateTime minTime = DateTime.ParseExact(minTimeString, timeFormat, provider);


            foreach (Bil bil in bilListe)
            {

                timeString = bil.Tid;

                DateTime time = DateTime.ParseExact(timeString, timeFormat, provider);

                bool timeRange = time <= maxTime && time >= minTime;


                if (timeRange)
                {
                    result.Add(bil);
                }
            }
            return result;
        }

        public int CountBiler()
        {
            List<Bil> bilListe = getAllBiler().ToList();

            int antal = bilListe.Count;

            return antal;
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
                        bil.Dato = reader.GetString(1);
                        bil.Tid = reader.GetString(2);
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
            string queryString = "INSERT INTO Biler (Dato, Tid) VALUES (@Dato, @Tid)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    CultureInfo provider = new CultureInfo("da-DK");

                    DateTime dato = DateTime.Now;
                    DateTime tid = DateTime.Now;
                    
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Dato", bil.Dato = dato.ToString("d", provider));
                    command.Parameters.AddWithValue("@Tid", bil.Tid = tid.ToString("t", provider));
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
