using GXPEngine;                                // GXPEngine contains the engine
using System.Collections.Generic;
using System;
using System.IO.Ports;
using System.Threading;

public class MyGame : Game
{
    public bool gameIsPaused = false;
    public string startLevel =
    "MainMenu.tmx";
    //"TestMap.tmx";
    //"Test.tmx";
    string levelToLoad = null;
    string currentLevel;
    private SoundChannel backgroundMusicSC;
    private float timeItResumed;
    public readonly PlayerData playerData;
    public MyGame() : base(1280, 720, false, false, -1, -1, true)     // Create a window
    {

        playerData = new PlayerData();
        LoadLevel(startLevel);
        OnAfterStep += CheckLoadLevel;

    }
    void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
    }
    void CheckLoadLevel()
    {
        if (levelToLoad != null)
        {
            DestroyAll();
            AddChild(new Level(levelToLoad));
            if (currentLevel != "MainMenu.tmx")
                AddChild(new HUD());
            levelToLoad = null;
        }
    }
    public void LoadLevel(string filename, bool shouldResetPlayerData = false, string soundFile = "BackgroundMusic.ogg")
    {
        if (backgroundMusicSC != null)
            backgroundMusicSC.Stop();
        Sound backgroundMusic = new Sound(soundFile);
        backgroundMusicSC = backgroundMusic.Play();
        levelToLoad = filename;
        currentLevel = filename;
        if (shouldResetPlayerData)
            playerData.Reset();
    }

    void PauseGameSwitch()
    {
        if (!gameIsPaused && levelToLoad != "MainMenu.tmx" && levelToLoad!= "EndScreen.tmx" && Time.time >= timeItResumed + 30000f)
        {
            gameIsPaused = true;
        }
    }
    public void ResumeGame()
    {
        gameIsPaused = false;
        timeItResumed = Time.time;
    }
    public void ResetCurrentLevel()
    {
        DestroyAll();
        playerData.Reset();
        AddChild(new Level(currentLevel));
        AddChild(new HUD());
    }
    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        PauseGameSwitch();
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        //SerialPort port = new SerialPort();
        //port.PortName = "COM4";
        //port.BaudRate = 9600;
        //port.RtsEnable = true;
        //port.DtrEnable = true;
        //port.Open();
        //while (true)
        //{
        //    string line = port.ReadLine(); // read separated values
        //                                   //string line = port.ReadExisting(); // when using characters
        //    //if (line != "")
        //    //{
        //    //    Console.WriteLine("Read from port: " + line);

        //    //}

        //    if (Console.KeyAvailable)
        //    {
        //        ConsoleKeyInfo key = Console.ReadKey();
        //        port.Write(key.KeyChar.ToString());  // writing a string to Arduino
        //    }
        //    Thread.Sleep(30);
        //}
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}