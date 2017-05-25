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

        /// <summary>
        /// Responsável por realizar o movimento de uma entidade como um carro: 
        /// só se move para frente e para traz, mas pode girar em torno de si mesma
        /// </summary>
        /// <param name="speed">Velocidade em pixels por segundo</param>
        /// <param name="turnSpeed">Força do giro em radianos por segundo</param>
        public MovementComponent(int speed = 0, int turnSpeed = 0)
        {
            Speed = speed;
            TurnSpeed = turnSpeed;
        }

        public void update()
        {
            // Movimento total do jogador nos eixos X e Y;
            var movement = Vector2.Zero;
            // Rotação realizada pelo jogador
            float rotation = 0;
            // Velocidade desejada
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
