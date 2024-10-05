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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Helloworld
{

    public class Program
    {
        
        public static void Main(string[] args)
        {
        IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

        DataContextDapper dapper = new DataContextDapper(config);
        DataContextEF entityFramework = new DataContextEF(config);



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
            
            entityFramework.Add(computer);
            entityFramework.SaveChanges();

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
            bool result = dapper.ExecuteSql(sql);
            Console.WriteLine(result);

            string sqlSelect = @"SELECT * FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);


            foreach(Computer singleComputer in computers)
            {
                Console.WriteLine("'" + computer.ComputerId
                        + "', '" + computer.Motherboard
                        + "', '" + computer.HasWifi
                        + "', '" + computer.HasLTE
                        + "', '" + computer.ReleaseDate
                        + "', '" + computer.Price
                        + "', '" + computer.VideoCard
            + "')");
            }

            Console.WriteLine(computer.Motherboard);

            IEnumerable<Computer>? computerEF = entityFramework.Computer?.ToList<Computer>();
            if(computerEF != null)
            {
                foreach(Computer singleComputer in computers)
                {
                    Console.WriteLine("'" + computer.ComputerId
                            + "', '" + computer.Motherboard
                            + "', '" + computer.HasWifi
                            + "', '" + computer.HasLTE
                            + "', '" + computer.ReleaseDate
                            + "', '" + computer.Price
                            + "', '" + computer.VideoCard
                        + "')");
                }
            }


            Console.WriteLine(computer.Motherboard);
            
        
        }
    }
}