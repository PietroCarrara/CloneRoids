using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;

namespace CloneRoids
{
    public static class VirtualInput
    {
        // Bools para representar se as teclas estão sendo apertadas
        static bool keyZ, keyUp, keyDown, keyLeft, keyRight;

        public static float Tempo = 0;

        // Bools dizendo se frame passado estas teclas foram pressionadas
        static bool wasKeyZ, wasKeyUp, wasKeyDown, wasKeyLeft, wasKeyRight;

        public static void Reset()
        {
            keyZ = keyUp = keyDown = keyLeft = keyRight = wasKeyZ = wasKeyUp = wasKeyDown = wasKeyLeft = wasKeyRight = false;
        }

        public static bool IsKeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.Z:
                    return keyZ;
                case Keys.Up:
                    return keyUp;
                case Keys.Down:
                    return keyDown;
                case Keys.Left:
                    return keyLeft;
                case Keys.Right:
                    return keyRight;
                default:
                    return false;
            }
        }

        public static bool IsKeyPressed(Keys key)
        {
            switch (key)
            {
                case Keys.Z:
                    return wasKeyZ;
                case Keys.Up:
                    return keyUp;
                case Keys.Down:
                    return keyDown;
                case Keys.Left:
                    return keyLeft;
                case Keys.Right:
                    return keyRight;
                default:
                    return false;
            }
        }

        public static void PressKey(Keys key)
        {
            Tempo = 0;

            switch (key)
            {
                case Keys.Z:
                    wasKeyZ = keyZ = true;
                    break;
                case Keys.Up:
                    wasKeyUp = keyUp = true;
                    break;
                case Keys.Down:
                    wasKeyDown = keyDown = true;
                    break;
                case Keys.Left:
                    wasKeyLeft = keyLeft = true;
                    break;
                case Keys.Right:
                    wasKeyRight = keyRight = true;
                    break;
            }
        }

        public static void ReleaseKey(Keys key)
        {
            Tempo = 0;

            switch (key)
            {
                case Keys.Z:
                    keyZ = false;
                    break;
                case Keys.Up:
                    keyUp = false;
                    break;
                case Keys.Down:
                    keyDown = false;
                    break;
                case Keys.Left:
                    keyLeft = false;
                    break;
                case Keys.Right:
                    keyRight = false;
                    break;
            }
        }

        public static void Update()
        {
            wasKeyZ = wasKeyUp = wasKeyDown = wasKeyLeft = wasKeyRight = false;

            Tempo += Time.deltaTime;
        }
    }
}
