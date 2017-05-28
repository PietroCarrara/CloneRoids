using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using Microsoft.Xna.Framework;
using CloneRoids.Scenes;
using Microsoft.Xna.Framework.Graphics;
using Nez.Sprites;

namespace CloneRoids.Components
{
    public class Asteroider : Component, IUpdatable
    {
        private int lives;
        private Collider collider;

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
            CollisionResult result;
            var tiros = cena.Projeteis;

            if (collider.collidesWith(cena.Player.getCollider<Collider>(), out result))
            {
                cena.Lose();

                return;
            }

            foreach (var tiro in tiros)
            {
                if (collider.collidesWith(tiro.getCollider<Collider>(), out result))
                {
                    if (lives > 0)
                        spawnChildren();

                    entity.destroy();

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
            var cena = entity.scene as MainScene;

            cena.AsteroidsDestroyed++;

            var texture = entity.scene.content.Load<Texture2D>("Sprites/Asteroid/main");

            int layer = 0;
            Flags.setFlag(ref layer, Constants.AsteroidLayer);

            var child1 = cena.CreateAsteroid("asteroid");
            child1.addComponent(new Sprite(texture));
            child1.addComponent(new Asteroider(lives - 1, Velocity * 1.1f, radius));
            child1.transform.rotationDegrees = entity.transform.rotationDegrees + Nez.Random.nextInt(90) + 1;
            child1.transform.position = entity.transform.position;
            child1.addCollider(new CircleCollider(radius))
                .physicsLayer = layer;
            child1.addComponent(new BorderTeleporter(radius, radius,
                Constants.ScreenWidth, Constants.ScreenHeight));

            var child2 = cena.CreateAsteroid("asteroid");
            child2.addComponent(new Sprite(texture));
            child2.addComponent(new Asteroider(lives - 1, Velocity * 1.25f, radius));
            child2.transform.rotationDegrees = entity.transform.rotationDegrees - Nez.Random.nextInt(90) + 1;
            child2.transform.position = entity.transform.position;
            child2.addCollider(new CircleCollider(radius))
                .physicsLayer = layer;
            child2.addComponent(new BorderTeleporter(radius, radius,
                Constants.ScreenWidth, Constants.ScreenHeight));
        }
    }
}
