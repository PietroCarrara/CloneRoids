using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneRoids
{
    public class InvisibleCollider : CircleCollider
    {
        public InvisibleCollider(float radius) : base(radius)
        { }

        public override void debugRender(Graphics graphics)
        {
        }
    }
}
