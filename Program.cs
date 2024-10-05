using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using Dapper;
using Helloworld.Data;
using Helloworld.Models;
using Microsoft.Data.SqlClient;


namespace Helloworld
{

    public class Program
    {
        
        public static void Main(string[] args)
        {
        
        DataContextDapper dapper = new DataContextDapper();



        string sqlCommand = "SELECT GETDATE()";

        DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);

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
            bool result =dapper.ExecuteSql(sql);
            Console.WriteLine(result);

            string sqlSelect = @"SELECT * FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

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