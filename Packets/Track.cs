using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InSimDotNet;
using InSimDotNet.Packets;

namespace DL.Packets
{
    public class Track
    {
		public static void OnStateChange(InSim insim, IS_STA STA)
		{
			try
			{
				if (InSimCode.TrackName != STA.Track)
				{
					InSimCode.TrackName = STA.Track;
					insim.Send(new IS_TINY { SubT = TinyType.TINY_AXI, ReqI = 255 });
				}
			}
			catch (Exception EX) { Send.ToDiscord("log","OnStateChange: \n" + EX.ToString()); }
		}

		public static void OnAutocrossInformation(InSim insim, IS_AXI AXI)
		{
			try
			{
				if (AXI.NumO != 0)
				{
					InSimCode.LayoutName = AXI.LName;
					if (AXI.ReqI == 0)
					{
						Send.ToAll("^7O Layout ^5" + InSimCode.LayoutName + " ^7foi carregado");
						insim.Send("/pit_all");
					}

				}
			}
			catch (Exception EX) { Send.ToDiscord("log", "OnAutocrossInformation: \n" + EX.ToString()); }
		}

		public static void OnTinyReceived(InSim insim, IS_TINY TINY)
		{
			if (TINY.SubT == TinyType.TINY_AXC)
			{
				try
				{
					if (InSimCode.LayoutName != "None") Send.ToAll("^7O Layout ^5" + InSimCode.LayoutName + " ^7foi limpo");
					InSimCode.LayoutName = "None";
				}
				catch (Exception EX) { Send.ToDiscord("log", "OnTinyReceived: \n" + EX.ToString()); }
			}
		}
	}
}
