using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using Microsoft.Xna.Framework.Input;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using CloneRoids.Scenes;

namespace CloneRoids.Components
{
    public class Shooter : Component, IUpdatable
    {
        float tempo = 2;

        public void update()
        {
            tempo += Time.deltaTime;

            // Crie um tiro quando a tecla for prossionada
            if (Input.isKeyPressed(Constants.ShootKey) || VirtualInput.IsKeyPressed(Constants.ShootKey) && tempo >= 1)
                createShoot();
        }

        private void createShoot()
        {
            tempo = 0;

            // Pega a cena a qual esta entidade pertence,
            // e cria uma entidade nela
            var cena = entity.scene as MainScene;
            var tiro = cena.CreateProjectile("tiro");
            tiro.transform.position = entity.transform.position;
            tiro.transform.rotation = entity.transform.rotation;

            int i = 0;
            Flags.setFlag(ref i, Constants.ShootLayer);

            // Adiciona e configura  um colisor na entidade que criamos
            var collider = tiro.addCollider(new CircleCollider(Constants.ShootRadius));
            collider.physicsLayer = i;
            collider.collidesWithLayers = i;

            var texture = entity.scene.content.Load<Texture2D>("Sprites/Shot/main");
            tiro.addComponent(new Sprite(texture));
            // Não deixa a entidade sair do jogo
            tiro.addComponent(new BorderTeleporter(Constants.ShootRadius, Constants.ShootRadius, 1280, 720));
            // Esta entidade é um tiro
            tiro.addComponent(new Shot(Constants.ShootSpeed));
        }
    }
}
