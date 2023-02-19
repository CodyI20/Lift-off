using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class BuyButton : Button
{
    const int healing = 10;
    const int damageIncrease = 2;
    const int maxHPIncrease = 10;


    string filename;
    public BuyButton(string filename, int cols, int rows) : base(filename, cols, rows)
    {
        this.filename = filename;
    }

    protected override void DoSomething()
    {
        if (filename == "square.png")
            ((MyGame)game).playerData.playerDamage += damageIncrease;
        if (((MyGame)game).playerData.lives < ((MyGame)game).playerData.maxLives && HasEnoughCoins() && filename == "colors.png")
        {
            SubstractCoins();
            ((MyGame)game).FindObjectOfType<Player>().GetHealed(healing);
            Console.WriteLine("CurrentHP: {0}", ((MyGame)game).playerData.lives);
        }
        if (HasEnoughCoins() && filename == "circle.png")
        {
            SubstractCoins();
            ((MyGame)game).FindObjectOfType<Player>().IncreaseMaxHP(maxHPIncrease);
            ((MyGame)game).FindObjectOfType<Player>().GetHealed(healing);
            Console.WriteLine("MaxHP: {0}", ((MyGame)game).playerData.maxLives);
        }
        ((MyGame)game).gameIsPaused = false;
    }

    bool HasEnoughCoins()
    {
        if (((MyGame)game).playerData.coins >= 0)
        {
            return true;
        }
        return false;
    }

    void SubstractCoins()
    {
        ((MyGame)game).playerData.coins -= 0;
    }

    void Update()
    {
        ButtonFunctionality();
    }
}
