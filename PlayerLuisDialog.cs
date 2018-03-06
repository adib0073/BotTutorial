using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"], 
            ConfigurationManager.AppSettings["LuisAPIKey"], 
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            var response = context.MakeMessage();
            response.Text = "You are using the MP3 Player Skill! How can I help?";
            response.Speak = response.Text;
            response.InputHint = InputHints.ExpectingInput;
            await context.PostAsync(response);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Player")]
        public async Task MixtapeIntent(IDialogContext context, LuisResult result)
        {
            var response = context.MakeMessage();
            response.Text = "Welcome to MP3 Player! Let's find a song to play.";
            response.Speak = response.Text;
            response.InputHint = InputHints.IgnoringInput;
            await context.PostAsync(response);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Clear")]
        public async Task PlaySongIntent(IDialogContext context, LuisResult result)
        {
            var response = context.MakeMessage();
            response.Text = "Are you sure you want to delete this?";
            response.Speak = response.Text;
            response.InputHint = InputHints.AcceptingInput;
            await context.PostAsync(response);
            context.Wait(MessageReceived);
        }
    }
}
