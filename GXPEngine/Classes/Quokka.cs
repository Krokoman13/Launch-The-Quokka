using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Physics;
using GXPEngine.Core;
using GXPEngine;

class Quokka : PhysicsCircle
{
    Timer deadTimer;
    AnimationSprite body;

    public Quokka(int pRadius) : base(pRadius, new GXPEngine.Core.Vector2(0, 0))
    {
        //SetColor(System.Drawing.Color.Blue);
        effectedByGravity = true;
        bouncyness = 0.65f;
        
        body = new AnimationSprite(Settings.ASSET_PATH + "Art/Quokka.png", 5, 1, 5, false, false);
        body.SetOrigin(radius, radius);
        AddChild(body);
    }

    public override bool moving
    {
        get { return true; }
    }

    public override void Collide(PhysicsObject other)
    {
        base.Collide(other);

        float reflectStrength = other.bouncyness + bouncyness;
        if (reflectStrength < 0) reflectStrength = 0;

        if (other is PhysicsLine)
        {
            velocity.Reflect(((PhysicsLine)other).lineVector.Normal(), reflectStrength);
        }
        else if (other is PhysicsCircle)
        {
            velocity.Reflect(Vector2.DirectionBetween(position, other.position).Normalized(), reflectStrength);
        }

        if (velocity.length > 100f)
        {
            body.SetFrame(Utils.Random(1, 3));
            
        }
    }

    protected override void applyVelocity()
    {
        if (velocity.length > 1500f)
        {
             velocity.length = 1500f;
        }

        //Console.WriteLine(velocity.length);
        velocity -= ((Level)game.Currentscene).resistance * velocity;
        base.applyVelocity();
    }

    public override void Update()
    {
        if (velocity.length > 200f)
        {
            body.rotation = velocity.angleDeg + 90;
        }

        if (deadTimer != null)
        {
            if (deadTimer.done)
                LateRemove();
        }
        if (game.Currentscene is Level)
        {
            base.Update();
        }
        

        if (Input.GetKeyDown(Key.S))
        {
            LateRemove();
        }
    }

    public void Die()
    {
        if (deadTimer == null)
        {
            deadTimer = new Timer(0.01f);
            AddChild(deadTimer);
            //LateRemove();
            parent.recieveMessage("dead");
        }
    }
}

