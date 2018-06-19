using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace TABGSH
{


    public static class ItemManager
    {
        public enum PickupType //Pickup.JGHOAEDPDBB
        {
            Health,
            // Token: 0x040003F8 RID: 1016
            Grenade,
            // Token: 0x040003F9 RID: 1017
            WeaponAttatchment,
            // Token: 0x040003FA RID: 1018
            Weapon,
            // Token: 0x040003FB RID: 1019
            OtherConsumable,
            // Token: 0x040003FC RID: 1020
            Armor,
            // Token: 0x040003FD RID: 1021
            Ammo
        }

        public static IEnumerable<Pickup> Pickups;
        public static void UpdatePickupList()
        {
            Pickups = UnityEngine.Object.FindObjectsOfType<Pickup>();
        }
        public static Color[] PickupColor = new Color[]
        {
            HackSettings.GetColor("ESP_Item_Color_Health"),
            HackSettings.GetColor("ESP_Item_Color_Grenade"),
            HackSettings.GetColor("ESP_Item_Color_WeaponAttatchment"),
            HackSettings.GetColor("ESP_Item_Color_Weapon"),
            HackSettings.GetColor("ESP_Item_Color_OtherConsumable"),
            HackSettings.GetColor("ESP_Item_Color_Armor"),
            HackSettings.GetColor("ESP_Item_Color_Ammo"),
            HackSettings.GetColor("ESP_Item_Color_Unknown"),
        };
        public static ItemDataEntry GetItemDataEntry(int idKey)
        {
            return LootDatabase.Instance.HMEFDENEPFC(idKey);
        }
        public static bool IsValidPickup(this Pickup pickup)
        {
            if (pickup == null)
                return false;
            return true;
        }

        public static void PickupFar(this Pickup pickup) //
        {
            NetworkManager.GetServerHandler().SendPlayerUpdate(pickup.transform.position, Vector2.zero, pickup.transform.position, 1);
            PlayerManager.GetLocalPlayer().m_interactionHandler.PickUp(pickup);
        }

        public static List<ItemDataEntry> GetItemList(Pickup.JGHOAEDPDBB desiredType)
        {
            List<ItemDataEntry> ItemDataEntryList = new List<ItemDataEntry>();
            for (int i = 0; i < LootDatabase.Instance.ItemCount; i++)
            {
                var cItem = GetItemDataEntry(i);
                if (cItem.pickup.weaponType == desiredType)
                {
                    ItemDataEntryList.Add(cItem);
                }
            }
            return ItemDataEntryList;
        }
        public static void AddItem(int itemID)
        {
            if (itemID > LootDatabase.Instance.ItemCount)
                itemID = LootDatabase.Instance.ItemCount;
            if (itemID < 0)
                itemID = 0;

            Pickup spawnedItem = UnityEngine.Object.Instantiate<Pickup>(GetItemDataEntry(itemID).pickup);
            spawnedItem.canInteract = true;
            if (spawnedItem.weaponType == Pickup.JGHOAEDPDBB.Ammo)
                spawnedItem.m_quanitity = 999;
            else
                spawnedItem.m_quanitity = 1;
            PlayerManager.GetLocalPlayer().m_interactionHandler.PickUp(spawnedItem, true, Pickup.ODBCKHOEDEF.None, -1);
        }
        public static Color GetItemTypeColor(Pickup.JGHOAEDPDBB type)
        {
            if ((int)type < 0)
                type = Pickup.JGHOAEDPDBB.Health;
            if ((int)type > (PickupColor.Length - 1))
                type = Pickup.JGHOAEDPDBB.Health;
            return PickupColor[(int)type];
        }
    }
}
