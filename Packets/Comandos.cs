using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using InSimDotNet;
using InSimDotNet.Helpers;
using InSimDotNet.Packets;

namespace DL.Packets
{
    public class Comandos
    {
        public static int Pista = 0;
        public static int VotosNao = 0;
        public static int VotosSim = 0;
        public static string QuemIniciou = "None";
        public static void OnPlayerMessage(InSim insim, IS_MSO MSO)
        {
            try
            {
                if (MSO.UserType == UserType.MSO_USER) { Send.ToDiscord("msg", MSO.Msg); }
                if (MSO.UserType == UserType.MSO_PREFIX)
                {
                    string Text = MSO.Msg.Substring(MSO.TextStart, (MSO.Msg.Length - MSO.TextStart));
                    string[] command = Text.Split(' ');
                    command[0] = command[0].ToLower();
                    switch (command[0])
                    {
                        case "!ajuda":
                                Send.ToUCID(MSO.UCID, "^7Página de ajuda:", MessageSound.SND_SILENT);
                                Send.ToUCID(MSO.UCID, "^5!ajuda ^7- Página de ajuda", MessageSound.SND_SILENT);
                                Send.ToUCID(MSO.UCID, "^5!pista ^7- Mostra a pista atual e o Layout", MessageSound.SND_SILENT);
                                Send.ToUCID(MSO.UCID, "^5!admins ^7- Shows a list of all connected admins on the server", MessageSound.SND_SILENT);
                                Send.ToUCID(MSO.UCID, "^5!players ^7- Lista dos jogadores com a conta (para enviar no reportar)", MessageSound.SND_SILENT);
                                Send.ToUCID(MSO.UCID, "^5!reportar [conta] ^7- Reportar jogador (utilize !players para pegar a conta)", MessageSound.SND_SILENT);
                                Send.ToUCID(MSO.UCID, "^5!votar [1-47] ^7- Abrir votação para alterar o layout", MessageSound.SND_SILENT);
                                Send.ToUCID(MSO.UCID, "^5!sim ^7- Votar ^5SIM^7 para alterar o layout", MessageSound.SND_SILENT);
                                Send.ToUCID(MSO.UCID, "^5!nao ^7- Votar ^5NÃO^7 para alterar o layout", MessageSound.SND_SILENT);
                                Send.ToUCID(MSO.UCID, "^5!votacao ^7- Verificar resultado da votação", MessageSound.SND_SILENT);
                                Send.ToUCID(MSO.UCID, "^5!discord ^7- Link do discord", MessageSound.SND_SILENT);
                                break;

                        case "!pista": Send.ToUCID(MSO.UCID, "^7Pista atual ^5" + TrackHelper.GetFullTrackName(InSimCode.TrackName) + " ^7com o layout ^5" + InSimCode.LayoutName, MessageSound.SND_SILENT); break;
                        /*case "!reportar":
                            byte UCID = MSO.UCID;
                            insim.Send(new IS_BTN { Text = "", BStyle = ButtonStyles.ISB_DARK, H = 76, W = 100, T = 48, L = 53, ClickID = 1, UCID = UCID, ReqI = 1 });
                            insim.Send(new IS_BTN { Text = "^7Selecione um jogador para reportar", BStyle = ButtonStyles.ISB_DARK, H = 6, W = 50, T = 40, L = 78, ClickID = 2, UCID = UCID, ReqI = 2 });
                            insim.Send(new IS_BTN { Text = "^1[X]^7 Fechar", BStyle = ButtonStyles.ISB_CLICK, H = 5, W = 32, T = 124, L = 131, ClickID = 8, UCID = UCID, ReqI = 8 });
                            byte T = 49;
                            byte L = 54;
                            //int countT = 0;
                            //int countL = 0;
                            int count = 2;
                            byte ID = 3;
                            // adicionar 5 pra baixo
                            // adicionar 32 pro lado
                            foreach (var CurrentConnection in Dados.PlayerList.Values)
                            {
                                if (CurrentConnection.UName != "")
                                {
                                    switch (ID)
                                    {
                                        case 8:
                                            ID = 9;
                                            break;
                                    }
                                    insim.Send(new IS_BTN { Text = CurrentConnection.PName, BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = T, L = L, ClickID = ID, UCID = UCID, ReqI = ID });
                                    switch (count)
                                    {
                                        case 2:
                                            count += 1;
                                            L = (byte)(L + 32);
                                            break;
                                        case 3:
                                            count += 1;
                                            L = (byte)(L + 32);
                                            break;
                                        case 4:
                                            count += 1;
                                            L = 54;
                                            T = (byte)(T + 5);
                                            break;
                                        case 5:
                                            count += 1;
                                            L = (byte)(L + 32);
                                            break;
                                    }
                                    ID = (byte)(ID + 1);
                                    Console.WriteLine(ID);
                                    Send.ToUCID(MSO.UCID, CurrentConnection.PName + " ^7(" + CurrentConnection.UName + ")", MessageSound.SND_SILENT);
                                }
                            } 
                            /*insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 49, L = 87, ClickID = 4, UCID = UCID, ReqI = 4 });
                            insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 49, L = 119, ClickID = 18, UCID = UCID, ReqI = 18 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 49, L = 119, ClickID = 5, UCID = UCID, ReqI = 5 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 54, L = 119, ClickID = 6, UCID = UCID, ReqI = 6 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 59, L = 119, ClickID = 7, UCID = UCID, ReqI = 7 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 64, L = 119, ClickID = 44, UCID = UCID, ReqI = 44 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 69, L = 119, ClickID = 9, UCID = UCID, ReqI = 9 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 74, L = 119, ClickID = 10, UCID = UCID, ReqI = 10 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 79, L = 119, ClickID = 11, UCID = UCID, ReqI = 11 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 84, L = 119, ClickID = 12, UCID = UCID, ReqI = 12 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 89, L = 119, ClickID = 13, UCID = UCID, ReqI = 13 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 94, L = 119, ClickID = 14, UCID = UCID, ReqI = 14 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 99, L = 119, ClickID = 15, UCID = UCID, ReqI = 15 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 104, L = 119, ClickID = 16, UCID = UCID, ReqI = 16 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 109, L = 119, ClickID = 17, UCID = UCID, ReqI = 17 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 59, L = 54, ClickID = 19, UCID = UCID, ReqI = 19 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 64, L = 54, ClickID = 20, UCID = UCID, ReqI = 20 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 69, L = 54, ClickID = 21, UCID = UCID, ReqI = 21 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 74, L = 54, ClickID = 22, UCID = UCID, ReqI = 22 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 79, L = 54, ClickID = 23, UCID = UCID, ReqI = 23 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 84, L = 54, ClickID = 24, UCID = UCID, ReqI = 24 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 89, L = 54, ClickID = 25, UCID = UCID, ReqI = 25 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 94, L = 54, ClickID = 26, UCID = UCID, ReqI = 26 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 99, L = 54, ClickID = 27, UCID = UCID, ReqI = 27 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 104, L = 54, ClickID = 28, UCID = UCID, ReqI = 28 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 109, L = 54, ClickID = 29, UCID = UCID, ReqI = 29 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 54, L = 87, ClickID = 30, UCID = UCID, ReqI = 30 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 59, L = 87, ClickID = 31, UCID = UCID, ReqI = 31 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 64, L = 87, ClickID = 32, UCID = UCID, ReqI = 32 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 69, L = 87, ClickID = 33, UCID = UCID, ReqI = 33 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 74, L = 87, ClickID = 34, UCID = UCID, ReqI = 34 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 79, L = 87, ClickID = 35, UCID = UCID, ReqI = 35 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 84, L = 87, ClickID = 36, UCID = UCID, ReqI = 36 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 89, L = 87, ClickID = 37, UCID = UCID, ReqI = 37 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 94, L = 87, ClickID = 38, UCID = UCID, ReqI = 38 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 99, L = 87, ClickID = 39, UCID = UCID, ReqI = 39 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 104, L = 87, ClickID = 40, UCID = UCID, ReqI = 40 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 109, L = 87, ClickID = 41, UCID = UCID, ReqI = 41 });
                             insim.Send(new IS_BTN { Text = "teste teste teste teste teste", BStyle = ButtonStyles.ISB_CLICK, H = 6, W = 32, T = 114, L = 54, ClickID = 43, UCID = UCID, ReqI = 43 }); 


                            //if (command.Length == 1) {Send.ToUCID(MSO.UCID, "^7Formato inválido. Utilize: ^5!reportar [conta]", MessageSound.SND_ERROR);break;}
                            //string Username = Text.Remove(0, command[0].Length + 1);
                            break;
                            */

                        case "!players":
                            foreach (var CurrentConnection in Dados.PlayerList.Values)
                            {
                                if (CurrentConnection.UName != "")
                                {
                                    Send.ToUCID(MSO.UCID, CurrentConnection.PName + " ^7(" + CurrentConnection.UName + ")", MessageSound.SND_SILENT);
                                }
                            }
                            break;

                        case "!votar":
                            if (command.Length == 1) { Send.ToUCID(MSO.UCID, "^7Formato inválido ^5!votar [1-47]", MessageSound.SND_ERROR);break; }
                            if (InSimCode.votando == true) { Send.ToUCID(MSO.UCID, "^7Uma votação já está ocorrendo, utilize ^6", MessageSound.SND_ERROR); break; }
                            InSimCode.votando = true;
                            string Pista_String = Text.Remove(0, command[0].Length + 1);
                            Pista = Int32.Parse(Pista_String);
                            if (Pista > 47 ) { Send.ToUCID(MSO.UCID, "^7Escolha um número entre ^5[1-47] ^7EX: ^4!votar 35", MessageSound.SND_ERROR); break; }
                            if (Pista <= 0) { Send.ToUCID(MSO.UCID, "^7Escolha um número entre ^5[1-47] ^7EX: ^4!votar 35", MessageSound.SND_ERROR); break; }
                            DateTime DateNow = DateTime.Now;
                            DateTime DateLater = DateNow.AddMinutes(2);
                            Timer TimerCountDown = new Timer { Interval = 1000, AutoReset = true };
                            QuemIniciou = Dados.PlayerList[MSO.UCID].PName;
                            InSimCode.votos_sim.Add(Dados.PlayerList[MSO.UCID].UName.ToLower());
                            VotosSim = InSimCode.votos_sim.Count();
                            insim.Send(new IS_BTN { Text = "", BStyle = ButtonStyles.ISB_DARK, H = 40, W = 32, T = 91, L = 4, ClickID = 51, UCID = 255, ReqI = 51 });
                            insim.Send(new IS_BTN { Text = "^7Votação:", BStyle = ButtonStyles.ISB_DARK, H = 6, W = 25, T = 84, L = 8, ClickID = 52, UCID = 255, ReqI = 52 });
                            insim.Send(new IS_BTN { Text = "^7Alterar layout para:", BStyle = ButtonStyles.ISB_C1, H = 6, W = 25, T = 93, L = 8, ClickID = 53, UCID = 255, ReqI = 53 });
                            insim.Send(new IS_BTN { Text = $"^7DRIFT^5{Pista}", BStyle = ButtonStyles.ISB_C1, H = 6, W = 25, T = 98, L = 8, ClickID = 54, UCID = 255, ReqI = 54 });
                            insim.Send(new IS_BTN { Text = "^7Quem inicou:", BStyle = ButtonStyles.ISB_C1, H = 6, W = 25, T = 105, L = 8, ClickID = 55, UCID = 255, ReqI = 55 });
                            insim.Send(new IS_BTN { Text = $"^7Votos: Sim ^5({VotosSim}) ^7Não ^5({VotosNao})", BStyle = ButtonStyles.ISB_C1, H = 6, W = 31, T = 120, L = 5, ClickID = 59, UCID = 255, ReqI = 59 });
                            insim.Send(new IS_BTN { Text = $"^7{Dados.PlayerList[MSO.UCID].PName}", BStyle = ButtonStyles.ISB_C1, H = 6, W = 31, T = 112, L = 5, ClickID = 56, UCID = 255, ReqI = 56 });
                            insim.Send(new IS_BTN { Text = $"^7Vote ^5!sim^7 ou ^5!nao^7.", BStyle = ButtonStyles.ISB_C1, H = 6, W = 35, T = 135, L = 3, ClickID = 60, UCID = 255, ReqI = 60 });
                            insim.Send(new IS_BTN { Text = $"^7Tempo restante: ^500:00:00", BStyle = ButtonStyles.ISB_C1, H = 6, W = 35, T = 130, L = 3, ClickID = 57, UCID = 255, ReqI = 57 });
                            TimerCountDown.Elapsed += delegate {
                                try
                                {
                                    DateTime DateNow2 = DateTime.Now;
                                    TimeSpan interval = DateLater - DateNow2;
                                    insim.Send(new IS_BTN { Text = $"^7Tempo restante: ^5{interval.ToString("hh':'mm':'ss")}", BStyle = ButtonStyles.ISB_C1, H = 6, W = 35, T = 130, L = 3, ClickID = 57, UCID = 255, ReqI = 57 });
                                    if (interval.ToString("hh':'mm':'ss") == "00:00:00")
                                    {
                                        //Console.WriteLine("Acabou o tempo :)");
                                        OnVoteFinish();
                                        TimerCountDown.Stop();
                                    }
                                } catch (Exception e)
                                {
                                    Send.ToDiscord("log", "VoteErrorLog: \n```" + e.ToString() + "```");
                                }
                            };
                            TimerCountDown.Start();
                            break;
                        case "!sim":
                            if (!InSimCode.votando) { Send.ToUCID(MSO.UCID, "^5Não^7 há votação no momento.", MessageSound.SND_ERROR); break; }
                            if (InSimCode.votos_sim.Contains(Dados.PlayerList[MSO.UCID].UName.ToLower())) { Send.ToUCID(MSO.UCID, "^6Você já votou ^5SIM^7.", MessageSound.SND_ERROR); break; }
                            if (InSimCode.votos_nao.Contains(Dados.PlayerList[MSO.UCID].UName.ToLower()))
                            {
                                InSimCode.votos_nao.Remove(Dados.PlayerList[MSO.UCID].UName.ToLower());
                                InSimCode.votos_sim.Add(Dados.PlayerList[MSO.UCID].UName.ToLower());
                                Send.ToAll($"^6{Dados.PlayerList[MSO.UCID].PName} alterou para ^5SIM.");
                                VoteUpdateDashboard();
                                break; 
                            }
                            InSimCode.votos_sim.Add(Dados.PlayerList[MSO.UCID].UName.ToLower());
                            Send.ToAll($"^6{Dados.PlayerList[MSO.UCID].PName} votou ^5SIM.");
                            VoteUpdateDashboard();
                            break;
                        case "!nao":
                            if (!InSimCode.votando) { Send.ToUCID(MSO.UCID, "^5Não^7 há votação no momento.", MessageSound.SND_ERROR); break; }
                            if (InSimCode.votos_nao.Contains(Dados.PlayerList[MSO.UCID].UName.ToLower())) { Send.ToUCID(MSO.UCID, "^6Você já votou ^5NÃO^7.", MessageSound.SND_ERROR); break; }
                            if (InSimCode.votos_sim.Contains(Dados.PlayerList[MSO.UCID].UName.ToLower()))
                            {
                                InSimCode.votos_sim.Remove(Dados.PlayerList[MSO.UCID].UName.ToLower());
                                InSimCode.votos_nao.Add(Dados.PlayerList[MSO.UCID].UName.ToLower());
                                Send.ToAll($"^6{Dados.PlayerList[MSO.UCID].PName} alterou para ^5NÃO.");
                                VoteUpdateDashboard();
                                break;
                            }
                            InSimCode.votos_nao.Add(Dados.PlayerList[MSO.UCID].UName.ToLower());
                            Send.ToAll($"^6{Dados.PlayerList[MSO.UCID].PName} votou ^5NÃO.");
                            VoteUpdateDashboard();
                            break;
                        default: 
                            Send.ToUCID(MSO.UCID, "^7Comando desconhecido. Utilize ^5!ajuda^7 para mais informações", MessageSound.SND_ERROR);
                            break;

                    }
                }
                } catch (Exception e)
            {
                Send.ToDiscord("log", "OnPlayerMessage: \n```" + e.ToString() + "```");
            }
        }


        public static void VoteUpdateDashboard()
        {
            VotosNao = InSimCode.votos_nao.Count();
            VotosSim = InSimCode.votos_sim.Count();
            InSimCode.insim.Send(new IS_BTN { Text = $"^7Votos: Sim ^5({VotosSim}) ^7Não ^5({VotosNao})", BStyle = ButtonStyles.ISB_C1, H = 6, W = 31, T = 120, L = 5, ClickID = 59, UCID = 255, ReqI = 59 });
        }

        public static void OnVoteFinish()
        {
            VotosNao = InSimCode.votos_nao.Count();
            VotosSim = InSimCode.votos_sim.Count();
            Send.ToAll(" ");
            Send.ToAll("^5-----------------------------------------");
            Send.ToAll($"^7Votação encerrada!");
            if (VotosNao > VotosSim)
            {
                Send.ToAll("^7Resultado: ^5Não");
                Send.ToAll("^7A lay ^5permanece^7 a mesma.");
            }
            else if (VotosSim > VotosNao)
            {
                Send.ToAll("^7Resultado: ^5Sim");
                Send.ToAll($"^7Alterando layout para DRIFT^5{Pista}^7 em 5 segundos");
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 51, ClickID = 51, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 52, ClickID = 52, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 53, ClickID = 53, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 54, ClickID = 54, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 55, ClickID = 55, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 56, ClickID = 56, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 57, ClickID = 57, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 58, ClickID = 58, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 59, ClickID = 59, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 60, ClickID = 60, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 61, ClickID = 61, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 62, ClickID = 62, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 63, ClickID = 63, SubT = ButtonFunction.BFN_DEL_BTN });
                InSimCode.insim.Send(new IS_BFN { UCID = 255, ReqI = 64, ClickID = 64, SubT = ButtonFunction.BFN_DEL_BTN });
                DateTime DateNow = DateTime.Now;
                DateTime DateLater = DateNow.AddSeconds(4);
                int count = 4;
                Timer timer = new Timer { Interval = 1000, AutoReset = true };
                timer.Interval = 1000;
                timer.Elapsed += delegate {
                    try
                    {
                        DateTime DateNow2 = DateTime.Now;
                        TimeSpan interval = DateLater - DateNow2;
                        Send.ToAll($"^7Alterando layout para DRIFT^5{Pista}^7 em ^5{count}^7 segundos");
                        count -= 1;
                        if (interval.ToString("hh':'mm':'ss") == "00:00:00")
                        {
                            InSimCode.insim.Send($"/axload DRIFT{Pista}");
                            timer.Stop();
                        }
                    }
                    catch (Exception e)
                    {
                        Send.ToDiscord("log", "OnVoteFinish: \n```" + e.ToString() + "```");
                    }
                };
                timer.Start();
            }
            else if (VotosNao == VotosSim)
            {
                Send.ToAll("Resultado: Não");
                Send.ToAll("A lay permanece a mesma.");
            }
            Send.ToAll("-----------------------------------------");
            Send.ToAll(" ");
            InSimCode.votando = false;
            InSimCode.votos_sim.Clear();
            InSimCode.votos_nao.Clear();
        }

    }
}
