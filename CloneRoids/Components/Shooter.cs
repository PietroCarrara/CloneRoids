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
        public override void onAddedToEntity()
        {
            base.onAddedToEntity();


        }

        public void update()
        {
            if (Input.isKeyPressed(Constants.ShootKey))
                createShoot();
        }

        private void createShoot()
        { 
            var tiro = entity.scene.createEntity("tiro", entity.transform.position);
            tiro.transform.rotation = entity.transform.rotation;

            int i = 0;
            Flags.setFlag(ref i, 1);

            var collider = tiro.addCollider(new BoxCollider(20, 20));
            collider.physicsLayer = i;
            collider.collidesWithLayers = i;

            tiro.addComponent(new Shot(Constants.ShootSpeed));
        }
    }
}
