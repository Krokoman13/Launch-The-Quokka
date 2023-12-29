using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Physics;
using System.Drawing;

public class Level3 : Level
{
    public Level3() : base("level3")
    {
        name = "level3";
        gravity = new Vector2(0, 500f);
        resistance = 0f;
        window.SetXY(420f, 165f);
        window.SetCycle(0, 14, 6);

        plankStock = 5;
        springStock = 4;
        pillowStock = 3;
    }

    public override void onLoad()
    {
        AddChild(new Spikes(30, 0, height - 30));

        AddChild(new Star(105, 680));
        AddChild(new MetalWall(3, 215, 550));

        AddChild(new MetalWall(3, 730, 600));
        AddChild(new MetalWall(2, 730, 600, -90));
        AddChild(new Star(865, 700));
        AddChild(new MetalWall(1, 1135, 600, -90));
        AddChild(new MetalWall(2, 1215, 500));
        AddChild(new Laser(915, 587, -90));

        AddChild(new MetalWall(2, 540, 650));
        AddChild(new MetalWall(2, 538, 360, 90));
        AddChild(new MetalWall(1, 538, 360));
        AddChild(new Laser(360, 125));

        AddChild(new Spikes(4, 965, 170, 180));
        AddChild(new Star(870, 220));
        AddChild(new MetalWall(2, 755, 125));
        AddChild(new MetalWall(2, 965, 125));

        AddChild(new MetalWall(1, 1215, 277));
        AddChild(new MetalWall(3, 1215, 300, -90));
        
        base.onLoad();

        AddChild(new Objective(125, 125, 1175, 750, "settings"));
        rocket.SetXY(1275, 650);

    }
}

public class Level2 : Level
{
    public Level2() : base("level1")
    {
        name = "level2";
        gravity = new Vector2(0, 2000f);
        resistance = 0.006f;
        window.SetXY(102f, 200f);
        window.scale = 1.07f;
        plankStock = springStock = pillowStock = 6;
    }

    public override void onLoad()
    {
        AddChild(new Spikes(30, 0, height - 30));

        AddChild(new MetalWall(3, 295, 75));
        AddChild(new MetalWall(3, 295, 550));
        AddChild(new MetalWall(2, 540, 340));
        AddChild(new Laser(300, 350, -90));
        AddChild(new Star(430, 200));

        AddChild(new MetalWall(3, 800, 75));
        AddChild(new MetalWall(3, 800, 550));
        AddChild(new Star(900, 650));

        AddChild(new MetalWall(3, 1160, 428, -90));
        AddChild(new MetalWall(1, 1160, 80));
        AddChild(new Laser(1173, 170));
        AddChild(new Star(1300, 300));

        base.onLoad();

        AddChild(new Objective(125, 125, 1175, 750, "settings"));
        rocket.SetXY(1075, 650);
    }
}

public class Level1 : Level
{
    public Level1() : base("level1")
    {
        gravity = new Vector2(0, 2000f);
        resistance = 0.006f;
        window.SetXY(102f, 200f);
        window.scale = 1.07f;
        plankStock = springStock = pillowStock = 1;
    }

    public override void onLoad()
    {
        AddChild(new PhysicsLine(0, height, width, height));
        AddChild(new PhysicsLine(0, height + 10, width, height + 10));

        AddChild(new Star(80, 400));

        AddChild(new MetalWall(2, 400, 125));
        AddChild(new Star(412, 400));
        AddChild(new MetalWall(4, 400, 500));

        AddChild(new MetalWall(4, 800, 125));
        AddChild(new Star(812, 600));
        AddChild(new MetalWall(3, 800, 700));

        base.onLoad();

        AddChild(new PopUp(width/2, height/2));
        AddChild(new Objective(125, 125, 1375, 750, "settings"));
        rocket.SetXY(1275, 650);
    }
}

public class Level : Scene 
{
    public Camera mainCam;
    public Vector2 gravity;
    public float resistance;
    public int stars;
    public AnimationSprite rocket;

    protected Sprite panel;
    protected MyGame myGame;

    protected int plankStock;
    protected int springStock;
    protected int pillowStock;

    protected AnimationSprite window;

    //private Sprite background;
    private AnimationSprite starBar;

    public Level(string name) : base(name, Settings.ASSET_PATH + "Art/" + name + "Background.png")
    {
        myGame = (MyGame)game;
        window = new AnimationSprite(Settings.ASSET_PATH + "Art/" + name + "Window.png", 7, 2, 14, false, false);
        window.SetCycle(0, 14, 60);
    }

    public override void Update()
    {
        starBar.SetFrame(stars);
        window.Animate();

        if (Input.GetKeyDown(Key.S))
        {
            ReloadStars();
        }
    }

    private void ReloadStars()
    {
        foreach (GameObject child in GetChildren())
        {
            if (child is Star)
            {
                ((Star)child).StartAgain();
            }
        }

        stars = 0;
    }

    public override void onLoad()
    {
        base.onLoad();

        game.AddChildAt(window, 0);

        AddChild(new PhysicsLine(0, 0, 0, height));
        AddChild(new PhysicsLine(width, height, width, 0));
        AddChild(new PhysicsLine(width, 120, 0, 120));
        AddChildAt(new Cannon(80, 186), 1);

        panel = new Sprite(Settings.ASSET_PATH + "Art/Panel.png", false, false);
        panel.height = 130;

        panel.AddChild(new PlankButton(450, 85, plankStock));
        panel.AddChild(new SpringButton(650, 85, springStock));
        panel.AddChild(new PillowButton(850, 85, pillowStock));

        rocket = new AnimationSprite(Settings.ASSET_PATH + "Art/RocketshipAnimation.png", 6, 3);
        rocket.scale = 0.5f;
        AddChild(rocket);

        panel.AddChild(new ButtenAssembly(73, 31, 3, 0.7f));

        starBar = new AnimationSprite(Settings.ASSET_PATH + "Art/StarBar.png", 4, 1, 4, false, false);
        starBar.SetXY(1150, 50);
        panel.AddChild(starBar);

        AddChildAt(panel, 1000);

        mainCam = new Camera(0, 0, width, height);
        AddChild(mainCam);
    }

    public override void onLeave()
    {
        base.onLeave();
        window.Remove();
    }

    public override void recieveMessage(string message)
    {
        if (message == "dead")
        {
            ReloadStars();
        }
    }
}
