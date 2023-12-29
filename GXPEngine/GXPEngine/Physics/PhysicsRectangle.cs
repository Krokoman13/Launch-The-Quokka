using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine.Physics
{
    public class PhysicsRectangle : PhysicsPolygon
    {
        public bool collided;

        public PhysicsRectangle(int pWidth, int pHeight, Vector2 pPosition) : 
            base(
            new List<Vector2>()
            {
                new Vector2(- pWidth / 2, - pHeight / 2),
                new Vector2(pWidth / 2, - pHeight / 2),
                new Vector2(pWidth / 2, pHeight / 2),
                new Vector2(- pWidth / 2,pHeight / 2)
            },
            pWidth, pHeight, pPosition)
        {
        }

        public PhysicsRectangle(int pWidth, int pHeight, int pX, int pY) : this(pWidth, pHeight, new Vector2 (pX, pY))
        {
        }

        public override void Update()
        {
            base.Update();
            collided = false;
        }

        public override void Collide(PhysicsObject other)
        {
            base.Collide(other);
            collided = true;
        }

        protected override void Draw()
        {
            base.Draw();
            //draw.Rect(0, 0, width * 2, height * 2);
        }
    }
}
