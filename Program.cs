using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using Dapper;
using Helloworld.Models;
using Microsoft.Data.SqlClient;


namespace Helloworld
{

    public class Program
    {
        
        public static void Main(string[] args)
        {
        string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";
        

        IDbConnection dbConnection = new SqlConnection(connectionString);

        string sqlCommand = "SELECT GETDATE()";

        DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);

        Console.WriteLine(rightNow);

            Computer computer = new Computer()
            {
                Motherboard = "2600",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.45m,
                VideoCard = "rtx 2060"
            };

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + computer.Motherboard
                        + "', '" + computer.HasWifi
                        + "', '" + computer.HasLTE
                        + "', '" + computer.ReleaseDate
                        + "', '" + computer.Price
                        + "', '" + computer.VideoCard
            + "')";

            
            Console.WriteLine(sql);
            int result = dbConnection.Execute(sql);
            Console.WriteLine(result);

            string sqlSelect = @"SELECT * FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect);

            foreach(Computer singleComputer in computers)
            {
                Console.WriteLine("'" + computer.Motherboard
                        + "', '" + computer.HasWifi
                        + "', '" + computer.HasLTE
                        + "', '" + computer.ReleaseDate
                        + "', '" + computer.Price
                        + "', '" + computer.VideoCard
            + "')");
            }

            Console.WriteLine(computer.Motherboard);
            
        
        }
    }
}