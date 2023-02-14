using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class NextLevelGate : AnimationSprite
{
    int scoreToAdvance;
    public NextLevelGate(string filename, int cols, int rows, TiledObject obj = null) : base(filename,cols,rows)
    {
        if (obj != null)
        {
            scoreToAdvance = obj.GetIntProperty("scoreToAdvance", 100);
        }
    }
    void Update()
    {
        Advance();
    }
    void Advance()
    {
        if (((MyGame)game).playerData.score >=scoreToAdvance && HitTest(parent.FindObjectOfType<Player>()) && Input.GetKeyDown(Key.F))
        {
            ((MyGame)game).LoadLevel("EndScreen.tmx");
            Console.WriteLine("Advancing to the next level...");
        }
    }
}
