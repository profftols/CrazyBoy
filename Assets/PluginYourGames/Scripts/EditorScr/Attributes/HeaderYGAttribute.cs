using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using YG.EditorScr;
#endif

namespace YG
{
    public class HeaderYGAttribute : PropertyAttribute
    {
        public string header { get; }
        public Color color { get; }
        public int indent { get; }

        public HeaderYGAttribute(string header)
        {
            this.header = header;
#if UNITY_EDITOR
            color = TextStyles.colorHeader;
#endif
            indent = 20;
        }

        public HeaderYGAttribute(string header, float r, float g, float b)
        {
            this.header = header;
            color = new Color(r, g, b);
            indent = 20;
        }

        public HeaderYGAttribute(string header, float r, float g, float b, float a)
        {
            this.header = header;
            color = new Color(r, g, b, a);
            indent = 20;
        }

        public HeaderYGAttribute(string header, string color)
        {
            this.header = header;
            this.color = ConvertStrinColor(color);
            indent = 20;
        }

        public HeaderYGAttribute(string header, int indent)
        {
            this.header = header;
            color = new Color(1.0f, 0.5f, 0.0f);
            this.indent = indent;
        }

        public HeaderYGAttribute(string header, string color, int indent)
        {
            this.header = header;
            this.color = ConvertStrinColor(color);
            this.indent = indent;
        }

        private Color ConvertStrinColor(string str)
        {
            if (str == "white")
                return Color.white;
            if (str == "black")
                return Color.black;
            if (str == "gray")
                return Color.gray;
            if (str == "red")
                return Color.red;
            if (str == "blue")
                return Color.blue;
            if (str == "yellow")
                return Color.yellow;
            if (str == "green")
                return Color.green;
            if (str == "cyan")
                return Color.cyan;
            return color;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(HeaderYGAttribute))]
    public class YGHeaderDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            var attr = (HeaderYGAttribute)attribute;

            var headerStyle = TextStyles.Header();
            headerStyle.normal.textColor = attr.color;

            position.y += attr.indent / 2;
            EditorGUI.LabelField(position, attr.header, headerStyle);
        }

        public override float GetHeight()
        {
            var attr = (HeaderYGAttribute)attribute;
            return base.GetHeight() + attr.indent;
        }
    }
#endif
}