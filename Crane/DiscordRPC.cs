using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using System;
using System.Windows.Forms;

namespace Crane
{
    public partial class MainForm : Form
    {
        DiscordRpcClient client;

        private void InitializeClient()
        {
            client = new DiscordRpcClient("id here");

            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            client.OnReady += Client_OnReady;

            client.OnPresenceUpdate += Client_OnPresenceUpdate;

            client.Initialize();

            ResetPresence();
        }

        private void UpdatePresence()
        {
            client.UpdateDetails("Phase " + phase + " with Score " + score);
            client.UpdateState("Playing " + (restrc ? "restricted" : "free"));
        }

        private void ResetPresence()
        {
            client.SetPresence(new RichPresence()
            {
                Details = "Phase " + phase + " with Score " + score,
                State = "Playing " + (restrc ? "restricted" : "free"),
                Assets = new Assets()
                {
                    LargeImageKey = "crane",
                    LargeImageText = "http://chimkenserver.tk/stuff/",
                    SmallImageKey = "None"
                }
            });
            client.UpdateStartTime();
        }

        private void DeinitializeClient()
        {
            client.Dispose();
        }

        private void Client_OnPresenceUpdate(object sender, PresenceMessage args)
        {
            Console.WriteLine("Received Update! {0}", args.Presence);
        }

        private void Client_OnReady(object sender, ReadyMessage args)
        {
            Console.WriteLine("Received Ready from user {0}", args.User.Username);
        }
    }
}
