using Nez;
using Microsoft.Xna.Framework;

using CloneRoids.Components;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CloneRoids.Scenes
{
    class MainScene : Scene
    {
        public List<Entity> Projeteis = new List<Entity>();
        public List<Entity> Asteroides = new List<Entity>();

        public override void initialize()
        {
            base.initialize();

            // Adiciona um renderizador normal
            addRenderer(new DefaultRenderer());
            clearColor = Color.Black;

            CreatePlayer();

            var texture = content.Load<Texture2D>("Sprites/Asteroid/main");

            int layer = 0;
            Flags.setFlag(ref layer, Constants.AsteroidLayer);

            var sla = CreateAsteroid("asteroid");
            sla.transform.rotation += 2;
            var coll = sla.addCollider(new CircleCollider(30));
            coll.physicsLayer = layer;
            sla.addComponent(new Sprite(texture));
            sla.addComponent(new Asteroider(3, 200, 30));
            sla.addComponent(new BorderTeleporter(30, 30, Constants.ScreenWidth, Constants.ScreenHeight));
        }

        public Entity CreateProjectile(string name)
        {
            var projetil = createEntity(name);

            Projeteis.Add(projetil);

            return projetil;
        }

        public void DestroyProjectile(Entity proj)
        {
            if(Projeteis.Remove(proj))
            {
                proj.destroy();
            }
        }

        public Entity CreateAsteroid(string name)
        {
            var projetil = createEntity(name);

            Asteroides.Add(projetil);

            return projetil;
        }

        public void DestroyAsteroid(Entity asteroid)
        {
            if (Asteroides.Remove(asteroid))
            {
                asteroid.destroy();
            }
        }

        private void CreatePlayer()
        {
            // Criamos o jogador
            var player = createEntity("player", new Vector2(Constants.ScreenWidth/2, Constants.ScreenHeight/2));

            // Definir a camada a qual o jogador pertence
            int playerLayer = 0, playerCollisionLayer = 0;
            Flags.setFlag(ref playerLayer, Constants.PlayerLayer);
            Flags.setFlag(ref playerCollisionLayer, Constants.PlayerLayer);

            // Adiconar o colisor
            var coll = player.addCollider(
                new BoxCollider(
                    Constants.PlayerWidth / 2,
                    Constants.PlayerWidth / 2,
                    Constants.PlayerWidth,
                    Constants.PlayerHeight));

            // Configurar o colisor
            coll.physicsLayer = playerLayer;
            coll.collidesWithLayers = playerCollisionLayer;
            coll.setLocalOffset(new Vector2(0, 0));

            player.addComponent(new AsteroidCollider());

            // Adiciona o sistema de física
            player.addComponent(new ArcadeRigidbody())
                .shouldUseGravity = false;

            // Adiciona o nosso movimentador
            player.addComponent(new PlayerPhysicsMovementer(
                Constants.PlayerSpeed,
                Constants.PlayerTurnSpeed));

            // Adiciona o gerador de tiros
            player.addComponent(new Shooter());
            // Adiciona o "consertador" de rotação
            player.addComponent(new RotationFixer());
            // Trava ele na tela. Se ele sair de um lado, vem do outro
            player.addComponent(new BorderTeleporter(20, 30, 1280, 720));

            // Carrega e adiciona a foto
            var playerTex = content.Load<Texture2D>("Sprites/Player/main");
            player.addComponent(new Sprite(playerTex));
        }
    }
}
