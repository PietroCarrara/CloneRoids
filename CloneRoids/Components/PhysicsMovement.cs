using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CloneRoids.Components
{
    public class PhysicsMovementer : Component, IUpdatable
    {
        public ArcadeRigidbody rb;

        public int Speed, TurnSpeed;

        public PhysicsMovementer(int speed, int turnSpeed)
        {
            Speed = speed;
            TurnSpeed = turnSpeed;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            rb = entity.getComponent<ArcadeRigidbody>();
        }

        public void update()
        {
            if (rb == null)
                return;

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

            // Freie com o tempo
            rb.velocity += -rb.velocity * Time.deltaTime;

            // Pra frente, pra traz
            if (Input.isKeyDown(Keys.Down))
            {
                velocity = Speed;
            }
            else if (Input.isKeyDown(Keys.Up))
            {
                velocity = -Speed;
            }
            else
            {
                // Se não vamos nos mover, não perca tempo calculando
                return;
            }

            // Calcula o movimento para X e Y
            movement.X = -velocity * Mathf.sin(rotation);
            movement.Y = velocity * Mathf.cos(rotation);

            // Aplica o movimento
            rb.addImpulse(movement * Time.deltaTime);
        }
    }
}
