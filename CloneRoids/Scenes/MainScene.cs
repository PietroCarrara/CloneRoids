using Nez;
using Microsoft.Xna.Framework;

using CloneRoids.Components;

namespace CloneRoids.Scenes
{
    class MainScene : Scene
    {
        public override void initialize()
        {
            base.initialize();

            addRenderer(new DefaultRenderer());

            // O triangulo tem 3 pontos,
            // assim como nosso player
            // definimos onde ficam esses
            // pontos aqui
            var points = new Vector2[]
            {
                new Vector2(0, 30),
                new Vector2(10, 0),
                new Vector2(20, 30)
            };

            // Criamos o jogador
            var player = createEntity("player");
            // Adicionamos o colisor com os
            // pontos de antes

            int i = 0;
            Flags.setFlag(ref i, 7);
            var coll = player.addCollider(new PolygonCollider(points));
            coll.physicsLayer = i;
            coll.collidesWithLayers = i;

            player.addComponent(new ArcadeRigidbody())
                .shouldUseGravity = false;

            player.addComponent(new PlayerPhysicsMovementer(
                Constants.PlayerSpeed,
                Constants.PlayerTurnSpeed));

            player.addComponent(new Shooter());
            player.addComponent(new RotationFixer());

            player.addComponent(new BorderTeleporter(20, 30, 1280, 720));
        }
    }
}
