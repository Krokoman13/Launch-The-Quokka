using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Physics;
using GXPEngine.Core;
using GXPEngine;

public abstract class Placable : GameObject
{
    protected List<PhysicsObject> PhysicsObjects;
    public int width = 0;
    public int height = 0;
    public PhysicsRectangle mainCollider;

    Vector2 oldPos;
    Vector2 position;

    float oldRotation;

    public Placable(Vector2 pPosition) : base(false)
    {
        PhysicsObjects = new List<PhysicsObject>();
        position = pPosition;
        Step();
    }

    protected void Step()
    {
        position.SetXY(x, y);

        foreach (PhysicsObject physicsObject in PhysicsObjects)
        {
            physicsObject.parent = parent;
            physicsObject.position += (position - oldPos);
            physicsObject.vecRotation.RotateDegrees(rotation - oldRotation);
            physicsObject.position.RotateAroundDegrees(rotation - oldRotation, position);
        }

        if (Input.GetMouseButtonDown(0) && IsInside(Input.mouseX, Input.mouseY))
        {
            ((MyGame)game).mouse.recieve(this);
        }
        oldPos = position;
        oldRotation = rotation;
    }

    public override void Destroy()
    {
        base.Destroy();

        foreach (PhysicsObject physicsObject in PhysicsObjects)
        {
            physicsObject.Destroy();
        }
    }

    protected virtual void Run()
    {
        if (mainCollider.collided)
        {
            Collide();
        }
    }

    protected abstract void Collide();

    protected void Update()
    {
        Step();

        if (!(parent is Mouse))
        {
            Run();
        }
    }

    private bool IsInside(int pX, int pY)
    {
        int rx = (int)x - width/2;
        int ry = (int)y - height/2;
        int rw = (int)width;
        int rh = (int)height;

        if (pX >= rx &&         // right of the left edge AND
        pX <= rx + rw &&    // left of the right edge AND
        pY >= ry &&         // below the top AND
        pY <= ry + rh)
        {    // above the bottom
            return true;
        }
        return false;
    }
}
