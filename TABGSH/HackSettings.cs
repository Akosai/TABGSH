using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TABGSH
{
    public static class HackSettings
    {
        public static Dictionary<string, int> SettingsInt = new Dictionary<string, int>()
        {
            {"GUI_Main_Index",0}
        };
        public static Dictionary<string, float> SettingsFloats = new Dictionary<string, float>() {
            {"Collect_NextUpdate" ,0f},
            {"Collect_UpdateInterval",1/30f },
            {"ESP_NextUpdate",0f },
            {"ESP_LastUpdate",0f },
            {"ESP_UpdateInterval", 1f/30f },


            {"ESP_Player_Distance",500f },

            {"ESP_Item_Distance",500f },

            {"ESP_Vehicle_Distance", 500f },

            {"PM_NearestPlayer_Distance",9999f },
            {"HACK_NoGunArea_Distance", 20f }


        };
        public static Dictionary<string, bool> SettingsBoolean = new Dictionary<string, bool>() {
            {"GUI_Show",true },
            {"GUI_SpawnItems_Show",false },

            {"ESP_Draw",false },
            {"ESP_Player_Draw", false },
            {"ESP_Player_Draw_Distance",true },
            {"ESP_Player_Draw_Health",true },
            {"ESP_Player_Draw_Name",true },
            {"ESP_Player_Draw_2DBox",false },
            {"ESP_Player_Draw_HeadX",false },
            {"ESP_Player_Draw_Nearest",false },
            {"ESP_Player_Draw_Skeleton",false },

            {"ESP_Item_Draw", false },
            {"ESP_Item_Draw_Name",true },
            {"ESP_Item_Draw_2DBox",false },

            {"ESP_Vehicle_Draw", false },
            {"ESP_Vehicle_Draw_Name", false },
            {"ESP_Vehicle_Draw_Distance", false },
            {"ESP_Vehicle_Draw_2DBox", false },

            {"PM_IgnoreTeam",true },
            {"PM_IgnoreMe",true },
            {"PM_IgnoreDummies",false },
            {"PM_IgnoreDowned",true },

            {"Aimbot_Silent", false },

            {"HACK_Gunhack",false },
            {"HACK_SpamHeal",false },
            {"HACK_SendFakePosition", false },
            {"HACK_NoGunArea", false }

        };
        public static Dictionary<string, Rect> SettingsRects = new Dictionary<string, Rect>()
        {
            {"GUI_MainWindow", new Rect(0, 0, 500, 550) }
        };
        public static Dictionary<string, string> SettingsStrings = new Dictionary<string, string>()
        {
            {"FakeName","Keklord" },
            {"GUI_Main_Textbox1","" },
            {"GUI_Debug_Text", "Debug Log" }
        };
        public static Dictionary<string, Vector2> SettingsVector3 = new Dictionary<string, Vector2>()
        {
            {"MAP_Industry",Vector3.zero}
        };
        public static Dictionary<string, Vector2> SettingsVector2 = new Dictionary<string, Vector2>()
        {
            {"GUI_SpawnItems_Scroll",Vector2.zero}
        };
        public static Dictionary<string, Color> SettingsColors = new Dictionary<string, Color>()
        {
            {"ESP_Player_Color_General", Color.cyan},
            {"ESP_Player_Color_Nearest", Color.green },

            {"ESP_Item_Color_Health", new Color(0f,1f,0f,1f) },
            {"ESP_Item_Color_Grenade", new Color(0.698f, 0.133f, 0.133f, 1f) },
            {"ESP_Item_Color_WeaponAttatchment", new Color(1f,0f,1f,1f) },
            {"ESP_Item_Color_Weapon", new Color(1.000f, 0.647f, 0f, 1f)},
            {"ESP_Item_Color_OtherConsumable", new Color(1f,0f,1f,1f) },
            {"ESP_Item_Color_Armor", new Color(0f,0f,1f,1f) },
            {"ESP_Item_Color_Ammo", new Color(1f,0.92f,0.016f,1f) },
            {"ESP_Item_Color_Unknown", new Color(0f,0f,0f,1f) }
        };
        public static Dictionary<string, KeyCode> SettingsKeyboard = new Dictionary<string, KeyCode>()
        {
            {"KEY_GUI_Menu_Toggle",KeyCode.F1}
        };
        public static int GetInt(string key)
        {
            return SettingsInt[key];
        }
        public static void SetInt(string key, int value)
        {
            SettingsInt[key] = value;
        }
        public static void Toggle(string key)
        {
            SettingsBoolean[key] = !SettingsBoolean[key];
        }
        public static void SetState(string key, bool state)
        {
            SettingsBoolean[key] = state;
        }
        public static bool GetState(string key)
        {
            return SettingsBoolean[key];
        }
        public static float GetFloat(string key)
        {
            return SettingsFloats[key];
        }
        public static void SetFloat(string key, float value)
        {
            SettingsFloats[key] = value;
        }
        public static Rect GetRect(string key)
        {
            return SettingsRects[key];
        }
        public static void SetRect(string key, Rect newRect)
        {
            SettingsRects[key] = newRect;
        }
        public static string GetString(string key)
        {
            return SettingsStrings[key];
        }
        public static void SetString(string key, string value)
        {
            SettingsStrings[key] = value;
        }
        public static Vector2 GetVector2(string key)
        {
            return SettingsVector2[key];
        }
        public static void SetVector2(string key, Vector2 vector)
        {
            SettingsVector2[key] = vector;
        }
        public static Color GetColor(string key)
        {
            return SettingsColors[key];
        }
        public static void SetColor(string key, Color vector)
        {
            SettingsColors[key] = vector;
        }
        public static KeyCode GetKey(string key)
        {
            return SettingsKeyboard[key];
        }
        public static void SetKey(string key, KeyCode code)
        {
            SettingsKeyboard[key] = code;
        }
        public static T GetValue<T>(string key)
        {
            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(SettingsStrings[key], typeof(T));
            if (typeof(T) == typeof(float))
                return (T)Convert.ChangeType(SettingsFloats[key], typeof(T));
            if (typeof(T) == typeof(int))
                return (T)Convert.ChangeType(SettingsInt[key], typeof(T));
            if (typeof(T) == typeof(bool))
                return (T)Convert.ChangeType(SettingsBoolean[key], typeof(T));
            if (typeof(T) == typeof(Color))
                return (T)Convert.ChangeType(SettingsColors[key], typeof(T));
            if (typeof(T) == typeof(Rect))
                return (T)Convert.ChangeType(SettingsRects[key], typeof(T));
            if (typeof(T) == typeof(Vector2))
                return (T)Convert.ChangeType(SettingsVector2[key], typeof(T));
            if (typeof(T) == typeof(Vector3))
                return (T)Convert.ChangeType(SettingsVector3[key], typeof(T));
            if (typeof(T) == typeof(KeyCode))
                return (T)Convert.ChangeType(SettingsKeyboard[key], typeof(T));
            return default(T);
        }
        public static void SetValue<T>(string key, T value)
        {
            if (typeof(T) == typeof(string))
                SettingsStrings[key] = (string)Convert.ChangeType(value, typeof(string));
            if (typeof(T) == typeof(float))
                SettingsFloats[key] = (float)Convert.ChangeType(value, typeof(float));
            if (typeof(T) == typeof(int))
                SettingsInt[key] = (int)Convert.ChangeType(value, typeof(int));
            if (typeof(T) == typeof(bool))
                SettingsBoolean[key] = (bool)Convert.ChangeType(value, typeof(bool));
            if (typeof(T) == typeof(Color))
                SettingsColors[key] = (Color)Convert.ChangeType(value, typeof(Color));
            if (typeof(T) == typeof(Rect))
                SettingsRects[key] = (Rect)Convert.ChangeType(value, typeof(Rect));
            if (typeof(T) == typeof(Vector2))
                SettingsVector2[key] = (Vector2)Convert.ChangeType(value, typeof(Vector2));
            if (typeof(T) == typeof(Vector3))
                SettingsVector3[key] = (Vector3)Convert.ChangeType(value, typeof(Vector3));
            if (typeof(T) == typeof(KeyCode))
                SettingsKeyboard[key] = (KeyCode)Convert.ChangeType(value, typeof(KeyCode));
        }
    }
}
