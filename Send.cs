using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Webhook;
using InSimDotNet;
using InSimDotNet.Packets;

namespace DL.Packets
{
    internal class Send
    {
        public static void ToDiscord(string canal, string text)
        {
            string url = ""; 
            switch (canal)
            {
                case "log":
                    url = "https://discord.com/api/webhooks/1002323687220969573/4dGL9tcnJVGoMwRb-akeuoUa9p7QbIbG4I8UqPKwND_KI0p4HzJdXG14nWSpbtiPljz9";
                    break;
                case "msg":
                    string[] colors = {
                    "^0",
                    "^1",
                    "^2",
                    "^3",
                    "^4",
                    "^5",
                    "^6",
                    "^7",
                    "^8"};
                    foreach (var color in colors)
                    {
                        text = text.Replace(color, "");
                    }
                    url = "https://discord.com/api/webhooks/1001582661917224970/JkeIxp5rZJ9qpQLZ09tlGEcZI677sUZjXNUjCHb_cES6enSqshbLDUOUrquDojFECM94";
                    break;
            }
            try
            {
                DiscordWebhook hook = new DiscordWebhook();
                hook.Url = url;
                DiscordMessage message = new DiscordMessage();
                message.Content = $"{text}";
                hook.Send(message);
            } catch (Exception e)
            {
                Console.WriteLine("Erro webhook:" + e.ToString());
            }
        }
        public static void ToAll(string Message) { InSimCode.insim.Send("/msg " + Message); }

        public static void ToUCID(byte UCID, string Message, MessageSound som)
        {
            InSimCode.insim.Send(new IS_MTC { UCID = UCID, Msg = Message, Sound = som });
        }
    }
}
