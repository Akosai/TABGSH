using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace TABGSH
{
    public static class GuiMenu
    {

        public static void MainMenu_Index0()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Menu 0 - Hotkeys");
            HackSettings.SetState("GUI_Show", GUILayout.Toggle(HackSettings.GetState("GUI_Show"), "(F1) Toggle Menu"));
            HackSettings.SetState("ESP_Player_Draw", GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw"), "(F2) Draw Players"));
            HackSettings.SetState("HACK_Gunhack", GUILayout.Toggle(HackSettings.GetState("HACK_Gunhack"), "(F4) Toggle Gunhack"));
            HackSettings.SetState("HACK_SpamHeal", GUILayout.Toggle(HackSettings.GetState("HACK_SpamHeal"), "(F6) Toggle DemiGod"));
            HackSettings.SetState("HACK_SendFakePosition", GUILayout.Toggle(HackSettings.GetState("HACK_SendFakePosition"), "(F9) Toggle SendFakePosition"));
            HackSettings.SetState("Aimbot_Silent", GUILayout.Toggle(HackSettings.GetState("Aimbot_Silent"), "Toggle Silent Aimbot"));
            HackSettings.SetState("HACK_NoGunArea", GUILayout.Toggle(HackSettings.GetState("HACK_NoGunArea"), "Toggle No Gun Area"));
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Change name"))
            {
                NetworkManager.ChangeName(HackSettings.GetString("FakeName"));
            }
            HackSettings.SetString("FakeName", GUILayout.TextField(HackSettings.GetString("FakeName")));
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        public static void MainMenu_Index1()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Menu 1 - ESP Settings");
            HackSettings.SetState("ESP_Draw", GUILayout.Toggle(HackSettings.GetState("ESP_Draw"), "Enable ESP"));
            GUILayout.Label("Player Settings");
            HackSettings.SetState("ESP_Player_Draw", GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw"), "(F2) Draw Players"));
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Draw Distance({HackSettings.GetFloat("ESP_Player_Distance")}):");
            HackSettings.SetFloat("ESP_Player_Distance", Mathf.Floor(GUILayout.HorizontalSlider(HackSettings.GetFloat("ESP_Player_Distance"), 100, 2000)));
            GUILayout.EndHorizontal();
            
            HackSettings.SetState("ESP_Player_Draw_Distance", GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw_Distance"), "Draw Distance"));
            HackSettings.SetState("ESP_Player_Draw_Health", GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw_Health"), "Draw Health"));
            HackSettings.SetState("ESP_Player_Draw_Name", GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw_Name"), "Draw Name"));
            HackSettings.SetState("ESP_Player_Draw_Glow", GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw_Glow"), "Draw Glow"));
            HackSettings.SetState("ESP_Player_Draw_2DBox", GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw_2DBox"), "Draw 2DBox"));
            HackSettings.SetState("ESP_Player_Draw_HeadX", GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw_HeadX"), "Draw Head Marker"));
            HackSettings.SetState("ESP_Player_Draw_Nearest", GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw_Nearest"), "Draw Nearest Player"));
            HackSettings.SetState("ESP_Player_Draw_Skeleton", GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw_Skeleton"), "Draw Skeleton"));

            GUILayout.Label("Item Settings");
            HackSettings.SetState("ESP_Item_Draw", GUILayout.Toggle(HackSettings.GetState("ESP_Item_Draw"), "(F5) Draw Items"));
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Draw Distance({HackSettings.GetFloat("ESP_Item_Distance")}):");
            HackSettings.SetFloat("ESP_Item_Distance", Mathf.Floor(GUILayout.HorizontalSlider(HackSettings.GetFloat("ESP_Item_Distance"), 100, 2000)));
            GUILayout.EndHorizontal();
            HackSettings.SetState("ESP_Item_Draw_Name", GUILayout.Toggle(HackSettings.GetState("ESP_Item_Draw_Name"), "Draw Name"));
            HackSettings.SetState("ESP_Item_Draw_2DBox", GUILayout.Toggle(HackSettings.GetState("ESP_Item_Draw_2DBox"), "Draw 2DBox"));
            HackSettings.SetState("ESP_Item_Draw_Glow", GUILayout.Toggle(HackSettings.GetState("ESP_Item_Draw_Glow"), "Draw Glow"));

            GUILayout.Label("Vehicle Settings");
            HackSettings.SetState("ESP_Vehicle_Draw", GUILayout.Toggle(HackSettings.GetState("ESP_Vehicle_Draw"), "Draw Vehicles"));
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Draw Distance({HackSettings.GetFloat("ESP_Vehicle_Distance")}):");
            HackSettings.SetFloat("ESP_Vehicle_Distance", Mathf.Floor(GUILayout.HorizontalSlider(HackSettings.GetFloat("ESP_Vehicle_Distance"), 100, 2000)));
            GUILayout.EndHorizontal();
            HackSettings.SetState("ESP_Vehicle_Draw_Name", GUILayout.Toggle(HackSettings.GetState("ESP_Vehicle_Draw_Name"), "Draw Name"));
            HackSettings.SetState("ESP_Vehicle_Draw_Distance", GUILayout.Toggle(HackSettings.GetState("ESP_Vehicle_Draw_Distance"), "Draw Distance"));
            HackSettings.SetState("ESP_Vehicle_Draw_2DBox", GUILayout.Toggle(HackSettings.GetState("ESP_Vehicle_Draw_2DBox"), "Draw 2DBox"));
            HackSettings.SetState("ESP_Vehicle_Draw_Glow", GUILayout.Toggle(HackSettings.GetState("ESP_Vehicle_Draw_Glow"), "Draw Glow"));

            GUILayout.EndVertical();
        }
        public static void MainMenu_Index2()
        {
            
            GUILayout.BeginVertical();
            foreach (Pickup.JGHOAEDPDBB type in Enum.GetValues(typeof(Pickup.JGHOAEDPDBB)))
            {
                GUILayout.Label(Enum.GetName(typeof(Pickup.JGHOAEDPDBB), type));
                List<ItemDataEntry> listOfItems = ItemManager.GetItemList(type);
                foreach (ItemDataEntry entry in listOfItems)
                {
                    if (GUILayout.Button(entry.pickup.itemName))
                    {
                        ItemManager.AddItem(entry.pickup.m_itemIndex);
                    }
                }
            }
            GUILayout.EndVertical();
           
        }
        public static void MainMenu_Index3()
        {

            GUILayout.BeginVertical();
            HackSettings.SetString("GUI_Debug_Text", GUILayout.TextArea(HackSettings.GetString("GUI_Debug_Text")));
            GUILayout.EndVertical();

        }
        public static void MenuWindow(int id)
        {
            if (id == 0)
            {
                GUI.DragWindow(new Rect(0, 0, 300, 20));

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Main"))
                {
                    HackSettings.SetInt("GUI_Main_Index", 0);
                }
                if (GUILayout.Button("ESP"))
                {
                    HackSettings.SetInt("GUI_Main_Index", 1);
                }
                if (GUILayout.Button("Items"))
                {
                    HackSettings.SetInt("GUI_Main_Index", 2);
                }
                if (GUILayout.Button("Debug"))
                {
                    HackSettings.SetInt("GUI_Main_Index", 3);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginArea(new Rect(20, 70, 460, 460));
                HackSettings.SetVector2("GUI_SpawnItems_Scroll", GUILayout.BeginScrollView(HackSettings.GetVector2("GUI_SpawnItems_Scroll")));
                switch (HackSettings.GetInt("GUI_Main_Index"))
                {
                    case 0:
                        MainMenu_Index0();
                        break;
                    case 1:
                        MainMenu_Index1();
                        break;
                    case 2:
                        MainMenu_Index2();
                        break;
                    case 3:
                        MainMenu_Index3();
                        break;
                    default:
                        break;
                }
                GUILayout.EndScrollView();
                GUILayout.EndArea();
                /*GUILayout.BeginVertical();
                GUILayout.Label("F1: Hide/Show Menu");
                HackSettings.SetState("ESP_Player_Draw",GUILayout.Toggle(HackSettings.GetState("ESP_Player_Draw"), "(F2)Draw Players"));
                HackSettings.SetState("Aimbot_Silent", GUILayout.Toggle(HackSettings.GetState("Aimbot_Silent"), "Silent Aimbot"));

                //_F4 = GUILayout.Toggle(_F4, "(F4)Gunhack");
                HackSettings.SetState("ESP_Item_Draw",GUILayout.Toggle(HackSettings.GetState("ESP_Item_Draw"), "(F5)Draw Items"));
                //_F6 = GUILayout.Toggle(_F6, "(F6)Spam Heal");
                if (GUILayout.Button("(K)Kill nearest Player"))
                    PlayerManager.KillPlayer(PlayerManager.GetNearestPlayer(true));
                HackSettings.SetState("HACK_SendFakePosition",GUILayout.Toggle(HackSettings.GetState("HACK_SendFakePosition"), "(F9)Spam fake location"));
                GUILayout.EndVertical();

                GUILayout.BeginHorizontal();
                HackSettings.SetString("GUI_Main_Textbox1", GUILayout.TextField(HackSettings.GetString("GUI_Main_Textbox1")));
                /*if (GUILayout.Button("Spawn Item"))
                    SpawnItemButton();
                if (GUILayout.Button("Pick up all Items"))
                {
                    foreach (var item in ItemManager.Pickups)
                    {
                        item.PickupFar();
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical();
                GUILayout.Label($"ESP Distance: {HackSettings.GetFloat("ESP_Player_Distance")}");
                HackSettings.SetFloat("ESP_Player_Distance",GUILayout.HorizontalSlider(HackSettings.GetFloat("ESP_Player_Distance"), 20, 1500));
                GUILayout.EndVertical();
                if (GUILayout.Button("Change Name"))
                {
                    NetworkManager.ChangeName(HackSettings.GetString("FakeName"));
                }
                if (GUILayout.Button("T: Taze nearest Player"))
                    GunManager.TazePlayer(PlayerManager.GetNearestPlayer(), true); */
                //HackSettings.SetState("GUI_SpawnItems_Show", GUILayout.Toggle(HackSettings.GetState("GUI_SpawnItems_Show"), "Spawn menu"));
            }
            else if (id == 1)
            {
                GUI.DragWindow(new Rect(0, 0, 500, 20));
                HackSettings.SetVector2("GUI_SpawnItems_Scroll", GUILayout.BeginScrollView(HackSettings.GetVector2("GUI_SpawnItems_Scroll")));
                GUILayout.BeginVertical();
                foreach (Pickup.JGHOAEDPDBB type in Enum.GetValues(typeof(Pickup.JGHOAEDPDBB)))
                {
                    GUILayout.Label(Enum.GetName(typeof(Pickup.JGHOAEDPDBB), type));
                    List<ItemDataEntry> listOfItems = ItemManager.GetItemList(type);
                    foreach (ItemDataEntry entry in listOfItems)
                    {
                        if (GUILayout.Button(entry.pickup.itemName))
                        {
                            ItemManager.AddItem(entry.pickup.m_itemIndex);
                        }
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
            }

        }
    }
}
