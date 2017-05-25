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
                new Vector2(0, 20),
                new Vector2(10, 0),
                new Vector2(20, 20)
            };

            // Criamos o jogador
            var player = createEntity("player");
            // Adicionamos o colisor com os
            // pontos de antes
            player.addCollider(new PolygonCollider(points));
            player.addComponent(new MovementComponent(300, 5));
            player.addComponent(new RotationFixer());
        }
    }
}
