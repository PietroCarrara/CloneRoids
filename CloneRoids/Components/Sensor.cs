﻿using System;
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
            shouldPress = Nez.Random.chance(50);
        }

        public Vector2 Position
        {
            get
            {
                return entity.transform.position
                    + offset;
            }
        }

        public void update()
        {
            foreach (var asteroid in cena.Asteroides)
            {
                if(asteroid.getCollider<Collider>().bounds.contains(Position))
                {
                    if (shouldPress)
                        VirtualInput.PressKey(key);
                    else
                        VirtualInput.ReleaseKey(key);

                    return;
                }

                if (!shouldPress)
                    VirtualInput.PressKey(key);
                else
                    VirtualInput.ReleaseKey(key);

            }
        }

        public override void debugRender(Graphics graphics)
        {
            base.debugRender(graphics);

            graphics.batcher.drawLine(entity.transform.position, Position,(shouldPress) ? Color.Green :Color.Red);
        }
    }
}