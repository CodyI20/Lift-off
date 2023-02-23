using GXPEngine;
using TiledMapParser;

class PlayButton : Button
{
    string loadFilename;
    public PlayButton(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows)
    {
        loadFilename = obj.GetStringProperty("load", "RPGMap");
    }

    protected override void DoSomething()
    {
        if (Input.GetKeyDown(Key.G))
            ((MyGame)game).LoadLevel(loadFilename + ".tmx");
    }

    void Update()
    {
        DoSomething();
    }
}
