using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWTradeRecommendationBot
{
    public class Commands : ModuleBase
    {
        private string APIKey = System.IO.File.ReadAllText(@"APIKey.txt");
        [Command("ping")]
        public async Task Ping()
        {
            var msg = await ReplyAsync("***Calculating ping***");

            await ReplyAsync($"Pong! ***{msg.Timestamp.Millisecond - DateTime.Now.Millisecond}***ms");


            await msg.DeleteAsync();
        }

        [Command("hello")]
        public async Task HelloCommand([Remainder]string args = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();



            // get user info from the Context
            var user = Context.User.ToString();

            // build out the reply
            sb.AppendLine($"You are -> [{user}] and your arguments were [{args}]");
            //sb.AppendLine($"And your parsed arguments were {SplittedArgs[0]} and {SplittedArgs[1]}.");

            // send simple string reply
            await ReplyAsync(sb.ToString());
        }
        [Command("help")]
        public async Task helpCommand()
        {
            var sb = new StringBuilder();
            sb.AppendLine("There is no help for you");
            await ReplyAsync(sb.ToString());
        }
        [Command("city")]
        public async Task cityCommand([Remainder]string args = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            

            // get user info from the Context
            var user = Context.User.ToString();
            PullAPIDATA pullapi = new PullAPIDATA();
            string APIData = pullapi.GetCity(args, APIKey);
            City city = new City();
            city = JsonConvert.DeserializeObject<City>(APIData);
            // build out the reply
            sb.AppendLine($"You are -> [{user}]");
            sb.AppendLine($"API pull were successful: {city.success}!");
            sb.AppendLine($"Error Message {city.General_message}");
            //sb.AppendLine($"And your parsed arguments were {SplittedArgs[0]} and {SplittedArgs[1]}.");

            // send simple string reply
            await ReplyAsync(sb.ToString());
        }

    }
}