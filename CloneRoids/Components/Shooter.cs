using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using Microsoft.Xna.Framework.Input;

namespace CloneRoids.Components
{
    public class Shooter : Component, IUpdatable
    {
        public void update()
        {
            // Crie um tiro quando a tecla for prossionada
            if (Input.isKeyPressed(Constants.ShootKey))
                createShoot();
        }

        private void createShoot()
        { 
            // Pega a cena a qual esta entidade pertence,
            // e cria uma entidade nela
            var tiro = entity.scene.createEntity("tiro", entity.transform.position);
            tiro.transform.rotation = entity.transform.rotation;

            int i = 0;
            Flags.setFlag(ref i, Constants.ShootLayer);

            // Adiciona e configura  um colisor na entidade que criamos
            var collider = tiro.addCollider(new BoxCollider(20, 20));
            collider.physicsLayer = i;
            collider.collidesWithLayers = i;

            // Não deixa a entidade sair do jogo
            tiro.addComponent(new BorderTeleporter(Constants.ShootWidth, Constants.ShootHeight, 1280, 720));
            // Esta entidade é um tiro
            tiro.addComponent(new Shot(Constants.ShootSpeed));
        }
    }
}
