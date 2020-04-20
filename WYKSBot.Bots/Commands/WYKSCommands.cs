using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WYKSBot.Bots.Dialogue;
using WYKSBot.Bots.Handlers.Dialogue.Steps;
using WYKSBot.Core.Service.Items;

namespace WYKSBot.Bots.Commands
{
    public class WYKSCommands : BaseCommandModule
    {
        private readonly IItemService _itemService;

        private int PLAYERS = 0;
        
        private const long TIME = 100000000;

        private int MONSTER = 1;

        private TimeSpan DURATION = new TimeSpan(TIME);
        public WYKSCommands(IItemService itemService)
        {
            _itemService = itemService;
        }

        [Command("start")]
        [Description("Asks who wants to play the game :eyes:")]
        public async Task Start(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            
            var discordEmoji = new DiscordEmoji[1];
            discordEmoji[0] = DiscordEmoji.FromUnicode("👍🏽");

            var options = discordEmoji.Select(x => x.ToString());

            var pollEmbed = new DiscordEmbedBuilder
            {
                Title = "Who wants to play a game?",
                Description = string.Join("", discordEmoji[0])
            };
            
            var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);

            foreach (var option in discordEmoji)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivity.CollectReactionsAsync(pollMessage, DURATION).ConfigureAwait(false);

            var distinctResult = result.Distinct().Count();

            PLAYERS = distinctResult;

            if (PLAYERS == 0)
            {
                await ctx.Channel.SendMessageAsync(PLAYERS + " players is playing :(").ConfigureAwait(false);
            }
              
            else if(PLAYERS == 1)
            {
                await ctx.Channel.SendMessageAsync(PLAYERS + " player is playing! \nType /ready to begin the game!").ConfigureAwait(false);
            }
                
            else
            {
                await ctx.Channel.SendMessageAsync(PLAYERS + " players are playing! \nType /ready to begin the game!").ConfigureAwait(false);
            }
        }

        [Command("ready")]
        [Description("Calls for a ready check!")]
        //implement further
        public async Task Ready(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var discordEmoji = new DiscordEmoji[1];
            discordEmoji[0] = DiscordEmoji.FromUnicode("👍🏽");

            var options = discordEmoji.Select(x => x.ToString());

            var pollEmbed = new DiscordEmbedBuilder
            {
                Title = "Are you ready to Begin?",
                //Description = string.Join("Press the check mark to signify you are ready: ", options)
            };

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);

            foreach (var option in discordEmoji)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }
            int readyCheck = 0;
            while (readyCheck != PLAYERS)
            {
               // _ = interactivity.WaitForReactionAsync().ConfigureAwait(false);
                readyCheck++;
            }
            //displays what emojis what was used
            await ctx.Channel.SendMessageAsync($"All {readyCheck} players are ready...It's time to begin!").ConfigureAwait(false);
        }

        [Command("reset")]
        [Description("Resets the bot")]
        public async Task Reset(CommandContext ctx)
        {
            PLAYERS = 0;
            MONSTER = 1;
            await ctx.Channel.SendMessageAsync("Bot Reset!").ConfigureAwait(false);
            await Start(ctx);
        }
    }
}
