using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;

public class PillowButton : PlacementButtons
{
    public PillowButton(float inpX, float inpY, int pStock = 1) : base(inpX, inpY, "PillowButton", pStock)
    {
    }

    protected override void click()
    {
        base.click();
        ((MyGame)game).mouse.recieve(new Pillow());
    }
}

public class SpringButton : PlacementButtons
{
    public SpringButton(float inpX, float inpY, int pStock = 1) : base(inpX, inpY, "Springbutton", pStock)
    {
    }

    protected override void click()
    {
        base.click(); 
        ((MyGame)game).mouse.recieve(new Spring());
    }
}

public class PlankButton : PlacementButtons
{
    public PlankButton(float inpX, float inpY, int pStock = 1) : base(inpX, inpY, "PlankButton", pStock)
    {
    }

    protected override void click()
    {
        base.click();
        ((MyGame)game).mouse.recieve(new Plank());
    }
}

public abstract class PlacementButtons : Button
{
    Canvas overlay;
    public int stock;
    protected Placable placable;

    protected PlacementButtons(float inpX, float inpY, string path, int pStock = 1) : base(inpX, inpY, path + ".png", 3)
    {
        overlay = new Canvas(width, height * 3);
        overlay.SetOrigin(overlay.width / 2, overlay.height / 2);
        AddChild(overlay);

        stock = pStock;
    }

    public override void Update()
    {
        var _newFont = new Font("Poetsen One", 25);
        overlay.graphics.Clear(Color.Empty);
        overlay.graphics.DrawString(stock.ToString(), _newFont, Brushes.Black, 65, 110);

        if (stock > 0)
        {
            base.Update();
        }
        else
        {
            SetFrame(2);
        }
    }

    protected override void click()
    {
        stock--;
        new Sound(Settings.ASSET_PATH + "SFX/PlaceObject.wav").Play(false, 0, Settings.sfxVolume, 0);
    }
}

