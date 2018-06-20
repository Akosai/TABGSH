using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Landfall.Network;
using System.IO;

namespace TABGSH
{
    public static class PlayerManager
    {
        public enum PlayerBone
        {
            Armature,
            Hip,
            Torso,
            Neck,
            Head,
            Leg_Left,
            Leg_Right,
            Knee_Left,
            Knee_Right,
            Foot_Left,
            Foot_Right,
            Arm_Left,
            Arm_Right,
            Elbow_Left,
            Elbow_Right,
            Wrist_Left,
            Wrist_Right,
            Hand_Left,
            Hand_Right

        }
        public static IEnumerable<Player> Players;
        public static Player NearestPlayer;
        public static void UpdatePlayerList()
        {
            Players = UnityEngine.Object.FindObjectsOfType<Player>();
            NearestPlayer = GetNearestPlayer(HackSettings.GetState("PM_IgnoreTeam"));
        }
        private static PhotonServerHandler m_photonServerHandler;
        public static bool IsTeammate(this Player player)
        {
            if (m_photonServerHandler == null)
                m_photonServerHandler = NetworkManager.GetServerHandler();
            if (!m_photonServerHandler)
                return false;
            IEnumerable<GLDCILHFINE> TeamGDL = m_photonServerHandler.GetTeamMates(false, false);
            foreach (var Mitglied in TeamGDL)
            {
                if (Mitglied.DIEMKLODHIA == player.gameObject)
                    return true;
            }
            return false;
        }
        public static Player GetLocalPlayer()
        {
            return Player.localPlayer;
        }
        public static void KillPlayer(this Player target)
        {
            if (target)
            {
                target.headDamagable.NetworkDamage(100);
                target.bodyDamagable.NetworkDamage(100);
                target.m_playerDeath.health = 0;
                target.m_playerDeath.DamagePlayer(100, target.transform.position);
                target.m_playerDeath.TakeDamage(target.transform.position * 999, target.transform.position * 999);
            }
        }
        public static bool IsDummy(this Player player)
        {
            return player.m_hasControl.isDummy;
        }
        public static bool IsPlayerValid(this Player refPlayer, bool teamInvalid = true, bool myselfInvalid = true)
        {
            if (!refPlayer)
                return false;

            if (teamInvalid && IsTeammate(refPlayer))
                return false;

            if (myselfInvalid && refPlayer == GetLocalPlayer())
                return false;

            PlayerDeath playerDeath = refPlayer.m_playerDeath;
            if (!playerDeath)
                return false;

            if (playerDeath.dead)
                return false;
            if (HackSettings.GetState("PM_IgnoreDowned"))
                if (playerDeath.isDown)
                    return false;

            return true;
        }
        public static float DistanceTo(Transform transform)
        {
            return Vector3.Distance(GetLocalPlayer().GetPosition(), transform.position);
        }
        public static Vector3 GetPosition(this Player player)
        {
            if (!player.IsPlayerValid(false, false))
                return Vector3.zero;
            var pHip = GetLocalPlayer().GetComponentInChildren<Hip>();
            if (pHip)
                return pHip.transform.position;

            var pTorso = GetLocalPlayer().GetComponentInChildren<Torso>();
            if (pTorso)
                return pTorso.transform.position;

            return Vector3.zero;
        }
        public static float DistanceTo(Player player)
        {
            Hip pHip = player.GetComponentInChildren<Hip>();
            if (!pHip)
                return Vector3.Distance(GetLocalPlayer().GetPosition(), player.transform.position);
            return Vector3.Distance(GetLocalPlayer().GetPosition(), player.GetComponentInChildren<Hip>().transform.position);
        }
        public static string GetName(this Player player)
        {
            if (!IsPlayerValid(player, false, false))
                return "Unknown";
            if (player.IsDummy())
                return "Dummy";
            GLDCILHFINE GLD = GetGLD(player);
            return GLD.HHLCJCDEKGL;
        }
        public static GLDCILHFINE GetGLD(this Player player)
        {
            if (!IsPlayerValid(player, false, false))
                return null;
            var netPlayer = player.GetComponent<Landfall.Network.NetworkPlayer>();
            if (!netPlayer)
                return null;
            return netPlayer.GetGayField<GLDCILHFINE>("AGINOLMILFF");
        }

        public static byte GetId(this Player player)
        {
            return player.GetComponent<Landfall.Network.NetworkPlayer>().GetGayField<byte>("OCDCPIGJOLN");
        }
        public static Player GetNearestPlayer(bool ignoreTeam = true)
        {
            float lowestDistance = HackSettings.GetFloat("PM_NearestPlayer_Distance");
            Player target = null;
            foreach (Player c_Player in Players)
            {
                if (!IsPlayerValid(c_Player, ignoreTeam))
                    continue;
                float distance = (float)Math.Floor(DistanceTo(c_Player));
                if (distance <= lowestDistance)
                {
                    lowestDistance = distance;
                    target = c_Player;
                }

            }
            return target;
        }
        
        public enum HitLayers //Thanks to UC-IDontReallyKnow for the layers
        {
            TransparentFX,
            Raycast,
            unk1,
            Water,
            UI,
            unk2,
            unk3,
            HideFromSelf,
            Map,
            Stickys,
            Props,
            Terrain,
            LocalPlayer,
            Weapon01,
            Weapon02,
            Weapon03,
            ScreenParticles,
            Road,
            Wheel,
            DontRender,
            DontCollide,
            DontCollideWithLocalPlayer,
            Dropper,
            DroppedRagdoll,
            Armor,
            Helmet,
            Bullets
        }
        
        public static bool IsVisible(this Player player)
        {
            RaycastHit hit;

            Transform playerTransform = PlayerManager.GetLocalPlayer().m_head.transform;

            Vector3 Heading = (player.m_head.transform.position - playerTransform.position);
            Vector3 rayDirection = Heading / Heading.magnitude;
            int layermask = ~((1 << (int)HitLayers.LocalPlayer) | 
                (1 << (int)HitLayers.Weapon01) | 
                (1 << (int)HitLayers.Weapon02) | 
                (1 << (int)HitLayers.Weapon03) | 
                (1 << (int)HitLayers.DontCollide) |
                (1 << (int)HitLayers.DontCollideWithLocalPlayer));
            Ray ray = new Ray(playerTransform.position, rayDirection);
            if (Physics.Raycast(ray, out hit, 9999f,layermask))
            {
                if (hit.collider.attachedRigidbody.GetComponent<Head>().transform.root.GetComponent<Player>() == player)
                    return true;
            }

            return false;
        }

    }

}
