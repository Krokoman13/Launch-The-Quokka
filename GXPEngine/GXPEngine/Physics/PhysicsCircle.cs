using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine.Physics
{
    public class PhysicsCircle : PhysicsObject
    {
        public int radius
        {
            get{ return _radius;}
        }
        private int _radius;

        public PhysicsCircle(int pRadius, Vector2 pPosition) : base(pRadius * 2 + 1, pRadius * 2 + 1, pPosition)
        {
            _radius = pRadius;
        }

        public override bool Colliding(PhysicsLine line)
        {
            // get length of the line
            Vector2 distanceVec = line.lineVector;
            float len = distanceVec.length;

            float dot = (((position.x - line.start.x) * (line.end.x - line.start.x)) + ((position.y - line.start.y) * (line.end.y - line.start.y))) / (len*len);

            Vector2 closest = line.start + dot * (line.end - line.start);

            float d1 = Vector2.Distance(closest, line.start);
            float d2 = Vector2.Distance(closest, line.end);

            // get the length of the line
            float lineLen = line.lineVector.length;
            float buffer = 0.1f;    // higher # = less accurate

            if (!(d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer))
            {
                return false;
            }

            // get distance to closest point
            distanceVec = closest - position;

            if (distanceVec.length <= radius)
            {
                return true;
            }
            return false;
        }

        bool linePoint(PhysicsLine line, Vector2 point)
        {
            float d1 = Vector2.Distance(point, line.start);
            float d2 = Vector2.Distance(point, line.end);

            // get the length of the line
            float lineLen = line.lineVector.length;
            float buffer = 0.3f;    // higher # = less accurate

            if (d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer)
            {
                return true;
            }
            return false;
        }

        public override bool Colliding(PhysicsCircle other)
        {
            Vector2 Difference = position - other.position;
            float distance = Difference.length;
            float minDistance = other.radius + radius;

            if (minDistance > distance)
            {
                return true;
            }

            return false;
        }

        public override void Collide(PhysicsObject other)
        {
            if (other is PhysicsLine)
            {
                Vector2 _lineVector = ((PhysicsLine)other).lineVector;
                Vector2 _lineToBall = position - ((PhysicsLine)other).start;
                float ballDistance = _lineToBall.Dot(_lineVector.Normal());
                position = position + (-ballDistance + radius) * _lineVector.Normal();
                return;
            }
            if (other is PhysicsCircle)
            {
                Vector2 Difference = position - other.position;
                float distance = Difference.length;

                Vector2 normal = Difference.Normalized();
                float overlap = ((PhysicsCircle)other).radius + radius - distance;

                position = position + normal * overlap;
                return;
            }
        }

        protected override void Draw()
        {
            draw.Ellipse(radius, radius, radius * 2, radius * 2);
        }
    }
}
