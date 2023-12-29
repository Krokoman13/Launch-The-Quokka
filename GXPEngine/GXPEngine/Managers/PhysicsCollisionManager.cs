using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Physics;
using GXPEngine.Core;

namespace GXPEngine.Managers
{
	public class PhysicsCollisionManager
	{
        List<PhysicsObject> _movingPhysicsObjects = new List<PhysicsObject>();
		List<PhysicsObject> _allPhysicsObjects = new List<PhysicsObject>();
		float collisionCounter;
		bool collision;

		private void checkCollisions(PhysicsObject physicsObject, List<PhysicsObject> others)
		{
			foreach (PhysicsObject other in others)
			{
				collisionCounter++;

				if (DetectCollision(physicsObject, other))
				{
					if (physicsObject.Colliding(other))
					{
						//break;
					}
				}
			}
		}

		public void Add(PhysicsObject physicsObject)
		{
			if (physicsObject.moving)
			{
				_movingPhysicsObjects.Add(physicsObject);
				return;
			}

			_allPhysicsObjects.Add(physicsObject);
		}

		public void Remove(PhysicsObject physicsObject)
		{
			if (physicsObject.moving)
			{
				_movingPhysicsObjects.Remove(physicsObject);
				return;
			}

			_allPhysicsObjects.Remove(physicsObject);
		}

		private void checkAllCollisions()
		{
			collision = false;

			foreach (PhysicsObject physicsObject in _movingPhysicsObjects)
			{
				checkCollisions(physicsObject, _allPhysicsObjects);
			}
		}

		public static bool DetectCollision(PhysicsObject a, PhysicsObject b)
		{
			float distance = Vector2.Distance(a.position, b.position);
			float radiusA = new Vector2(a.width, a.height).length/2;
			float radiusB = new Vector2(b.width, b.height).length/2;

			return distance < (radiusA + radiusB);
		}

		internal void Step()
        {
			int i = 1;
			collisionCounter = 0;
			checkAllCollisions();

			while (collision)
			{
				i++;
				checkAllCollisions();

				if (i > 50)
				{
					break;
				}
			}

			//Console.WriteLine("CheckedCollisions: " + collisionCounter + " Moving items: " + _allPhysicsObjects.Count + " ");
		}
    }
}
