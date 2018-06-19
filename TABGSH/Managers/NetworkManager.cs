using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Landfall.Network;
using UnityEngine;
namespace TABGSH
{
    public static class NetworkManager
    {
        private static PhotonServerHandler m_photonServerHandler;
        private static PhotonServerConnector m_photonServerConnector;
        public static PhotonServerHandler GetServerHandler()
        {
            if (m_photonServerHandler == null)
                m_photonServerHandler = GameObject.FindObjectOfType<PhotonServerHandler>();
            if (!m_photonServerHandler)
                return null;
            return m_photonServerHandler;
        }
        public static PhotonServerConnector GetServerConnector()
        {
            if (m_photonServerConnector == null)
                m_photonServerConnector = GameObject.FindObjectOfType<PhotonServerConnector>();
            if (!m_photonServerConnector)
                return null;
            return m_photonServerConnector;
        }
        public static List<GLDCILHFINE> GetGLDs()
        {
            List<GLDCILHFINE> Shit = (List<GLDCILHFINE>)typeof(PhotonServerHandler).GetField("MDCFNBBLMGA", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(GetServerHandler());
            return Shit;
        }
        public static void ChangeName(string name)
        {
            Log.AddLogText("Changed name to: " + name);
            typeof(PhotonServerConnector).GetMethod("GLDEKFKPEPD", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).Invoke(null, new object[] { name });
            GLDCILHFINE shitty = PlayerManager.GetGLD(PlayerManager.GetLocalPlayer());
            typeof(KPGBJIOJKIG).GetMethod("GLDEKFKPEPD", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(shitty, new object[] { name });
            /*
            GLDCILHFINE shitty = PlayerManager.GetGLD(PlayerManager.GetLocalPlayer());
            typeof(KPGBJIOJKIG).GetMethod("GLDEKFKPEPD", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(shitty, new object[] { name });
            typeof(PhotonServerConnector).GetMethod("GLDEKFKPEPD", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).Invoke(null, new object[] {name});*/
        }
    }

}
