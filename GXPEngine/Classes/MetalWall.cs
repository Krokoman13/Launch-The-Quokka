using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Physics;

class MetalWall : GameObject
{
    PhysicsRectangle rect;
    int width;
    int height;

    public MetalWall(int length, int pX, int pY, int angle = 0)
    {
        x = pX;
        y = pY;

        rotation = angle;
        build(length);

        rect = new PhysicsRectangle(width, height, pX + width/2, pY + height/2);
        rect.SetColor(System.Drawing.Color.Black);
        EasyDraw border = rect.FindObjectOfType<EasyDraw>();
        border.parent = this;
        border.SetXY(width/2 + 3, height/2);
        rect.vecRotation.angleDeg = rotation;
        rect.position.RotateAroundDegrees(rotation, new GXPEngine.Core.Vector2(x, y));
        game.Currentscene.AddChild(rect);
    }

    void build(int length)
    {
        Sprite body;

        for (int i = 0; i < length; i++)
        {
            body = new Sprite(Settings.ASSET_PATH + "Art/metal_wall.png");
            //body.SetOrigin(body.width/2, body.height/2);
            body.x = +body.height;
            body.y = i*body.width;

            body.rotation = 90;

            AddChild(body);

            width = body.height;
            height = length * body.width;
        }
    }
}

