using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class BuyButton : Button
{
    public BuyButton(string filename, int cols, int rows) : base(filename, cols, rows)
    {

    }

    protected override void DoSomething()
    {
        ((MyGame)game).gameIsPaused = false;
    }

    void Update()
    {
        ButtonFunctionality();
    }
}
