using GXPEngine;
using TiledMapParser;

class PlayButton : Button
{
    string loadFilename;
    string musicFile;
    public PlayButton(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows)
    {
        if(obj != null)
        {
            loadFilename = obj.GetStringProperty("load");
            musicFile = obj.GetStringProperty("loadMusic");
        }
    }

    protected override void DoSomething()
    {
        if (Input.GetKeyDown(Key.G))
            ((MyGame)game).LoadLevel(loadFilename + ".tmx",false, musicFile);
    }

    void Update()
    {
        DoSomething();
    }
}
