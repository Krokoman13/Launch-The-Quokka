using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Physics;

public class Destructor : PhysicsRectangle
{
    public Destructor (int width, int height, int x, int y) : base(width, height, x, y) 
    {
        trigger = true;
        //SetColor(System.Drawing.Color.Red);
    }

    public override void Collide(PhysicsObject other)
    {
        if (other is Quokka)
        {
            ((Quokka)other).Die();
        }
    }
}

