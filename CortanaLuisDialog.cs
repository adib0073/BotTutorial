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
            response.Text = "Welcome to the MP3 skill.";

            AudioCard card = new AudioCard
            {
                Media = new MediaUrl[] { new MediaUrl("http://msconnect-cortanaskill.azurewebsites.net/tada.mp3") }
            };

            response.Attachments.Add(card.ToAttachment());

            response.InputHint = InputHints.IgnoringInput;
            await context.PostAsync(response);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Player")]
        public async Task MixtapeIntent(IDialogContext context, LuisResult result)
        {
            var response = context.MakeMessage();

            response.Text = "Let's find a song for you to play.";
            
            //for modifying how Cortana speaks
            response.Speak = @"<speak><prosody rate=""fast"">You are using the MP3 Player Skill. <audio src=""http://msconnect-cortanaskill.azurewebsites.net/tada.mp3""/>How can I help?</prosody></speak> ";
            //For attaching audio cards
            AudioCard card = new AudioCard
            {
                Media = new MediaUrl[] { new MediaUrl("http://www.mediacollege.com/audio/tone/files/100Hz_44100Hz_16bit_05sec.mp3") }
            };

            response.Attachments.Add(card.ToAttachment());


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
