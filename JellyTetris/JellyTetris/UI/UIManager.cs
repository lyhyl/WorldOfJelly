using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace JellyTetris.UI
{
    public static class UIManager
    {
        private static List<Control> ctrlList = new List<Control>();

        public static void Add(Control ctrl)
        {
            ctrlList.Add(ctrl);
        }

        public static void Remove(Control ctrl)
        {
            ctrlList.Remove(ctrl);
        }

        public static void RemoveAll()
        {
            ctrlList.Clear();
        }

        public static void HandleMouse()
        {
            foreach (Control ctrl in ctrlList)
                ctrl.HandleMouse();
        }
    }
}
