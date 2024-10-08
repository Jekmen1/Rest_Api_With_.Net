using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
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

        string computersJson = File.ReadAllText("ComputersSnake.json");

        Mapper mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<ComputerSnake, Computer>()
                .ForMember(destination => destination.ComputerId, options =>
                 options.MapFrom(source => source.computer_id))
                .ForMember(destination => destination.CPUCores, options =>
                 options.MapFrom(source => source.cpu_cores))
                .ForMember(destination => destination.HasLTE, options =>
                 options.MapFrom(source => source.has_lte))
                .ForMember(destination => destination.HasWifi, options =>
                 options.MapFrom(source => source.has_wifi))
                .ForMember(destination => destination.Motherboard, options =>
                 options.MapFrom(source => source.motherboard))
                .ForMember(destination => destination.VideoCard, options =>
                 options.MapFrom(source => source.video_card))
                .ForMember(destination => destination.ReleaseDate, options =>
                 options.MapFrom(source => source.release_data))
                .ForMember(destination => destination.Price, options =>
                 options.MapFrom(source => source.price * .8m));
        }));
        
        IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);
        
        if(computersSystem != null)
        {
            IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);

            foreach(Computer computer in computerResult)
            {
            }
        }

        // JsonSerializerOptions options = new JsonSerializerOptions()
        // {
        //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase   
        // };
        
        // // // Console.WriteLine(computersJson);

        // IEnumerable<Computer>? computersNewtonSoft = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
        // IEnumerable<Computer>? computersSystem = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

        // if (computersNewtonSoft != null)
        // {
        //     foreach(Computer computer in computersNewtonSoft)
        //     {
        //         string sql = "\n" + @"INSERT INTO TutorialAppSchema.Computer(
        //             Motherboard,
        //             HasWifi,
        //             HasLTE,
        //             ReleaseDate,
        //             Price,
        //             VideoCard
        //         ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
        //                         + "', '" + computer.HasWifi
        //                         + "', '" + computer.HasLTE
        //                         + "', '" + computer.ReleaseDate
        //                         + "', '" + computer.Price
        //                         + "', '" +EscapeSingleQuote (computer.VideoCard)
        //                     + "')\n";

        //         dapper.ExecuteSql(sql);

        //     }
        // }

        // JsonSerializerSettings settings = new JsonSerializerSettings()
        // {
        //     ContractResolver = new CamelCasePropertyNamesContractResolver()
        // };

        // string computersCopy = JsonConvert.SerializeObject(computersNewtonSoft, settings);
        // File.WriteAllText("computersCopyNewtonsoft.txt", computersCopy);




        // string computersCopy2 = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);
        // File.WriteAllText("computersCopyNewtonsoft2.txt", computersCopy2);
        // }


        // static string EscapeSingleQuote(string input)
        // {
        //     string output = input.Replace("'", "''");

        //     return output;
        // }
    }
}
}