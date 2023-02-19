using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class BuyButton : Button
{
    string filename;
    public BuyButton(string filename, int cols, int rows) : base(filename, cols, rows)
    {
        this.filename = filename;
    }

    protected override void DoSomething()
    {
        if (filename == "square.png")
            ((MyGame)game).playerData.playerDamage += 2;
        ((MyGame)game).gameIsPaused = false;
    }

    void Update()
    {
        ButtonFunctionality();
    }
}
