namespace Helloworld.Models
{
        public class ComputerSnake
    {
        public int computer_id { get; set; }
        public string motherboard { get; set; }
        public int? has_wifi { get; set; }
        public bool has_lte { get; set; }
        public DateTime? release_data { get; set; }
        public string video_card { get; set; }
        public string cpu_cores { get; set; }
        public decimal price { get; set; }


        public ComputerSnake()
        {
            if(video_card == null)
            {
                video_card = "";
            } 
            if(motherboard == null)
            {
                motherboard = "";
            }
            if(cpu_cores == null)
            {
                cpu_cores = 0;
            }
            
        }

    }
}