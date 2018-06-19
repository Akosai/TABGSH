/*
    Class used for debugging.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace TABGSH
{
    public static class Log
    {
        private static System.IO.StreamWriter GWriter = new System.IO.StreamWriter(Environment.CurrentDirectory + @"\log.log", true, Encoding.Unicode);

        public static void WriteLine(string text, bool newLine = false)
        {
            GWriter.WriteLine(text);
            if (newLine)
                GWriter.WriteLine("");
            GWriter.Flush();
        }


        public static void CollectList<T>(List<T> list)
        {
            int i = 0;
            foreach (T entry in list)
            {
                WriteLine($"{i}: {entry}");
               i++;
            }
            WriteLine("");
        }
        public static void AddLogText(string text)
        {
            string newtext = $"{HackSettings.GetString("GUI_Debug_Text")} {text} \n";
            HackSettings.SetString("GUI_Debug_Text", newtext);
        }
        public static void DumpObject(UnityEngine.GameObject player)
        {
            UnityEngine.Component[] comps = player.GetComponents<Component>();
            UnityEngine.Component[] Ccomps = player.GetComponentsInChildren<Component>();
            UnityEngine.Component[] Pcomps = player.GetComponentsInParent<Component>();
            foreach (var entry in comps)
            {
                WriteLine($"Name: {entry.name}");
                WriteLine($"\tType: {entry.GetType()}");
                WriteLine($"\tTag: {entry.tag}");
            }
            WriteLine($"--- {comps.Length} ---",true);

            foreach (var entry in Ccomps)
            {
                WriteLine($"Name: {entry.name}");
                WriteLine($"\tType: {entry.GetType()}");
                WriteLine($"\tTag: {entry.tag}");
            }
            WriteLine($"--- {Ccomps.Length} ---",true);

            foreach (var entry in Pcomps)
            {
                WriteLine($"Name: {entry.name}");
                WriteLine($"\tType: {entry.GetType()}");
                WriteLine($"\tTag: {entry.tag}");
            }
            WriteLine($"--- {Pcomps.Length} ---",true);
        }
    }
}
