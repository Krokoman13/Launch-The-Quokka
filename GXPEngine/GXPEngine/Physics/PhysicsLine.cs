using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine.Physics
{
    public class PhysicsLine : PhysicsObject
    {
        public Vector2 start;
        public Vector2 end;
        public Vector2 middle
        {
            get { return CalculateMiddle(start, end); }
        }
        public Vector2 lineVector
        {
            get { return start - end; }
        }

        public PhysicsLine(Vector2 pStart, Vector2 pEnd, float pBouncyness = 0f, bool pTrigger = false) : base((int)Vector2.Distance(pEnd, pStart), 1, CalculateMiddle(pStart, pEnd))
        {
            start = pStart;
            end = pEnd;

            bouncyness = pBouncyness;
            trigger = pTrigger;
            vecRotation.angleDeg = lineVector.angleDeg;

            UpdateScreenPosition();
        }

        public PhysicsLine(int startX, int startY, int endX, int endY ) : this(new Vector2(startX, startY), new Vector2(endX, endY))
        { 
        }

        private static Vector2 CalculateMiddle(Vector2 _start, Vector2 _end)
        {
            Vector2 middle = _end + _start;
            middle /= 2f;
            return middle;
        }

        protected override void Draw()
        {
            draw.Rect(0, 0, width*2, height*2);
            //draw.Clear(System.Drawing.Color.Green);
        }

        public override bool Colliding(PhysicsLine other)
        {
            throw new NotImplementedException();
        }

        public override bool Colliding(PhysicsCircle circle)
        {
            throw new NotImplementedException();
        }

        public override void Collide(PhysicsObject other)
        {
        }
    }
}
