using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using System.Collections;

namespace TABGSH
{
    public static class GuIDraw
    {
        private static Texture2D _coloredLineTexture = new Texture2D(1, 1);
        private static Color _coloredLineColor;

        public static void DrawOutline(Rect rect, string text, GUIStyle style, Color outColor, Color inColor, float size)
        {
            float halfSize = size * 0.5F;
            GUIStyle backupStyle = new GUIStyle(style);
            Color backupColor = GUI.color;

            style.normal.textColor = outColor;
            GUI.color = outColor;

            rect.x -= halfSize;
            GUI.Label(rect, text, style);

            rect.x += size;
            GUI.Label(rect, text, style);

            rect.x -= halfSize;
            rect.y -= halfSize;
            GUI.Label(rect, text, style);

            rect.y += size;
            GUI.Label(rect, text, style);

            rect.y -= halfSize;
            style.normal.textColor = inColor;
            GUI.color = backupColor;
            GUI.Label(rect, text, style);

            style = backupStyle;
        }

        public static void Draw3DBox(GameObject target, Color color) //https://www.unknowncheats.me/forum/counterstrike-global-offensive/175021-3d-box-esp.html
        {
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
            MeshRenderer meshrnd = target.GetComponentInChildren<MeshRenderer>();
            if (meshrnd)
                bounds = meshrnd.bounds;
            else
                return;
            Vector3[] corners = GetBoundPosition(bounds);
            Vector3[] ScreenW2S = new Vector3[8];
            for (int i = 0; i < 8; i++)
            {
                corners[i] = Quaternion.Euler(target.transform.rotation.eulerAngles) * corners[i];
                ScreenW2S[i] = Camera.main.WorldToScreenPoint(corners[i]);
                ScreenW2S[i].y = Screen.height - ScreenW2S[i].y;
                //DrawShadow(new Rect(ScreenW2S[i].x, ScreenW2S[i].y, 200, 200), new GUIContent($"{i}"), GUI.skin.GetStyle(""), Color.red, Color.black, Vector2.zero);
            }
            if (ScreenW2S[4].z > 0.01f && ScreenW2S[0].z > 0.01f)
                DrawLine(ScreenW2S[4], ScreenW2S[0], color); //FTL - FTR
            if (ScreenW2S[6].z > 0.01f && ScreenW2S[2].z > 0.01f)
                DrawLine(ScreenW2S[6], ScreenW2S[2], color); //FTR - FBR
            if (ScreenW2S[7].z > 0.01f && ScreenW2S[3].z > 0.01f)
                DrawLine(ScreenW2S[7], ScreenW2S[3], color); //FBR - FBL
            if (ScreenW2S[1].z > 0.01f && ScreenW2S[5].z > 0.01f)
                DrawLine(ScreenW2S[1], ScreenW2S[5], color); //FBL - FTL

            if (ScreenW2S[4].z > 0.01f && ScreenW2S[6].z > 0.01f)
                DrawLine(ScreenW2S[4], ScreenW2S[6], color); //BTL - BTR
            if (ScreenW2S[0].z > 0.01f && ScreenW2S[2].z > 0.01f)
                DrawLine(ScreenW2S[0], ScreenW2S[2], color); //BTR - BBR
            if (ScreenW2S[7].z > 0.01f && ScreenW2S[1].z > 0.01f)
                DrawLine(ScreenW2S[7], ScreenW2S[1], color); //BBR - BBL
            if (ScreenW2S[3].z > 0.01f && ScreenW2S[5].z > 0.01f)
                DrawLine(ScreenW2S[3], ScreenW2S[5], color); //BBL - BTL

            if (ScreenW2S[4].z > 0.01f && ScreenW2S[7].z > 0.01f)
                DrawLine(ScreenW2S[4], ScreenW2S[7], color); //FTL - BTL
            if (ScreenW2S[6].z > 0.01f && ScreenW2S[1].z > 0.01f)
                DrawLine(ScreenW2S[6], ScreenW2S[1], color); //FTR - BTR
            if (ScreenW2S[0].z > 0.01f && ScreenW2S[3].z > 0.01f)
                DrawLine(ScreenW2S[0], ScreenW2S[3], color); //FBR - BBR
            if (ScreenW2S[2].z > 0.01f && ScreenW2S[5].z > 0.01f)
                DrawLine(ScreenW2S[2], ScreenW2S[5], color); //FBL - BBL
        }


        public static Vector3[] GetBoundPosition(Bounds bounds)//Vector3 mins, Vector3 maxs)
        {
            /*
            Vector3[] corners = {
                mins, maxs,
                new Vector3(mins.x,mins.y,maxs.z),
                new Vector3(mins.x,maxs.y,mins.z),
                new Vector3(maxs.x,mins.y,mins.z),
                new Vector3(mins.x,maxs.y,maxs.z),
                new Vector3(maxs.x,mins.y,maxs.z),
                new Vector3(maxs.x,maxs.y,mins.z),
            };*/
            Vector3 center = bounds.center;
            Vector3 extents = bounds.size / 2f;
            Vector3[] corners = {
                center + new Vector3(- extents.x,  extents.y,  - extents.z), //FrontTopLeft 0
                center + new Vector3( extents.x,  extents.y,  - extents.z), //FrontTopRight 1
                center + new Vector3(- extents.x,  - extents.y,  - extents.z), //FrontBottomLeft 2
                center + new Vector3(extents.x,  - extents.y,  - extents.z), //FrontBottomRight 3

                center + new Vector3( - extents.x,  extents.y,  extents.z), //BackTopLeft 4
                center + new Vector3( extents.x, extents.y,  extents.z), //BackTopRight 5
                center + new Vector3( - extents.x,  - extents.y,  extents.z), //BackBottomLeft 6
                center + new Vector3( extents.x,  - extents.y, extents.z) //BackBottomRight 7
            };
            return corners;
        }
        public static void DrawShadow(Rect rect, GUIContent content, GUIStyle style, Color txtColor, Color shadowColor,
                                        Vector2 direction)
        {
            GUIStyle backupStyle = style;

            style.normal.textColor = shadowColor;
            rect.x += direction.x;
            rect.y += direction.y;
            GUI.Label(rect, content, style);

            style.normal.textColor = txtColor;
            rect.x -= direction.x;
            rect.y -= direction.y;
            GUI.Label(rect, content, style);

            style = backupStyle;
        }
        public static void DrawLayoutShadow(GUIContent content, GUIStyle style, Color txtColor, Color shadowColor,
                                        Vector2 direction, params GUILayoutOption[] options)
        {
            DrawShadow(GUILayoutUtility.GetRect(content, style, options), content, style, txtColor, shadowColor, direction);
        }

        public static bool DrawButtonWithShadow(Rect r, GUIContent content, GUIStyle style, float shadowAlpha, Vector2 direction)
        {
            GUIStyle letters = new GUIStyle(style);
            letters.normal.background = null;
            letters.hover.background = null;
            letters.active.background = null;

            bool result = GUI.Button(r, content, style);

            Color color = r.Contains(Event.current.mousePosition) ? letters.hover.textColor : letters.normal.textColor;

            DrawShadow(r, content, letters, color, new Color(0f, 0f, 0f, shadowAlpha), direction);

            return result;
        }

        public static bool DrawLayoutButtonWithShadow(GUIContent content, GUIStyle style, float shadowAlpha,
                                                       Vector2 direction, params GUILayoutOption[] options)
        {
            return DrawButtonWithShadow(GUILayoutUtility.GetRect(content, style, options), content, style, shadowAlpha, direction);
        }
        public static void Draw2DBox(float x, float y, float w, float h, Color color)
        {
            DrawLine(new Vector2(x, y), new Vector2(x + w, y), color);
            DrawLine(new Vector2(x, y), new Vector2(x, y + h), color);
            DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color);
            DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color);
        }

        public static void DrawLineStretched(Vector2 start, Vector2 end, Texture2D texture, int thick)
        {
            var vector = end - start;
            float pivot = 57.29578f * Mathf.Atan(vector.y / vector.x);
            if (vector.x < 0f)
                pivot += 180f;
            if (thick < 1)
                thick = 1;

            int yOffset = (int)Mathf.Ceil((float)(thick / 2));
            GUIUtility.RotateAroundPivot(pivot, start);
            GUI.DrawTexture(new Rect(start.x, start.y - yOffset, vector.magnitude, (float)thick), texture);
            GUIUtility.RotateAroundPivot(-pivot, start);
        }
        public static void DrawLine(Vector2 start, Vector2 end, Color color, int thick)
        {
            if (_coloredLineTexture == null || _coloredLineColor != color)
            {
                _coloredLineColor = color;

                _coloredLineTexture.SetPixel(0, 0, _coloredLineColor);
                _coloredLineTexture.wrapMode = 0;
                _coloredLineTexture.Apply();
            }
            DrawLineStretched(start, end, _coloredLineTexture, thick);
        }
        public static void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            DrawLine(start, end, color, 1);
        }
        public static void DrawLine(Vector2 start, Vector2 end, Texture2D texture, int thick)
        {
            var vector = end - start;
            float pivot = 57.29578f * Mathf.Atan(vector.y / vector.x);
            if (vector.x < 0f)
                pivot += 180f;
            if (thick < 1)
                thick = 1;

            int yOffset = (int)Mathf.Ceil((float)(thick / 2));
            var rect = new Rect(start.x, start.y - yOffset, Vector2.Distance(start, end), (float)thick);
            GUIUtility.RotateAroundPivot(pivot, start);
            GUI.BeginGroup(rect);
            int num3 = Mathf.RoundToInt(rect.width);
            int num4 = Mathf.RoundToInt(rect.height);

            for (int i = 0; i < num4; i += texture.height)
            {
                for (int j = 0; j < num3; j += texture.width)
                {
                    GUI.DrawTexture(new Rect((float)j, (float)i, (float)texture.width, (float)texture.height), texture);
                }
            }
        }
        public static void DrawLine(Vector2 start, Vector2 end, Texture2D texture)
        {
            DrawLine(start, end, texture, 1);
        }
    }
}
