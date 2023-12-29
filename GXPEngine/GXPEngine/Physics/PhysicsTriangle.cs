using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine.Physics
{
    public class PhysicsTriangle : PhysicsPolygon
    {
        public PhysicsTriangle(int pWidth, int pHeight, Vector2 pPosition) :
            base(
            new List<Vector2>()
            {
               new Vector2(- pWidth / 3, - pHeight / 3),
               new Vector2(0, 0),
               new Vector2(pWidth / 3, pHeight / 3),
            }, pWidth, pHeight, pPosition)
        {
        }

        protected override void Draw()
        {
            base.Draw();
        }
    }
}
