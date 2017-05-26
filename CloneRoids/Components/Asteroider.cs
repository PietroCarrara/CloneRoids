using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using Microsoft.Xna.Framework;
using CloneRoids.Scenes;

namespace CloneRoids.Components
{
    public class Asteroider : Component, IUpdatable
    {
        private int lives;
        private Collider collider;

        private int hits = 3;

        public float Velocity;

        private float sin, cos;

        private float radius;

        public Asteroider(int lives, float velocity, float radius)
        {
            this.lives = lives;

            Velocity = velocity;

            this.radius = radius;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            collider = entity.getCollider<Collider>();

            sin = Mathf.sin(entity.transform.rotation);
            cos = Mathf.cos(entity.transform.rotation);
        }

        public void update()
        {
            var cena = entity.scene as MainScene;

            var tiros = cena.Projeteis;

            foreach (var tiro in tiros)
            {
                CollisionResult result;
                if (collider.collidesWith(tiro.getCollider<Collider>(), out result))
                {
                    hits--;

                    if (hits <= 0)
                    {
                        if (lives > 0)
                            spawnChildren();

                        entity.destroy();
                    }

                    cena.DestroyProjectile(tiro);
                    
                    return;
                }
            }

            move();
        }

        private void move()
        {
            // Movimento total do jogador nos eixos X e Y;
            var movement = Vector2.Zero;

            // Calcula o movimento para X e Y
            movement.X = Velocity * sin;
            movement.Y = -Velocity * cos;

            // Aplica o movimento
            entity.move(movement * Time.deltaTime);
        }

        private void spawnChildren()
        {
            var radius = this.radius / 1.5f;

            var child1 = entity.scene.createEntity("asteroid");
            child1.addComponent(new PrototypeSprite(30, 30));
            child1.addComponent(new Asteroider(lives - 1, Velocity * 1.1f, radius));
            child1.transform.rotationDegrees = entity.transform.rotationDegrees + 90;
            child1.transform.position = entity.transform.position;
            child1.addCollider(new CircleCollider(radius));
            child1.addComponent(new BorderTeleporter(radius, radius,
                Constants.ScreenWidth, Constants.ScreenHeight));

            var child2 = entity.scene.createEntity("asteroid");
            child2.addComponent(new PrototypeSprite(radius / 1.5f, radius));
            child2.addComponent(new Asteroider(lives - 1, Velocity * 1.25f, radius));
            child2.transform.rotationDegrees = entity.transform.rotationDegrees - 90;
            child2.transform.position = entity.transform.position;
            child2.addCollider(new CircleCollider(radius));
            child2.addComponent(new BorderTeleporter(radius, radius,
                Constants.ScreenWidth, Constants.ScreenHeight));
        }
    }
}
