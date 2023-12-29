using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine;

public class Mouse : GameObject
{
    Vector2 position;
    Vector2 oldPosition;

    AnimationSprite cursor;

    public bool hovering;
    Placable equipped;

    Vector2 direction
    {
        get { return position - oldPosition; }
    }

    public Mouse() : base(false)
    {
        cursor = new AnimationSprite(Settings.ASSET_PATH + "Art/Cursor.png", 3, 1, 3, false, false);
        cursor.SetFrame(1);
        cursor.scale = 0.5f;

        game.ShowMouse(false);
        AddChild(cursor);
    }

    public void Update()
    {
        oldPosition = position;
        SetXY(Input.mouseX, Input.mouseY);
        position = new Vector2(x, y);

        cursor.SetFrame(1);
        cursor.SetOrigin(50, 5);

        if (equipped != null)
        {
            cursor.SetFrame(2);
            cursor.SetOrigin(70, 10);

            if (Input.GetKeyDown(Key.A))
            {
                equipped.rotation += -22.5f;
            }

            if (Input.GetKeyDown(Key.D))
            {
                equipped.rotation += 22.5f;
            }
        }

        if (Input.GetMouseButtonUp(0) && equipped != null)
        {
            release();
        }

        if (hovering)
        {
            cursor.SetFrame(0);
            cursor.SetOrigin(60, 10);
            hovering = false;
        }
    }

    private void release()
    {
        if (y < 120)
        {
            if (equipped is Plank)
            {
                ((PlankButton)game.Currentscene.FindObjectOfType(typeof(PlankButton))).stock ++;
            }
            if (equipped is Spring)
            {
                ((SpringButton)game.Currentscene.FindObjectOfType(typeof(SpringButton))).stock++;
            }
            if (equipped is Pillow)
            {
                ((PillowButton)game.Currentscene.FindObjectOfType(typeof(PillowButton))).stock++;
            }

            equipped.Destroy();
        }
        else
        {
            equipped.SetXY(position.x, position.y);
            game.Currentscene.AddChild(equipped);
        }

        equipped = null;
    }

    public void recieve(Placable placable)
    {
        if (equipped == null)
        {
            equipped = placable;

            equipped.SetXY(0, 0);
            AddChildAt(equipped,0);
        }
    }
}

