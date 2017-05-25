using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace CloneRoids.Components
{
    class MovementComponent : Component, IUpdatable
    {
        public int Speed, TurnSpeed;

        public MovementComponent(int speed = 0, int turnSpeed = 0)
        {
            Speed = speed;
            TurnSpeed = turnSpeed;
        }

        public void update()
        {
            var movement = Vector2.Zero;
            float rotation = 0;
            int velocity = 0;

            // Giro
            if (Input.isKeyDown(Keys.Right))
                rotation = TurnSpeed;
            else if (Input.isKeyDown(Keys.Left))
                rotation = -TurnSpeed;

            // Aplica o giro (em radianos), e armazena
            // a rotação na variável rotation
            rotation = entity.transform.rotation += rotation * Time.deltaTime;

            // Pra frente, pra traz
            if (Input.isKeyDown(Keys.Down))
            {
                velocity = Speed;
            }
            else if (Input.isKeyDown(Keys.Up))
            {
                velocity = -Speed;
            }

            // Calcula o movimento para X e Y
            movement.X = - velocity * Mathf.sin(rotation);
            movement.Y = velocity * Mathf.cos(rotation);

            // Aplica o movimento
            entity.move(movement * Time.deltaTime);
        }
    }
}
