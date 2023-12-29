using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Managers;

class Comic : Screen
{
    private AnimationSprite comic;
    private int count;

    public Comic() : base("Comic")
    {
    }

    public override void Update()
    {
        base.Update();

        comic.SetFrame(count);

        if (Input.GetMouseButtonUp(0))
        {
            count++;
            if (count >= 5) game._sceneManager.GotoNextscene();
        }
    }

    public override void onLoad()
    {
        base.onLoad();
        count = -1;

        comic = new AnimationSprite(Settings.ASSET_PATH + "Art/BeginningComic.png", 5, 1, 5, false, false);
        AddChild(comic);

        AddChild(new BackButton(120, 750));
    }
}

public class Menu : Screen
{
    public Menu() : base("Menu"/*, name + "Background.png"*/)
    {
        //
    }

    public override void onLoad()
    {
        base.onLoad();
        AddChild(new ButtenAssembly(120, 60, 1));
        AddChild(new LvSwtchButton(1315, 750, new MerchScreen(), Settings.ASSET_PATH + "Art/Merch.png", 2));
        AddChild(new StopButton(120, 750));

        AddChild(new LvSwtchButton(500, 400, new Comic(), Settings.ASSET_PATH + "Art/lv1.png", 2));
        AddChild(new LvSwtchButton(750, 400, new Level2(), Settings.ASSET_PATH + "Art/lv2.png", 2));
        AddChild(new LvSwtchButton(1000, 400, new Level3(), Settings.ASSET_PATH + "Art/lv3.png", 2));

        Sprite logo = new Sprite(Settings.ASSET_PATH + "Art/Logo.png", false, false);
        logo.scale = 0.15f;
        logo.SetXY(1130, 30);
        AddChild(logo);
    }
}

public class MerchScreen : Screen
{
    public MerchScreen() : base("MerchScreen")
    {
        //
    }

    public override void onLoad()
    {
        base.onLoad();
        AddChild(new Sprite(Settings.ASSET_PATH + "Art/MerchFrame.png"));
        AddChild(new ButtenAssembly(120, 60, 2));
        AddChild(new BackButton(120, 750));
    }
}

public class SettingScreen : Screen
{
    public float volume = 5;
    public float sfxVolume = 5;

    public SettingScreen() : base("Settings") { }

    Font font = new Font("Poetsen One", 30);

    public override void onLoad()
    {   
        Sprite sprite = new Sprite(Settings.ASSET_PATH + "Art/Music.png");

        sprite.x = 375;
        sprite.y = 75;

        AddChild(sprite);
        AddChild(new ControlButton(500, 225, "-", "Minus.png"));
        AddChild(new ControlButton(1000, 225, "+", "Plus.png"));


        sprite = new Sprite(Settings.ASSET_PATH + "Art/SFX.png");

        sprite.x = 375;
        sprite.y = 475;

        AddChild(sprite);
        AddChild(new ControlButton(500, 625, "-SFX", "Minus.png"));
        AddChild(new ControlButton(1000, 625, "+SFX", "Plus.png"));

        AddChild(new LvSwtchButton(120, 60, new Menu(), "Menu.png", 2));
        AddChild(new BackButton(120, 750));

        base.onLoad();
    }

    public override void Update()
    {
        overlay.graphics.Clear(Color.Empty);
        overlay.graphics.DrawString(volume.ToString(), font, Brushes.White, 710, 210);

        overlay.graphics.DrawString(sfxVolume.ToString(), font, Brushes.White, 710, 610);
    }

    public override void recieveMessage(string message)
    {
        if(!(volume < 0) || (volume > 10))
        {
            if(volume == 0)
            {
                //
            }
            else
            {
                if(message == "-")
                {
                    volume--;
                }
            }
            if (volume == 10)
            {
                //
            }
            else
            {
                if (message == "+")
                {
                    volume++;
                }
            }
        }

        if (message == "-SFX")
        {
            sfxVolume--;
        }
        if (message == "+SFX")
        {
            sfxVolume++;
        }

        if (sfxVolume < 0) sfxVolume = 0;
        if (sfxVolume > 10) sfxVolume = 10;

        Settings.sfxVolume = sfxVolume / 2f;
    }
}

public class Screen : Scene
{
    protected Canvas overlay;

    public Screen(string name) : base(name, Settings.ASSET_PATH + "Art/menuBackground.png")
    {
    }

    public override void onLoad()
    {
        base.onLoad();

        overlay = new Canvas(width, height, false);
        AddChild(overlay);
    }
}
