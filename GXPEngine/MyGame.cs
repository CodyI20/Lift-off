using GXPEngine;                                // GXPEngine contains the engine
using System.Collections.Generic;

public class MyGame : Game
{
    public string startLevel =
    "MainMenu.tmx";
    //".tmx";
    //"TestMap.tmx";
    //"Test.tmx";
    string levelToLoad = null;
    public string currentLevel;
    private SoundChannel backgroundMusicSC;
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
            if (levelToLoad != "EndScreen.tmx" && levelToLoad != "MainMenu.tmx")
                AddChild(new HUD());
            levelToLoad = null;
        }
    }
    public void LoadLevel(string filename, string soundFile = "BackgroundMusic.ogg")
    {
        if (backgroundMusicSC != null)
            backgroundMusicSC.Stop();
        Sound backgroundMusic = new Sound(soundFile);
        backgroundMusicSC = backgroundMusic.Play();
        levelToLoad = filename;
        currentLevel = filename;
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

    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}