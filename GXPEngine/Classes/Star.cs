using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Physics;

public class Star : PhysicsCircle
{
    AnimationSprite body;
    //Timer respawnTimer;
    bool active;

    public Star(int pX, int pY) : base(35, new GXPEngine.Core.Vector2(pX, pY))
    {
        trigger = true;

        StartAgain();

        body = new AnimationSprite(Settings.ASSET_PATH + "Art/StarSheet.png", 7, 3, 21);
    }

    public void StartAgain()
    {
        active = true;
    }

    public override void Update()
    {
        base.Update();

        if (body.parent == null && active)
        {
            body = new AnimationSprite(Settings.ASSET_PATH + "Art/StarSheet.png", 7, 3, 21);
            body.SetOrigin(body.width / 2, body.height / 2);
            body.SetCycle(0, 12, 5);
            AddChild(body);
        }

        body.Animate();

        if (body.currentFrame > 19)
        {
            body.LateDestroy();
        }
    }


    public override void Collide(PhysicsObject other)
    {
        if (other is Quokka && active)
        {
            ((Level)game.Currentscene).stars++;
            body.SetCycle(14, 7, 5);
            active = false;
            new Sound(Settings.ASSET_PATH + "SFX/Star1.wav").Play(false, 0, Settings.sfxVolume, 0);
        }
    }
}

