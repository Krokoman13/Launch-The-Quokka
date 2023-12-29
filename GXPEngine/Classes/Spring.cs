using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Physics;
using GXPEngine;

public class Spring : Placable
{
    AnimationSprite body;

    public Spring() : base(new GXPEngine.Core.Vector2(0, 0))
    {
        body = new AnimationSprite(Settings.ASSET_PATH + "Art/spring.png", 6, 2, 12, false, false);
        width = body.width;
        height = body.height;
        body.SetOrigin(body.width / 2, body.height / 2);
        body.SetCycle(0, 12, 0);

        AddChild(body);

        PhysicsRectangle left = new PhysicsRectangle(2, height, -width / 2, 0);
        //left.SetColor(System.Drawing.Color.Green);
        left.vecRotation.angleDeg = 180f;
        PhysicsObjects.Add(left);

        PhysicsRectangle right = new PhysicsRectangle(2, height, width / 2, 0);
        //right.SetColor(System.Drawing.Color.Pink);
        PhysicsObjects.Add(right);

        mainCollider = new PhysicsRectangle(width - 4, height + 5, 0, 0);
        mainCollider.bouncyness = 0.60f;
        //spring.SetColor(System.Drawing.Color.Red);
        PhysicsRectangle spring2 = new PhysicsRectangle(width - 14, height - 10, 0, 0);
        spring2.bouncyness = 0.60f;
        //PhysicsObjects.Add(spring2);
        PhysicsObjects.Add(mainCollider);
    }

    protected override void Collide()
    {
        body.SetFrame(1);
        new Sound(Settings.ASSET_PATH + "SFX/SpringJump.wav").Play(false, 0, Settings.sfxVolume, 0);
    }

    protected override void Run()
    {
        base.Run();

        if (body.currentFrame > 0)
        {
            body.Animate();
        }
    }
}
