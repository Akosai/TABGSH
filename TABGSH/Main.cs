using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace TABGSH
{
    public class Main : MonoBehaviour
    {
        private GameObject GameobjectHolder = null; //Gameobject holding our main class
        private bool _F4 = false; //Used for testing
        private bool _F6 = false; //Used for testing

        public void Load()
        {
            GameobjectHolder = new GameObject();
            GameobjectHolder.AddComponent<Main>();
            DontDestroyOnLoad(GameobjectHolder);
        }

        public void Unload()
        {
            Destroy(GameobjectHolder);
            Destroy(this);

        }
        private void Update()
        {
            
            if (Time.time >= HackSettings.GetFloat("Collect_NextUpdate"))
            {
                PlayerManager.UpdatePlayerList();
                ItemManager.UpdatePickupList();
                CarManager.UpdateCarList();
                HackSettings.SetFloat("Collect_NextUpdate", Time.time + HackSettings.GetFloat("Collect_UpdateInterval"));
            }
            
            if (Time.time >= HackSettings.GetFloat("ESP_NextUpdate"))
                HackSettings.SetFloat("ESP_NextUpdate", Time.time + HackSettings.GetFloat("ESP_UpdateInterval"));
                //Unload Method, ToDo: Remove Assembly when unloading
            if (Input.GetKeyDown(KeyCode.Delete))
                this.Unload();
                //Only allow input when we are not using the chat
            if (!TABGChat.inChat)
            {
                if (Input.GetKeyDown(HackSettings.GetKey("KEY_GUI_Menu_Toggle")))
                    HackSettings.Toggle("GUI_Show");
                if (Input.GetKeyDown(KeyCode.F2))
                    HackSettings.Toggle("ESP_Player_Draw");
                if (Input.GetKeyDown(KeyCode.F4))
                    HackSettings.Toggle("HACK_Gunhack");
                if (Input.GetKeyDown(KeyCode.F5))
                    HackSettings.Toggle("ESP_Item_Draw");
                if (Input.GetKeyDown(KeyCode.F6))
                    HackSettings.Toggle("HACK_SpamHeal");
                 //Teleport behind nearest Player
                if (Input.GetKey(KeyCode.Mouse4))
                {
                    var target = PlayerManager.NearestPlayer;

                    if (target)
                    {
                        Vector3 aimPos = Camera.main.transform.position;
                        aimPos = target.m_hip.transform.position - (target.m_hip.transform.forward * 1f);
                        //GetLocalPlayer().transform.position = aimPos;
                        PlayerManager.GetLocalPlayer().GetComponentInChildren<Torso>().transform.position = aimPos;
                    }
                }
                //Teleport to the newest marker
                if (Input.GetKey(KeyCode.Mouse3))
                {
                    var marker = FindObjectOfType<Landfall.Network.MarkerHandler>().markers;
                    float lastChange = 99999f;
                    Vector3 pos = Camera.main.transform.position;
                    foreach (var mark in marker)
                    {
                        if (mark == null || !mark.active)
                            continue;
                        if (mark.sinceChange < lastChange)
                        {
                            pos = mark.position;
                            lastChange = mark.sinceChange;
                        }
                    }
                    PlayerManager.GetLocalPlayer().GetComponentInChildren<Torso>().transform.position = pos;
                }
                //Kill nearest Player
                if (Input.GetKeyDown(KeyCode.K))
                {

                    KillPlayerButton();

                }
                //Spawn predefined set of items, some won't spawn
                if (Input.GetKey(KeyCode.B))
                {
                    int[] itemsToSpawn = { 50, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 60, 50, 130, 29, 17, 44, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 31, 31, 31 };
                    for (int i = 0; i < itemsToSpawn.Length; i++)
                    {
                        ItemManager.AddItem(i);
                    }
                }
                //Taze nearest Player for 9999f seconds
                if (Input.GetKey(KeyCode.T))
                {
                    GunManager.TazePlayer(PlayerManager.NearestPlayer, true, 9999f);
                }
                //Speedhack kinda, key-> Middlemousebutton
                if (Input.GetKey(KeyCode.Mouse2))
                {
                    PlayerManager.GetLocalPlayer().GetComponent<InputHandler>().inputMovementDirection += Camera.main.transform.forward * 9f;
                }
            }

            if (HackSettings.GetState("HACK_Gunhack"))
            {
                try
                {
                    Gun CurrentGun = GunManager.GetCurrentGun();
                    /*var CurrentStrength = PlayerManager.GetLocalPlayer().GetComponent<MeleeWeapon>();
                    if (CurrentStrength)
                    {
                        CurrentStrength.damageOnHit = 99999f;
                    }*/
                    if (CurrentGun)
                    {
                        List<Component> ListToDelete = new List<Component>{
                        CurrentGun.Component_Recoil(),
                        CurrentGun.GetComponent<AddScreenShake>(),
                        CurrentGun.GetComponentInChildren<ParticleSystem>(),
                        CurrentGun.GetComponent<GunSFX>(),
                        CurrentGun.projectile.GetComponentInChildren<ProjectileHit>().GetComponentInChildren<ParticleSystem>()};
                        foreach (var k in ListToDelete)
                        {
                            if (k)
                                DestroyObject(k);
                        }
                        CurrentGun.bullets = 99;
                        CurrentGun.bulletsInMag = 99;
                        CurrentGun.currentFireMode = 3;
                        var ProjectHit = CurrentGun.projectile.GetComponentInChildren<ProjectileHit>();
                        if (ProjectHit)
                        {
                            ProjectHit.damage = 9999f;
                            ProjectHit.effectMultiplier = 9999f;
                            ProjectHit.force = 9999f;
                        }
                    }
                }
                catch (Exception ex)
                {


                }
            }
            if (HackSettings.GetState("HACK_SpamHeal"))
            {
                NetworkSend.SendHealRequest();
            }
            
            if (Input.GetKeyDown(KeyCode.F8))
            {
                SendChat(PlayerManager.GetLocalPlayer().m_torso.transform, PlayerManager.GetName(PlayerManager.GetLocalPlayer()));

            }
            if (Input.GetKeyDown(KeyCode.F9))
                HackSettings.Toggle("HACK_SendFakePosition");

            if (HackSettings.GetState("Aimbot_Silent"))
            {
                var target = PlayerManager.NearestPlayer;
                if (target)
                    GunManager.DoAimbot(target);
            }
            if (HackSettings.GetState("HACK_SendFakePosition"))
                NetworkManager.GetServerHandler().SendPlayerUpdate(PlayerManager.GetLocalPlayer().GetPosition() - new Vector3(0, 120, 0), Vector2.zero, PlayerManager.GetLocalPlayer().GetPosition() + new Vector3(0, -120, 0), 5);
            if (HackSettings.GetState("HACK_NoGunArea"))
                NetworkSend.DropThatWeaponArea();
        }
        private Vector2 SpawnMenuScrollPosition = Vector2.zero;


        private void SendChat(Transform parent, string message)
        {
            NetworkManager.GetServerHandler().ClientRequestTalkingRockThrow(parent.position, parent.forward * 2f, message);
        }
        private void SpawnItemButton()
        {
            Log.DumpObject(PlayerManager.GetLocalPlayer().gameObject);
        }
        private void KillPlayerButton()
        {
            Player player = PlayerManager.NearestPlayer;
            if (PlayerManager.IsPlayerValid(player))
                PlayerManager.KillPlayer(player);
        }



        private void OnGUI()
        {
            /*if ((Event.current.type != EventType.Repaint) &&
                 (Event.current.type != EventType.Layout) && 
                 (Event.current.type != EventType.))
                return;*/
            GUI.color = Color.cyan;
            if (HackSettings.GetState("GUI_Show"))
                HackSettings.SetRect("GUI_MainWindow", GUI.Window(0, HackSettings.GetRect("GUI_MainWindow"), GuiMenu.MenuWindow, "~AA~"));

            if ((HackSettings.GetFloat("ESP_LastUpdate") <= HackSettings.GetFloat("ESP_NextUpdate")) && HackSettings.GetState("ESP_Draw"))
            {
                if (HackSettings.GetState("ESP_Player_Draw"))
                    DrawPlayers();
                if (HackSettings.GetState("ESP_Item_Draw"))
                    DrawPickups();
                if (HackSettings.GetState("ESP_Vehicle_Draw"))
                    DrawVehicles();
                HackSettings.SetFloat("ESP_LastUpdate", Time.time);
            }

        }


        private void DrawPlayers()
        {
            foreach (Player player in PlayerManager.Players)
            {
                if (!player.IsPlayerValid(HackSettings.GetState("PM_IgnoreTeam"), true))
                    continue;
                if (HackSettings.GetState("PM_IgnoreDummies") && player.IsDummy())
                    continue;
                Torso m_torso = player.m_torso;
                float distanceToObj = Mathf.Floor(PlayerManager.DistanceTo(m_torso.transform));
                if (distanceToObj > HackSettings.GetFloat("ESP_Player_Distance"))
                    continue;
                var W2S = Camera.main.WorldToScreenPoint(m_torso.transform.position);
                if (W2S.z < 0.01f)
                    continue;

                int healthRnd = (int)Math.Floor(player.m_playerDeath.health);
                float customESPHeight = 25 / distanceToObj * 34;
                W2S += new Vector3(customESPHeight - customESPHeight * 1.35f, customESPHeight, 0);
                Color drawColor = HackSettings.GetColor("ESP_Player_Color_General");
                if (HackSettings.GetState("ESP_Player_Draw_Nearest"))
                    if (PlayerManager.NearestPlayer == player)
                        drawColor = HackSettings.GetColor("ESP_Player_Color_Nearest");
                string drawText = (HackSettings.GetState("ESP_Player_Draw_Name") ? $"{PlayerManager.GetName(player)}" : "") +
                                (HackSettings.GetState("ESP_Player_Draw_Distance") ? $"({distanceToObj})" : "") +
                                (HackSettings.GetState("ESP_Player_Draw_Health") ? $"\n{healthRnd} HP" : "");
                Vector2 drawSize = GUI.skin.GetStyle(drawText).CalcSize(new GUIContent(drawText));
                /*
                    ToDo: Fix 2DBox size
                */
                if (HackSettings.GetState("ESP_Player_Draw_2DBox"))
                    GuIDraw.Draw2DBox(W2S.x, (float)Screen.height - W2S.y, 30f, 60f, Color.blue);
                    
                GuIDraw.DrawShadow(new Rect(W2S.x - drawSize.x / 2, (float)Screen.height - W2S.y, drawSize.x, drawSize.y), new GUIContent(drawText), GUI.skin.GetStyle(""), drawColor, Color.black, Vector2.zero);
                if (HackSettings.GetState("ESP_Player_Draw_HeadX"))
                {
                    Vector3 headW2S = Camera.main.WorldToScreenPoint(player.m_head.transform.position);
                    if (headW2S.z > 0.01f)
                    {
                        GuIDraw.DrawShadow(new Rect(headW2S.x, (float)Screen.height - headW2S.y, 40, 40), new GUIContent("X"), GUI.skin.GetStyle(""), drawColor, Color.black, Vector2.zero);
                    }
                }
                bool shouldIGlow = HackSettings.GetState("ESP_Player_Draw_Glow");
                player.gameObject.MakeMeGlow(drawColor,shouldIGlow);
            }
        }

        private void DrawPickups()
        {
            foreach (Pickup pickup in ItemManager.Pickups)
            {
                if (!ItemManager.IsValidPickup(pickup))
                    continue;

                float distance = Mathf.Floor(PlayerManager.DistanceTo(pickup.transform));
                if (distance > HackSettings.GetFloat("ESP_Item_Distance"))
                    continue;

                var W2S = Camera.main.WorldToScreenPoint(pickup.transform.position);
                if (W2S.z < 0.01f)
                    continue;

                Color drawColor = ItemManager.GetItemTypeColor(pickup.weaponType);
                var drawText = $"{pickup.itemName}";
                var drawSize = GUI.skin.GetStyle(drawText).CalcSize(new GUIContent(drawText));
                float customESPHeight = 5 / distance * 34;
                W2S += new Vector3(customESPHeight - customESPHeight * 1.35f, customESPHeight, 0);
                if (HackSettings.GetState("ESP_Item_Draw_Name"))
                {
                    GuIDraw.DrawShadow(new Rect(W2S.x - drawSize.x / 2, (float)Screen.height - W2S.y, drawSize.x, drawSize.y), new GUIContent(drawText), GUI.skin.GetStyle(""), drawColor, Color.black, Vector2.zero);
                }
                /*
                    ToDo: Fix 2DBox size
                */
                if (HackSettings.GetState("ESP_Item_Draw_2DBox"))
                {
                    GuIDraw.Draw2DBox(W2S.x, Screen.height - W2S.y, 20, 20, drawColor);
                }
                //GuIDraw.Draw3DBox(pickup.gameObject, drawColor); //Works and doesn't work at the same time
                
                bool shouldIGlow = HackSettings.GetState("ESP_Item_Draw_Glow");
                pickup.gameObject.MakeMeGlow(drawColor,shouldIGlow);
            }
        }
        private void DrawVehicles()
        {
            foreach (Car car in CarManager.Cars)
            {
                float distance = Mathf.Floor(PlayerManager.DistanceTo(car.transform));
                if (distance > HackSettings.GetFloat("ESP_Vehicle_Distance"))
                    continue;

                var W2S = Camera.main.WorldToScreenPoint(car.transform.position);
                if (W2S.z < 0.01f)
                    continue;
                float customESPHeight = 25 / distance * 34;
                W2S += new Vector3(customESPHeight - customESPHeight * 1.35f, customESPHeight, 0);
                Color drawColor = Color.blue;
                var drawText = (HackSettings.GetState("ESP_Vehicle_Draw_Distance") ? $"({distance})" : "") + (HackSettings.GetState("ESP_Vehicle_Draw_Name") ? $"{car.name}" : "");
                var drawSize = GUI.skin.GetStyle(drawText).CalcSize(new GUIContent(drawText));

                if (HackSettings.GetState("ESP_Vehicle_Draw_Name"))
                {
                    GuIDraw.DrawShadow(new Rect(W2S.x - drawSize.x / 2, (float)Screen.height - W2S.y, drawSize.x, drawSize.y), new GUIContent(drawText), GUI.skin.GetStyle(""), drawColor, Color.black, Vector2.zero);
                }
                /*
                    ToDo: Fix 2DBox size
                */
                if (HackSettings.GetState("ESP_Vehicle_Draw_2DBox"))
                {
                    GuIDraw.Draw2DBox(W2S.x - 70f, Screen.height - W2S.y, 50, 70, drawColor);
                }
                bool shouldIGlow = HackSettings.GetState("ESP_Vehicle_Draw_Glow");
                car.gameObject.MakeMeGlow(drawColor,shouldIGlow);
                //GuIDraw.Draw3DBox(car.gameObject, drawColor);
            }
        }
    }
}
