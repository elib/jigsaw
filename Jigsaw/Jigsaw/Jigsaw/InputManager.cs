using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Jigsaw
{
    class InputManager
    {

        public static List<Keys> justPressedKeys = new List<Keys>();
        private static Keys[] lastKeys ;

        public static void init()
        {
        }

        public static void update()
        {
            KeyboardState ks = Keyboard.GetState(0);
            Keys[] newKeys = ks.GetPressedKeys();

            justPressedKeys.Clear();

            if (lastKeys != null)
            {
                foreach (var key in newKeys)
                {
                    if (!lastKeys.Contains(key))
                    {
                        //newly-pressed!
                        justPressedKeys.Add(key);
                    }
                }
            }

            lastKeys = newKeys;
        }
    }
}