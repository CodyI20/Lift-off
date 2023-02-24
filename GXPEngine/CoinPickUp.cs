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
    public CoinPickUp(string filename, int cols, int rows, int coinsAwarded, Player player) : base(filename, cols, rows, player)
    {
        this.coinsAwarded = coinsAwarded;
    }
    protected override void DoSomething()
    {
        Sound coinPick = new Sound("Coin pickup.wav");
        coinPick.Play();
        ((MyGame)game).playerData.coins += coinsAwarded;
        ((MyGame)game).playerData.totalScore += coinsAwarded;
    }

    void Update()
    {
        GrabPickUP();
    }
}
