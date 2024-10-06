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



            Computer computer = new Computer()
            {
                Motherboard = "2600",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.45m,
                VideoCard = "rtx 2060"
            };
            
            
        string sql = "\n" + @"INSERT INTO TutorialAppSchema.Computer(
        Motherboard,
        HasWifi,
        HasLTE,
        ReleaseDate,
        Price,
        VideoCard
        ) VALUES ('" + computer.ComputerId
                        + "', '" + computer.Motherboard
                        + "', '" + computer.HasWifi
                        + "', '" + computer.HasLTE
                        + "', '" + computer.ReleaseDate
                        + "', '" + computer.Price
                        + "', '" + computer.VideoCard
                    + "')\n";
            
        File.WriteAllText("log.txt", sql);

        using StreamWriter openFile = new("log.txt", append: true);

        
        openFile.WriteLine(sql);
        openFile.Close();

        string result = File.ReadAllText("log.txt");
        
        Console.WriteLine(result);
        }
    }
}