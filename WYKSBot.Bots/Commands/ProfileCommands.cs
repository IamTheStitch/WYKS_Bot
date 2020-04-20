using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Linq;
using System.Threading.Tasks;
using WYKSBot.Core.Services.Profiles;
using WYKSBot.Core.ViewModels;
using WYKSBot.DAL.Profiles;

namespace WYKSBot.Bots.Commands
{
    public class ProfileCommands : BaseCommandModule
    {
        private readonly IProfileService _profileService;
        private readonly IExperienceService _experienceService;

        public ProfileCommands(IProfileService profileService, IExperienceService experienceService)
        {
            _profileService = profileService;
            _experienceService = experienceService;
        }

        [Command("profile")]
        public async Task Profile(CommandContext ctx)
        {
            await GetProfileToDisplayAsync(ctx, ctx.Member.Id);
        }

        [Command("profile")]
        public async Task Profile(CommandContext ctx, DiscordMember member)
        {
            await GetProfileToDisplayAsync(ctx, member.Id);
        }

        private async Task GetProfileToDisplayAsync(CommandContext ctx, ulong memberId)
        {
            Profile profile = await _profileService.GetOrCreateProfileAsync(memberId, ctx.Guild.Id).ConfigureAwait(false);

            DiscordMember member = ctx.Guild.Members[profile.DiscordId];

            var profileEmbed = new DiscordEmbedBuilder
            {
                Title = $"{member.DisplayName}'s Profile",
                ThumbnailUrl = member.AvatarUrl
            };

            profileEmbed.AddField("Level", profile.Level.ToString());
            profileEmbed.AddField("Xp", profile.Xp.ToString());
            profileEmbed.AddField("Gold", profile.Gold.ToString());
            if (profile.Items.Count > 0)
            {
                profileEmbed.AddField("Items", string.Join(", ", profile.Items.Select(x => x.Item.Name)));
            }

            await ctx.Channel.SendMessageAsync(embed: profileEmbed).ConfigureAwait(false);

            GrantXpViewModel viewModel = await _experienceService.GrantXpAsync(memberId, ctx.Guild.Id, 1).ConfigureAwait(false);

            if(!viewModel.LevelledUp) { return; }

            var levelUpEmbed = new DiscordEmbedBuilder
            {
                Title = $"{member.DisplayName} Is Now Level {viewModel.Profile.Level}",
                ThumbnailUrl = member.AvatarUrl
            };

            await ctx.Channel.SendMessageAsync(embed: levelUpEmbed).ConfigureAwait(false);
        }
    }
}
