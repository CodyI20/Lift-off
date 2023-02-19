using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class CoinPickUp : Pickup
{
    private int coinsAwarded;
    public CoinPickUp(string filename, int cols, int rows, int coinsAwarded) : base(filename, cols, rows)
    {
        this.coinsAwarded = coinsAwarded;
    }
    protected override void DoSomething()
    {
        ((MyGame)game).playerData.coins += coinsAwarded;
    }

    void Update()
    {
        GrabPickUP();
    }
}
