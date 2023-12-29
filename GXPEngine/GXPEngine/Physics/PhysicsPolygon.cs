using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine.Physics
{
    public class PhysicsPolygon : PhysicsObject
    {
        public List<PhysicsCircle> points
        {
            get { return createPoints(_positions); }
        }

        public List<PhysicsLine> lines
        {
            get { return createLines(points); }
        }

        protected List<Vector2> _positions;

        public PhysicsPolygon(List<Vector2> pPositions, Vector2 pPosition) : base(calculateWidth(pPositions), calculateHeight(pPositions), pPosition)
        {
            _positions = pPositions;
        }

        public PhysicsPolygon(List<Vector2> pPositions, int pWidth, int pHeight, Vector2 pPosition) : base(pWidth, pHeight, pPosition)
        {
            _positions = pPositions;
        }

        private static int calculateWidth(List<Vector2> pPoints)
        {
            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (Vector2 point in pPoints)
            {
                int pointX = (int)point.x;

                if (pointX < min) min = pointX;
                if (pointX > max) max = pointX;
            }

            return max - min + 4;
        }

        private static int calculateHeight(List<Vector2> pPoints)
        {
            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (Vector2 point in pPoints)
            {
                int pointY = (int)point.y;

                if (pointY < min) min = pointY;
                if (pointY > max) max = pointY;
            }

            return max - min + 4;
        }

        protected List<PhysicsCircle> createPoints(List<Vector2> positions)
        {
            List<PhysicsCircle> outP = new List<PhysicsCircle>();

            foreach (Vector2 pos in positions)
            {
                Vector2 newPos = pos;

                newPos.x += position.x;
                newPos.y += position.y;
                newPos.RotateAroundDegrees(vecRotation.angleDeg, position);

                outP.Add(new PhysicsCircle(1, newPos));
            }

            return outP;
        }

        protected List<PhysicsLine> createLines(List<PhysicsCircle> points)
        {
            List<PhysicsLine> outP = new List<PhysicsLine>();

            for (int i = 0; i < points.Count - 1; i++)
            {
                outP.Add(new PhysicsLine(points[i].position, points[i + 1].position, bouncyness, trigger));
            }

            outP.Add(new PhysicsLine(points[points.Count - 1].position, points[0].position, bouncyness, trigger));

            return outP;
        }

        public override bool Colliding(PhysicsLine lineSegment)
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

        protected override void Draw()
        {
            draw.StrokeWeight(3);

            for (int i = 0; i < _positions.Count - 1; i++)
            {
                draw.Line(_positions[i].x + width / 2, _positions[i].y + height / 2, _positions[i + 1].x + width / 2, _positions[i + 1].y + height / 2);
            }

            //draw.Line(_positions[points.Count - 1].x + width / 2, points[points.Count - 1].position.y + height / 2 - 440, _positions[0].x + width / 2, _positions[0].y + height / 2);
            draw.Line(_positions[points.Count - 1].x + width / 2, points[points.Count - 1].position.y + height / 2, _positions[0].x + width / 2, _positions[0].y + height / 2);
        }
    }
}