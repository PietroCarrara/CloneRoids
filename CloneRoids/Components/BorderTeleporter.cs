using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;

namespace CloneRoids.Components
{
    public class BorderTeleporter : Component, IUpdatable
    {
        public float Width, Height, ScreenWidth, ScreenHeight;

        public BorderTeleporter(float width, float height, float screenWidth, float screenHeight)
        {
            Height = height;
            Width = width;

            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public void update()
        {
            var pos = entity.transform.position;

            if (pos.X + Width < 0)
                pos.X = ScreenWidth;
            else if (pos.X > ScreenWidth)
                pos.X = 0 - Width;

            if (pos.Y + Height < 0)
                pos.Y = ScreenHeight;
            else if (pos.Y > ScreenHeight)
                pos.Y = 0 - Height;

            entity.transform.position = pos;
        }
    }
}
