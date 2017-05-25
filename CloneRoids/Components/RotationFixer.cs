using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;

namespace CloneRoids.Components
{
    class RotationFixer : Component, IUpdatable
    {
        public void update()
        {
            while (entity.transform.rotationDegrees > 360)
                entity.transform.rotationDegrees -= 360;

            while (entity.transform.rotationDegrees < 0)
                entity.transform.rotationDegrees += 360;
        }
    }
}
