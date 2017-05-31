using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CloneRoids.Scenes;

namespace CloneRoids.Components
{
    public class Sensor : Component, IUpdatable
    {
        private static Keys[] keys = new Keys[] { Keys.Z, Keys.Up, Keys.Down, Keys.Left, Keys.Right };

        bool shouldPress;

        private Keys key;

        MainScene cena;
        Vector2 offset;

        public Sensor(MainScene cena)
        {
            this.cena = cena;
            this.offset = new Vector2(Nez.Random.nextInt(Constants.ScreenWidth) - Constants.ScreenWidth / 2,
                                      Nez.Random.nextInt(Constants.ScreenHeight) - Constants.ScreenHeight / 2);

            key = keys.randomItem();
            shouldPress = Nez.Random.chance(.5f);
        }

        public Vector2 Position
        {
            get
            {
                var pos = entity.transform.position + offset;


                if (pos.X > Constants.ScreenWidth)
                {
                    pos.X -= Constants.ScreenWidth;
                }
                else if (pos.X < 0)
                {
                    pos.X += Constants.ScreenWidth;
                }

                if (pos.Y > Constants.ScreenHeight)
                {
                    pos.Y -= Constants.ScreenHeight;
                }
                else if (pos.Y < 0)
                {
                    pos.Y += Constants.ScreenHeight;
                }

                return pos;
            }
        }

        public void update()
        {
            foreach (var asteroid in cena.Asteroides)
            {
                int i = 0;
                Flags.setFlag(ref i, Constants.AsteroidLayer);

                var sla = Physics.linecast(entity.transform.position, Position, i);

                // Se a linha bateu em alguém
                if(sla.collider != null)
                {
                    if (shouldPress)
                        VirtualInput.PressKey(key);
                    else
                        VirtualInput.ReleaseKey(key);

                    return;
                }
            }
        }

        public override void debugRender(Graphics graphics)
        {
            base.debugRender(graphics);

            graphics.batcher.drawLine(entity.transform.position, Position, (shouldPress) ? Color.Green : Color.Red);
        }
    }
}
