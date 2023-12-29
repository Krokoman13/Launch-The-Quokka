using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine.Physics;
using GXPEngine.Core;
using GXPEngine;                                // GXPEngine contains the engine

public class MyGame : Game
{
	public SoundChannel soundChannel;
	public Mouse mouse;
	public SettingScreen sc;

	public MyGame() : base(1440, 810, false, false)
	{
		targetFps = 60;

		mouse = new Mouse();
		sc = new SettingScreen();

		_sceneManager.addscene(new Menu());
		_sceneManager.addscene(sc);
		_sceneManager.addscene(new MerchScreen());
		_sceneManager.addscene(new Comic());
		_sceneManager.addscene(new Level1());
		_sceneManager.addscene(new Level2());
		_sceneManager.addscene(new Level3());
		_sceneManager.addscene(new Menu());

		soundChannel = new Sound(Settings.ASSET_PATH + "SFX/MenuTheme.wav").Play(false, 0, sc.volume/3f, 0);
	}

    void Update()
	{
		soundChannel.Volume = sc.volume;

		AddChildAt(mouse, 100);
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}
