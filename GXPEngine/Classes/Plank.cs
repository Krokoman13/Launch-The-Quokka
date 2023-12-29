using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Physics;

class Plank : Placable
{
    public Plank() : base(new Vector2(0, 0))
    {
        //build(length);
        Sprite body = new Sprite(Settings.ASSET_PATH + "Art/plank.png", false, false);
        width = body.width;
        height = body.height;
        body.SetOrigin(body.width / 2, body.height / 2);

        AddChild(body);

        mainCollider = new PhysicsRectangle(width, height, new Vector2(0, 0));
        //rect.SetColor(System.Drawing.Color.Red);
        PhysicsObjects.Add(mainCollider);

        mainCollider = new PhysicsRectangle(width - 10, height - 10, new Vector2(0, 0));
        PhysicsObjects.Add(mainCollider);
    }

    void build(int length)
    {
        Sprite body;

        for (int i = 0; i < length;)
        {
            body = new Sprite(Settings.ASSET_PATH + "Art/metal_wall.png");
            //body.SetOrigin(body.width/2, body.height/2);

            body.x = i;

            height = body.height;

            i += body.width;
            AddChild(body);

            width = i;
        }
    }

    protected override void Collide()
    {
        new Sound(Settings.ASSET_PATH + "SFX/Bouncing.wav").Play(false, 0, Settings.sfxVolume, 0);
    }

    protected override void Run()
    {
        base.Run();
    }
}
