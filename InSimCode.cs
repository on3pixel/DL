using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InSimDotNet;
using InSimDotNet.Packets;
using DL.Packets;
namespace DL
{
    public class InSimCode
    {
        public static InSim insim;
        public static string LayoutName = "None";
        public static string TrackName = "None";
        public static bool votando = false;
        public static List<string> votos_sim = new List<string>();
        public static List<string> votos_nao = new List<string>();

        public InSimCode () {
            try
            {

                insim = new InSim();

                insim.Initialize(new InSimSettings {
                    Host = "IP", #CHANGE ME
                    Port = 29999, #CHANGE ME
                    Admin = "", #CHANGE ME
                    Flags = InSimFlags.ISF_MCI | InSimFlags.ISF_MSO_COLS,
                    Interval = 500,
                    Prefix = '!'
                });
                insim.InSimError += OnInSimError;
            } catch (InSimException e) {

                Send.ToDiscord("log", "InSim Connection error: \n```" + e.ToString() + "```");
            } finally
                {
                    if (insim.IsConnected)
                    {
                        insim.Bind<IS_NCN>(Dados.OnPlayerConnect);
                        insim.Bind<IS_CNL>(Dados.OnPlayerDisconnect);
                        insim.Bind<IS_NPL>(Dados.OnPlayerJoinRace);
                        insim.Bind<IS_PLL>(Dados.OnPlayerSpectate);
                        insim.Bind<IS_PLP>(Dados.OnPlayerPit);
                        insim.Bind<IS_MSO>(Comandos.OnPlayerMessage);
                        insim.Bind<IS_STA>(Track.OnStateChange);
                        insim.Bind<IS_AXI>(Track.OnAutocrossInformation);
                        insim.Bind<IS_TINY>(Track.OnTinyReceived);
                        insim.Bind<IS_BTC>(Buttons.OnButtonClicked);
                        insim.Bind<IS_CPR>(Dados.OnPlayerRename);
                        insim.Bind<IS_TOC>(Dados.OnPlayerTakeOver);
                        insim.Send(new IS_TINY { SubT = TinyType.TINY_SST, ReqI = 1 });//Used for Track/Layout names
                        insim.Send(new IS_TINY { SubT = TinyType.TINY_NCN, ReqI = 2 });//Used for Connections
                        insim.Send(new IS_TINY { SubT = TinyType.TINY_NPL, ReqI = 3 });//Used for Player in track
                        insim.Send(new IS_TINY { SubT = TinyType.TINY_MCI, ReqI = 4 });//Used for Player in track
                        insim.Send(new IS_BTN { Text = "^7Copyright © ^7DriftLife^5ﾂ^7tasty  driftlife.com.br", BStyle = ButtonStyles.ISB_LEFT, H = 5, W = 60, T = 194, L = 80, UCID = 255, ClickID = 200, ReqI = 200});
                    }
                }
            Console.ReadLine();
        }

        private void OnInSimError(object sender, InSimErrorEventArgs e)
        {
            Send.ToDiscord("log", "OnInSimError: \n```" + e.ToString() + "```");
        }
    }
}
