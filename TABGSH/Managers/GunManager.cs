using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TABGSH
{
    public static class GunManager
    {
        public static void DoAimbot(Player target)
        {
            if (!PlayerManager.IsPlayerValid(target))
                return;
            var CurrentGun = GunManager.GetCurrentGun();
            if (CurrentGun)
            {
                //Renderer getRenderer = target.m_head.GetComponentInChildren<Renderer>();
                //if (getRenderer.isVisible)
                //{
                Vector3 headPos = target.m_head.transform.position;
                Transform gunTransform = CurrentGun.GetGayField<Transform>("LMFIBANOEHH");
                Vector3 targetPos = headPos;/*+ BulletDrop + movementprediction*/;
                gunTransform.LookAt(targetPos);
                //}
            }
        }
        public static Gun GetCurrentGun(Player player = null)
        {
            if (player == null)
                player = PlayerManager.GetLocalPlayer();
            else
                if (!PlayerManager.IsPlayerValid(player, false, false))
                return null;
            Pickup.ODBCKHOEDEF SelectedSlot = player.m_weaponHandler.LJPNCHJMEAI;
            Gun CurrentGun = player.m_inventory.IJIKCMPPFOA(SelectedSlot).GetComponent<Pickup>().PHDMFNFACCC;
            if (!CurrentGun)
                return null;
            return CurrentGun;
        }
        public static Recoil Component_Recoil(this Gun gun)
        {
            return gun.GetComponent<Recoil>();
        }
        public static void TazePlayer(Player player, bool tazeFriends,float duration)
        {
            if (!player.IsPlayerValid(tazeFriends, true))
                return;
            NetworkManager.GetServerHandler().ClientDoEffect(player.GetId(), MKABDBMIHKD.Tase, duration);
        }
        /*Untested function*/
        public static void UnTazePlayer(Player player)
        {
            if (!player.IsPlayerValid(false,false))
                return;
            NetworkManager.GetServerHandler().ClientDoEffect(player.GetId(), MKABDBMIHKD.Tase, -99999f);
        }
    }
}
