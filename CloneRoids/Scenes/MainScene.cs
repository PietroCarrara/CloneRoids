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

        public override void initialize()
        {
            base.initialize();

            // Adiciona um renderizador normal
            addRenderer(new DefaultRenderer());
            clearColor = Color.Black;

            CreatePlayer();

            var sla = createEntity("asteroid");
            sla.transform.rotation += 2;
            sla.addCollider(new CircleCollider(30));
            sla.addComponent(new PrototypeSprite(20, 20).setColor(Color.Red));
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

        private void CreatePlayer()
        {
            // Criamos o jogador
            var player = createEntity("player");

            // Definir a camada a qual o jogador pertence
            int i = 0;
            Flags.setFlag(ref i, Constants.PlayerLayer);

            // Adiconar o colisor
            var coll = player.addCollider(
                new BoxCollider(
                    Constants.PlayerWidth / 2,
                    Constants.PlayerWidth / 2,
                    Constants.PlayerWidth,
                    Constants.PlayerHeight));

            // Configurar o colisor
            coll.physicsLayer = i;
            coll.collidesWithLayers = i;
            coll.setLocalOffset(new Vector2(0, 0));

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
