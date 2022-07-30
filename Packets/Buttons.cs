using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InSimDotNet;
using InSimDotNet.Packets;

namespace DL.Packets
{
    public class Buttons
    {
        public static void OnButtonClicked(InSim insim, IS_BTC BTC)
        {
            byte UCID = BTC.UCID;
            try
            {
                if (BTC.ClickID == 8)
                {
                    for (byte i = 0; i < 150; i++)
                    {
                        byte ReqI = i;
                        insim.Send(new IS_BFN { ReqI = ReqI, UCID = UCID, ClickID = i, SubT = ButtonFunction.BFN_DEL_BTN });
                    }
                }
            }
            catch (Exception e) { Send.ToDiscord("log", "OnButtonClicked\n```" + e.ToString() + "```"); }
        }
    }
}
