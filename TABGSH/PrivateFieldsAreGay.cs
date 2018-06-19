using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace TABGSH
{
    public static class  PrivateFieldsAreGay
    {
        public static T GetGayField<T>(this object obj, string fieldName)
        {
            return (T)obj.GetType().GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(obj);
        }

        public static void SetGayField<T>(this object obj, string fieldName,object value)
        {
            if (obj == null)
                return;
           obj.GetType().GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(obj,value);
        }
        public static void SetGayFieldNULL(this object obj, string fieldName)
        {
            if (obj == null)
                return;
            obj.GetType().GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, null);
        }
        public static T GetGayProperty<T>(this object obj, string fieldName)
        {
            return (T)obj.GetType().GetProperty(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(obj);
        }

        public static void SetGayProperty<T>(this object obj, string fieldName, object value)
        {
            if (obj == null)
                return;
            obj.GetType().GetProperty(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(null, value);
        }


        public static T StaticGetGayField<T>(this object obj, string fieldName)
        {
            return (T)obj.GetType().GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(null);
        }
        public static void StaticSetGayField<T>(this object obj, string fieldName, object value)
        {
            if (obj == null)
                return;
            obj.GetType().GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(null, value);
        }
        public static T StaticGetGayProperty<T>(this object obj, string fieldName)
        {
            return (T)obj.GetType().GetProperty(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(null);
        }
        public static void StaticSetGayProperty<T>(this object type, string fieldName, object value)
        {
            if (type == null)
                return;
            type.GetType().GetProperty(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(null, value);
        }
    }
}
