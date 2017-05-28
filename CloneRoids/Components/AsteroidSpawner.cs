using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using CloneRoids.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez.Sprites;

namespace CloneRoids.Components
{
    public class AsteroidSpawner : Component, IUpdatable
    {
        public static Texture2D Texture;

        private MainScene cena;

        public AsteroidSpawner(MainScene cena)
        {
            this.cena = cena;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            Texture = cena.content.Load<Texture2D>("Sprites/Asteroid/main");
        }

        public void update()
        {
            if (cena.Asteroides.Count > 4)
                return;

            var r = Nez.Random.nextInt(4);

            var asteroid = cena.CreateAsteroid("asteroid");

            switch (r)
            {
                case 0:
                    asteroid.transform.position = new Vector2(
                        Nez.Random.nextFloat(Constants.ScreenWidth),
                        Constants.ScreenHeight + Constants.AsteroidRadius);
                    break;
                case 1:
                    asteroid.transform.position = new Vector2(
                        Nez.Random.nextFloat(Constants.ScreenWidth),
                        -Constants.AsteroidRadius);
                    break;
                case 2:
                    asteroid.transform.position = new Vector2(
                        -Constants.AsteroidRadius,
                        Nez.Random.nextFloat(Constants.ScreenHeight));
                    break;
                case 3:
                    asteroid.transform.position = new Vector2(
                        Constants.ScreenWidth + Constants.AsteroidRadius,
                        Nez.Random.nextFloat(Constants.ScreenWidth));
                    break;
            }

            int layer = 0;
            Flags.setFlag(ref layer, Constants.AsteroidLayer);

            asteroid.addComponent(new Sprite(Texture));
            asteroid.addComponent(new Asteroider(3, Constants.AsteroidSpeed, Constants.AsteroidRadius));
            asteroid.transform.rotationDegrees = Nez.Random.nextInt(90) + 1;
            asteroid.addCollider(new CircleCollider(Constants.AsteroidRadius))
                .physicsLayer = layer;
            asteroid.addComponent(new BorderTeleporter(Constants.AsteroidRadius, Constants.AsteroidRadius,
                Constants.ScreenWidth, Constants.ScreenHeight));
        }
    }
}
