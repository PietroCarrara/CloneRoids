using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using Microsoft.Xna.Framework;

namespace CloneRoids.Components
{
    public class Shot : Component, IUpdatable
    {
        private float sin, cos;

        public int Speed;

        public Shot(int speed)
        {
            Speed = speed;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            sin = Mathf.sin(entity.transform.rotation);
            cos = Mathf.cos(entity.transform.rotation);

            Core.schedule(3, (timer) =>
            {
                entity.destroy();
            });
        }

        public void update()
        {
            // Movimento total do jogador nos eixos X e Y;
            var movement = Vector2.Zero;

            // Calcula o movimento para X e Y
            movement.X = Speed * sin;
            movement.Y = -Speed * cos;

            // Aplica o movimento
            entity.move(movement * Time.deltaTime);
        }
    }
}
