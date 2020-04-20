using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using WYKSBot.Bots.Dialogue;
using WYKSBot.Bots.Handlers.Dialogue.Steps;
using WYKSBot.Core.Service.Items;
using WYKSBot.DAL.Models.Items;

namespace WYKSBot.Bots.Commands
{
    public class ItemCommands : BaseCommandModule
    {
        private readonly IItemService _itemService;

        public ItemCommands(IItemService itemService)
        {
            _itemService = itemService;
        }
        
        [Command("createitem")]
        public async Task AddItem(CommandContext ctx)
        {
            var itemDescriptionStep = new TextStep("What is the item about?", null);
            var itemNameStep = new TextStep("What is the item called?", itemDescriptionStep);

            var item = new Item();

            itemNameStep.OnValidResult += (result) => item.Name = result;
            itemDescriptionStep.OnValidResult += (result) => item.Description = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                ctx.Channel,
                ctx.User,
                itemNameStep
                );

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded)
                return;

            await _itemService.CreateNewItemAsync(item).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync($"{item.Name} Created!").ConfigureAwait(false);
        }

        [Command("iteminfo")]
        public async Task Item(CommandContext ctx)
        {
            var itemNameStep = new TextStep("What item are you looking for?", null);

            string itemName = string.Empty;

            itemNameStep.OnValidResult += (result) => itemName = result;

            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                ctx.Channel,
                ctx.User,
                itemNameStep
            );

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            Item item = await _itemService.GetItemByNameAsync(itemName).ConfigureAwait(false);

            if (item == null)
            {
                await ctx.Channel.SendMessageAsync($"There is no item called {itemName}");
                return;
            }

            await ctx.Channel.SendMessageAsync($"Name: {item.Name}, Description: {item.Description} Price: {item.Price}"); }
    }
}
