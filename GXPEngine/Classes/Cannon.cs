using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Physics;

public class Cannon : Sprite
{
    AnimationSprite body;
    Quokka quokka;

    public Cannon(int pX, int pY) : base(Settings.ASSET_PATH + "Art/Cannonbacksprite.png", true, false)
    {
        x = pX;
        y = pY;

        SetOrigin(width / 2, height / 2);

        body = new AnimationSprite(Settings.ASSET_PATH + "Art/LaucherSheet2.png", 10, 4, 38, false, false);
        body.SetOrigin(body.width/2, body.height/2);
        body.SetCycle(0, 37, 1);
        body.SetXY(x, y);

        PhysicsRectangle rect = new PhysicsRectangle(body.width, body.height, new Vector2(x, y));
        //rect.SetColor(System.Drawing.Color.Red);
        //game.Currentscene.AddChild(rect);

        game.Currentscene.AddChildAt(body, 100);
    }

    void Update()
    {
        if (body.currentFrame > 0 && body.currentFrame <= 35)
        {
            body.Animate();
            quokka.position.SetXY(x, y + 50);
            quokka.velocity = new Vector2(0, 1500f);
            quokka.vecRotation.angleDeg = 0;
        }
        else if (body.currentFrame > 35)
        {
            ((Level)game.Currentscene).mainCam.Shake();
            new Sound(Settings.ASSET_PATH + "SFX/Launching.wav").Play(false, 0, Settings.sfxVolume/2f, 0);
            body.SetFrame(0);
        }

        if (Input.GetKeyUp(Key.S))
        {
            quokka = new Quokka(15);

            quokka.velocity = new Vector2(0, 1500f);
            game.Currentscene.AddChildAt(quokka, 10);

            body.SetFrame(1);
        }
    }
}

