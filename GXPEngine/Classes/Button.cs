using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.Drawing;

public class ButtenAssembly : GameObject
{
    public ButtenAssembly(int pX, int pY, int amount = 3, float pScale = 1) : base(false)
    {
        x = pX;
        y = pY;

        scale = pScale;

        if (amount > 0) AddChild(new LvSwtchButton(0, 80, new SettingScreen(), "Options.png", 2));
        if (amount > 1) AddChild(new LvSwtchButton(0, 0, new Menu(), "Menu.png", 2)); 
        if (amount > 2) AddChild(new ResetButton(0, 160));
    }
}

class LvSwtchButton : Button
{
    private string nextLevel;

    public string level
    {
        set { nextLevel = value; }
    }

    public LvSwtchButton(float inpX, float inpY, Scene nextLevelInp, string path, int cols = 1) : base(inpX, inpY, path, cols)
    {
        nextLevel = nextLevelInp.name;
        //text = nextLevelInp.name;
    }

    public LvSwtchButton(float inpX, float inpY, string nextLevelInp, string path) : base(inpX, inpY, path)
    {
        nextLevel = nextLevelInp;
        //text = nextLevelInp;
    }

    protected override void click()
    {
        base.click();
        if (nextLevel != null)
        {
            game.SceneManager.SetScene(nextLevel);
        }
        else
        {
            base.click();
        }
        new Sound(Settings.ASSET_PATH + "SFX/Button.wav").Play(false, 0, Settings.sfxVolume, 0);
    }
}

public class ControlButton : Button
{
    protected string message;

    public ControlButton(float inpX, float inpY, string action, string path) : base(inpX, inpY, path, 2)
    {
        message = action;
    }

    protected override void click()
    {
        base.click();
        parent.recieveMessage(message);
    }
}

public class BackButton : Button
{
    public BackButton(float inpX, float inpY) : base(inpX, inpY, "Back.png", 2)
    {
    }

    protected override void click()
    {
        base.click();
        game.SceneManager.GotoPreviousscene();
    }
}

public class StopButton : Button
{
    public StopButton(float inpX, float inpY) : base(inpX, inpY, "ExitButton.png", 2)
    {
    }

    protected override void click()
    {
        base.click();
        game.LateDestroy();
    }
}

public class ResetButton : Button
{
    public ResetButton(float inpX, float inpY) : base(inpX, inpY, "Reset.png", 2)
    {
    }

    protected override void click()
    {
        game.SceneManager.Reloadscene();
        new Sound(Settings.ASSET_PATH + "SFX/LevelRetry.wav").Play(false, 0, Settings.sfxVolume, 0);
    }
}

public class Button : AnimationSprite
{
   //private Canvas overlay;

    public Button(float inpX, float inpY, string path = "button.png", int cols = 1) : base(Settings.ASSET_PATH + "Art/" + path, cols, 1, cols, false)
    {
        //overlay = new Canvas(width, height);
        //overlay.SetOrigin(overlay.width / 2, overlay.height / 2);
        //AddChild(overlay);

        x = inpX;
        y = inpY;
        SetOrigin(width / 2, height / 2);
    }

    protected virtual void click()
    {
        int num = Utils.Random(-1, 7);
        new Sound(Settings.ASSET_PATH + "SFX/Button" + num + ".wav").Play(false, 0, Settings.sfxVolume, 0);
    }

    public virtual void Update()
    {
        //var _newFont = new Font("Pangolin", 15);
        //overlay.graphics.Clear(Color.Empty);
        //overlay.graphics.DrawString(text, _newFont, Brushes.White, 90, 50);

        if (HitTestPoint(Input.mouseX, Input.mouseY))
        {
            SetFrame(1);
            //overlay.y = 0;
            //overlay.x = 0;

            ((MyGame)game).mouse.hovering = true;

            if (Input.GetMouseButtonDown(0))
            {
                click();
            }
        }
        else
        {
            SetFrame(0);
            //overlay.y = -3;
            //overlay.x = 1;
        }
    }
}