using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InSimDotNet;
using InSimDotNet.Packets;
using InSimDotNet.Helpers;

namespace DL.Packets
{
    public class Dados
    {
        public class Conexao
        {
            public byte UCID;
            public byte PLID;
            public string UName;
            public string PName;
            public bool isAdmin;
        }
        public class Race
        {
            public byte UCID;
            public byte PLID;
            public string UName;
            public string PName;
            public string CName;
            public string SName;
        }
        public static Dictionary<byte, Conexao> PlayerList = new Dictionary<byte, Conexao>();
        public static Dictionary<byte, Race> RaceList = new Dictionary<byte, Race>();

        public static void OnPlayerConnect(InSim insim, IS_NCN NCN)
        {
            try
            {
                if (PlayerList.ContainsKey(NCN.UCID)) return;
                PlayerList.Add(NCN.UCID, new Conexao
                {
                    UCID = NCN.UCID,
                    PLID = 0,
                    PName = NCN.PName,
                    UName = NCN.UName,
                    isAdmin = NCN.Admin
                });
                if (NCN.ReqI == 0)
                {

                        byte UCID = NCN.UCID;
                        Send.ToUCID(NCN.UCID, "^7Bem-vindo ^2" + NCN.PName, MessageSound.SND_SYSMESSAGE);
                        Send.ToUCID(NCN.UCID, "^7Pista atual ^5" + TrackHelper.GetFullTrackName(InSimCode.TrackName) + " ^7com o layout ^5" + InSimCode.LayoutName, MessageSound.SND_SYSMESSAGE);
                        Send.ToUCID(NCN.UCID, "^7Sistema de votação para troca de layout", MessageSound.SND_SYSMESSAGE);
                        Send.ToUCID(NCN.UCID, "^7Utilize ^5!votar [NUMERO] ^7(1-47)", MessageSound.SND_SYSMESSAGE);
                        insim.Send(new IS_BTN { Text = "", BStyle = ButtonStyles.ISB_DARK, H = 40, W = 60, T = 62, L = 72, ClickID = 1, UCID = UCID, ReqI = 1 });
                        insim.Send(new IS_BTN { Text = "^7Bem vindo ao DriftLife :)", BStyle = ButtonStyles.ISB_C1, H = 6, W = 30, T = 63, L = 88, ClickID = 2, UCID = UCID, ReqI = 2 });
                        insim.Send(new IS_BTN { Text = "^7Regras:", BStyle = ButtonStyles.ISB_C1, H = 6, W = 30, T = 68, L = 87, ClickID = 3, UCID = UCID, ReqI = 3 });
                        insim.Send(new IS_BTN { Text = "^51. ^7Respeite quem está na pista ao sair do pit", BStyle = ButtonStyles.ISB_C1, H = 5, W = 60, T = 74, L = 72, ClickID = 4, UCID = UCID, ReqI = 4 });
                        insim.Send(new IS_BTN { Text = "^52. ^7Ajude players novos, você já foi novo também.", BStyle = ButtonStyles.ISB_C1, H = 5, W = 60, T = 78, L = 72, ClickID = 5, UCID = UCID, ReqI = 5 });
                        insim.Send(new IS_BTN { Text = "^7Reporte jogadores com o comando ^5!reportar^7.", BStyle = ButtonStyles.ISB_C1, H = 5, W = 60, T = 89, L = 72, ClickID = 6, UCID = UCID, ReqI = 6 });
                        insim.Send(new IS_BTN { Text = "^7Altere o layout utilizando ^5!votar [1-47]^7.", BStyle = ButtonStyles.ISB_C1, H = 5, W = 60, T = 94, L = 72, ClickID = 9, UCID = UCID, ReqI = 9 });
                        insim.Send(new IS_BTN { Text = "^53. ^7Trate os outros com respeito.", BStyle = ButtonStyles.ISB_C1, H = 5, W = 60, T = 82, L = 72, ClickID = 7, UCID = UCID, ReqI = 7 });
                        insim.Send(new IS_BTN { Text = "^1[X] ^7Fechar", BStyle = ButtonStyles.ISB_CLICK, H = 5, W = 25, T = 102, L = 113, ClickID = 8, UCID = UCID, ReqI = 8 });
                        insim.Send(new IS_BTN { Text = "^7Copyright © ^7DriftLife^5ﾂ^7tasty  driftlife.com.br", BStyle = ButtonStyles.ISB_LEFT, H = 5, W = 60, T = 194, L = 80, UCID = 255, ClickID = 200, ReqI = 200 });
                }
            } catch (Exception e)
            {
                Send.ToDiscord("log", "OnPlayerConnect: \n```" + e.ToString() + "```");
            }
        }
        public static void OnPlayerDisconnect(InSim insim, IS_CNL CNL)
        {
            try
            {
                if (RaceList.ContainsKey(PlayerList[CNL.UCID].PLID))
                {
                    RaceList.Remove(PlayerList[CNL.UCID].PLID);
                }
                if (!PlayerList.ContainsKey(CNL.UCID)) return;
                PlayerList.Remove(CNL.UCID);
            } catch (Exception e)
            {
                Send.ToDiscord("log", "OnPlayerDisconnect: \n```" + e.ToString() + "```");
            }
        }

        public static void OnPlayerJoinRace(InSim insim, IS_NPL NPL)
        {
            try
            {
                if (RaceList.ContainsKey(NPL.PLID)) return;
                RaceList.Add(NPL.PLID, new Race
                {
                    PLID = NPL.PLID,
                    UCID = NPL.UCID,
                    PName = NPL.PName,
                    UName = PlayerList[NPL.UCID].UName,
                    CName = NPL.CName,
                    SName = NPL.SName
                });
                PlayerList[NPL.UCID].PLID = NPL.PLID;
                insim.Send(new IS_BTN { Text = "^7Copyright © ^7DriftLife^5ﾂ^7tasty  driftlife.com.br", BStyle = ButtonStyles.ISB_LEFT, H = 5, W = 60, T = 194, L = 80, UCID = 255, ClickID = 200, ReqI = 200 });
            } catch (Exception e)
            {
                Send.ToDiscord("log", "OnPlayerJoinRace: \n```" + e.ToString() + "```");
            }
        }

        public static void OnPlayerSpectate(InSim insim, IS_PLL PLL)
        {
            RemovePLID(PLL.PLID);
            insim.Send(new IS_BTN { Text = "^7Copyright © ^7DriftLife^5ﾂ^7tasty  driftlife.com.br", BStyle = ButtonStyles.ISB_LEFT, H = 5, W = 60, T = 194, L = 80, UCID = 255, ClickID = 200, ReqI = 200 });
        }

        public static void OnPlayerPit(InSim insim, IS_PLP PLP)
        {
            RemovePLID(PLP.PLID);
            insim.Send(new IS_BTN { Text = "^7Copyright © ^7DriftLife^5ﾂ^7tasty  driftlife.com.br", BStyle = ButtonStyles.ISB_LEFT, H = 5, W = 60, T = 194, L = 80, UCID = 255, ClickID = 200, ReqI = 200 });
        }

        public static void OnPlayerRename(InSim insim, IS_CPR CPR)
        {
            try
            {
                Dados.PlayerList[CPR.UCID].PName = CPR.PName;
                foreach (var CurrentPlayer in Dados.PlayerList.Values) if (CurrentPlayer.UCID == CPR.UCID) CurrentPlayer.PName = CPR.PName;//make sure your code is AFTER this one
            }
            catch (Exception EX) { Send.ToDiscord("log", "OnplayerRename: \n```" + EX.ToString() + "```"); }
        }

        public static void OnPlayerTakeOver(InSim insim, IS_TOC TOC)
        {
            try
            {
                Dados.PlayerList[TOC.PLID].UCID = TOC.NewUCID;
                Dados.PlayerList[TOC.PLID].PName = Dados.PlayerList[TOC.NewUCID].PName;//make sure your code is AFTER this one
            }
            catch (Exception e) { Send.ToDiscord("log", "OnPlayerTakeOver: \n```" + e.ToString() + "```"); }
        }
        public static void RemovePLID(byte PLID)
        {
            try
            {
                if (!RaceList.ContainsKey(PLID)) return;
                RaceList.Remove(PLID);

            }
            catch (Exception e)
            {
                Send.ToDiscord("log", "RemovePLID: \n```" + e.ToString() + "```");
            }

        }
    }
}
