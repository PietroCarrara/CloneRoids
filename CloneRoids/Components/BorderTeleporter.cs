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

        /// <summary>
        /// Mantém uma entidade dentro da área informada
        /// </summary>
        /// <param name="width">Largura da entidade</param>
        /// <param name="height">Altura de entidade</param>
        /// <param name="screenWidth">Largura da área de contenção</param>
        /// <param name="screenHeight">Altura da área de contenção</param>
        public BorderTeleporter(float width, float height, float screenWidth, float screenHeight)
        {
            Height = height;
            Width = width;

            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public void update()
        {
            // Pega a posição da entidade
            var pos = entity.transform.position;

            // Se ela passou dos limites, corrija-a
            if (pos.X + Width < 0)
                pos.X = ScreenWidth;
            else if (pos.X > ScreenWidth)
                pos.X = 0 - Width;

            if (pos.Y + Height < 0)
                pos.Y = ScreenHeight;
            else if (pos.Y > ScreenHeight)
                pos.Y = 0 - Height;

            // Aplica a posição na entidade
            entity.transform.position = pos;
        }
    }
}
