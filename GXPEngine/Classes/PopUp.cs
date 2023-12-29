using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class PopUp : Sprite
{
    public PopUp(int pX, int pY) : base(Settings.ASSET_PATH + "Art/tutorialPopup.png")
    {
        x = pX;
        y = pY;

        SetOrigin(width/2, height/2);
    }

    void Update()
    {
        if (HitTestPoint(Input.mouseX, Input.mouseY))
        {
            ((MyGame)game).mouse.hovering = true;

            if (Input.GetMouseButtonUp(0))
            {
                LateDestroy();
            }
        }
    }
}

