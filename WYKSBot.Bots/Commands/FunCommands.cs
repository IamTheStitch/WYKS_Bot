using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WYKSBot.Bots.Attributes;
using WYKSBot.Bots.Dialogue;
using WYKSBot.Bots.Handlers.Dialogue.Steps;

namespace WYKSBot.Bots.Commands
{
    public class FunCommands : BaseCommandModule
    {

        /*[Command ("ping")]
        [Description("Returns pong")]
        [RequireCategories(ChannelCheckMode.Any, "Text Channels" )]
        public async Task Ping(CommandContext ctx)
        {
            //will respond with Pong
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [Command ("add")]
        [Description("Adds two numbers together")]
        public async Task Add(CommandContext ctx, 
            [Description("First Number")] int numberOne,
            [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command ("response")]
        public async Task Response(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }
        */
        [Command("respondreaction")]
        public async Task Reaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            //x is where we can set what the bot waits for
            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }
        /*
        [Command("poll")]
        public async Task Poll(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emojiOptions)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var options = emojiOptions.Select(x => x.ToString());

            var pollEmbed = new DiscordEmbedBuilder
            {
                Title = "Poll",
                Description = string.Join(" ", options)
            };

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);
            
            foreach(var option in emojiOptions)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
            
            var distinctResult = result.Distinct();

            //shows the results of each emoji and total amount
            var results = distinctResult.Select(x => $"{x.Emoji} : {x.Total}");

            //displays what emojis what was used
            await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);
        }

        [Command("emojidialogue")]
        public async Task EmojiDialogue(CommandContext ctx)
        {
            var yesStep = new TextStep("You chose yes", null);
            var noStep = new TextStep("You chose no", null);

            //insert emojis in dictionary here
            var emojiStep = new ReactionStep("Yes or No?", new Dictionary<DiscordEmoji, ReactionStepData>
        {
            { DiscordEmoji.FromName(ctx.Client, ":thumbsup:"), new ReactionStepData { Content = "This means yes", NextStep = yesStep} },
            { DiscordEmoji.FromName(ctx.Client, ":thumbsdown:"), new ReactionStepData { Content = "This means no", NextStep = noStep} }
        });

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                ctx.Channel,
                ctx.User,
                emojiStep
                );
            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }
        }
        */
    }    
}
