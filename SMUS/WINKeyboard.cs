﻿using System.Linq;
﻿using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using System.Text;

#if !MONO 

namespace SMUS
{
    internal class WINKeyboard
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern short GetKeyState(int virtualKeyCode);

        [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void SendInput(byte vk, byte scan, int flags, int extrainfo);

        private static readonly List<int> Down = new List<int>();

        public static bool IsKeyDownOnce(int keycode)
        {
            switch (GetKeyState(keycode))
            {
                case 1:

                    if (Down.Contains(keycode))
                        return false;

                    Down.Add(keycode);
                    return true;
                case 0:
                    Down.Remove(keycode);
                    break;
                default:
                    return false;
            }

            return false;
        }

    }
}

#endif