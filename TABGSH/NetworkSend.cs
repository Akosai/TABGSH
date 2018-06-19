using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace TABGSH
{
    public static class NetworkSend
    {
        //Doesn't Work
        public static void SendChatMessage(byte id, string message)
        {
            byte[] array = new byte[1 + 2 * message.Length];
            using (MemoryStream memoryStream = new MemoryStream(array))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(id);
                    binaryWriter.Write(message);
                }
            }
            Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>
    {
        {
            19,
            200
        },
        {
            18,
            array
        }
    };
            Landfall.Network.PhotonServerConnector.JOEFNGCJDDA.BIEPCIBBBDO.OpCustom(212, customOpParameters, true);
        }

        public static void SendHealRequest()
        {
            NetworkManager.GetServerHandler().ClientRequestHeal(31);
        }

        public static void DropThatWeaponArea()
        {
                foreach (Player player in PlayerManager.Players)
                {
                    if (PlayerManager.DistanceTo(player) < HackSettings.GetFloat("HACK_NoGunArea_Distance"))
                    {
                        GunManager.TazePlayer(player, false, 100f);
                    GunManager.TazePlayer(player, false, -100f);
                }
                }
        }
    }
}
