using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapper;
using Helloworld.Data;
using Helloworld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


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

        //     Computer computer = new Computer()
        //     {
        //         Motherboard = "2600",
        //         HasWifi = true,
        //         HasLTE = false,
        //         ReleaseDate = DateTime.Now,
        //         Price = 943.45m,
        //         VideoCard = "rtx 2060"
        //     };
            
            
        // string sql = "\n" + @"INSERT INTO TutorialAppSchema.Computer(
        // Motherboard,
        // HasWifi,
        // HasLTE,
        // ReleaseDate,
        // Price,
        // VideoCard
        // ) VALUES ('" + computer.ComputerId
        //                 + "', '" + computer.Motherboard
        //                 + "', '" + computer.HasWifi
        //                 + "', '" + computer.HasLTE
        //                 + "', '" + computer.ReleaseDate
        //                 + "', '" + computer.Price
        //                 + "', '" + computer.VideoCard
        //             + "')\n";
            
        // File.WriteAllText("log.txt", sql);

        // using StreamWriter openFile = new("log.txt", append: true);

        
        // openFile.WriteLine(sql);
        // openFile.Close();

        string computersJson = File.ReadAllText("Computers.json");

        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase   
        };
        
        // // Console.WriteLine(computersJson);

        IEnumerable<Computer>? computersNewtonSoft = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
        IEnumerable<Computer>? computersSystem = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

        if (computersNewtonSoft != null)
        {
            foreach(Computer computer in computersNewtonSoft)
            {
                string sql = "\n" + @"INSERT INTO TutorialAppSchema.Computer(
                    Motherboard,
                    HasWifi,
                    HasLTE,
                    ReleaseDate,
                    Price,
                    VideoCard
                ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                                + "', '" + computer.HasWifi
                                + "', '" + computer.HasLTE
                                + "', '" + computer.ReleaseDate
                                + "', '" + computer.Price
                                + "', '" +EscapeSingleQuote (computer.VideoCard)
                            + "')\n";

                dapper.ExecuteSql(sql);

            }
        }

        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        string computersCopy = JsonConvert.SerializeObject(computersNewtonSoft, settings);
        File.WriteAllText("computersCopyNewtonsoft.txt", computersCopy);




        string computersCopy2 = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);
        File.WriteAllText("computersCopyNewtonsoft2.txt", computersCopy2);
        }


        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");

            return output;
        }
    }
}