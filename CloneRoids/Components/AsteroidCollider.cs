using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using CloneRoids.Scenes;

namespace CloneRoids.Components
{
    public class AsteroidCollider : Component, IUpdatable
    {
        Collider coll;

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            coll = entity.getCollider<Collider>();
        }

        public void update()
        {
            var cena = entity.scene as MainScene;

            foreach (var asteroid in cena.Asteroides)
            {
                var result = new CollisionResult();
                if (coll.collidesWith(asteroid.getCollider<Collider>(), out result))
                {
                    entity.destroy();
                    return;
                }
            }
        }
    }
}
