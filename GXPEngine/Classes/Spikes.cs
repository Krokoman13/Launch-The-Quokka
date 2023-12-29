using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Physics;

class Spikes : GameObject
{
    PhysicsRectangle rect;
    int width;
    int height;

    public Spikes(int length, int pX, int pY, int angle = 0)
    {
        x = pX;
        y = pY;

        rotation = angle;
        build(length);

        rect = new Destructor(width, height, pX + width / 2, pY + height / 2);
        //rect.SetColor(System.Drawing.Color.Green);
        //rect.SetColor(System.Drawing.Color.Red);
        rect.vecRotation.angleDeg = rotation;
        rect.position.RotateAroundDegrees(rotation, new GXPEngine.Core.Vector2(x, y));
        game.Currentscene.AddChild(rect);
    }

    void build(int length)
    {
        Sprite body;

        for (int i = 0; i < length; i++)
        {
            body = new Sprite(Settings.ASSET_PATH + "Art/Spike.png");
            //body.SetOrigin(body.width/2, body.height/2);
            body.x = i * body.width;

            AddChild(body);

            width = length * body.width;
            height = body.height;
        }
    }
}

