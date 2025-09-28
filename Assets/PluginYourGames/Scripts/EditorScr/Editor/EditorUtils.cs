using UnityEditor;

namespace YG.EditorScr
{
    public static class EditorUtils
    {
        public static bool IsMouseOverWindow(EditorWindow window)
        {
            if (window == EditorWindow.mouseOverWindow)
                return true;
            return false;
        }

        public static bool IsMouseOverWindow(string nameWindow, bool includeInspector = true)
        {
            var windowUnderMouse = EditorWindow.mouseOverWindow;

            if (windowUnderMouse == null)
                return false;

            if (nameWindow == windowUnderMouse.titleContent.ToString()) return true;

            if (includeInspector && windowUnderMouse.titleContent.ToString() == "Inspector") return true;

            return false;
        }
    }
}