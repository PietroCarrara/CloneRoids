using Nez;
using Microsoft.Xna.Framework;

using CloneRoids.Components;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace CloneRoids.Scenes
{
    class MainScene : Scene
    {
        public override void initialize()
        {
            base.initialize();

            addRenderer(new DefaultRenderer());

            CreatePlayer();
        }

        private void CreatePlayer()
        {
            // Criamos o jogador
            var player = createEntity("player");
            // Adicionamos o colisor com os
            // pontos de antes

            int i = 0;
            Flags.setFlag(ref i, 7);

            var coll = player.addCollider(new BoxCollider(10, 15, 20, 30));
            coll.physicsLayer = i;
            coll.collidesWithLayers = i;
            coll.setLocalOffset(new Vector2(0, 0));

            player.addComponent(new ArcadeRigidbody())
                .shouldUseGravity = false;

            player.addComponent(new PlayerPhysicsMovementer(
                Constants.PlayerSpeed,
                Constants.PlayerTurnSpeed));

            player.addComponent(new Shooter());
            player.addComponent(new RotationFixer());

            player.addComponent(new BorderTeleporter(20, 30, 1280, 720));

            var playerTex = content.Load<Texture2D>("player");
            player.addComponent(new Sprite(playerTex));
        }
    }
}
