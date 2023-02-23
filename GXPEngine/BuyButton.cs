using GXPEngine;
using System;

class BuyButton : Button
{
    const int healing = 10;
    const int damageIncrease = 2;
    const int maxHPIncrease = 10;

    public BuyButton(string filename, int cols, int rows) : base(filename, cols, rows)
    {
    }

    protected override void DoSomething()
    {
        if (HasEnoughCoins() && Input.GetKeyDown(Key.J))
        {
            ((MyGame)game).playerData.playerDamage += damageIncrease;
            SubstractCoins();
        }
        if (((MyGame)game).playerData.lives < ((MyGame)game).playerData.maxLives && HasEnoughCoins() && Input.GetKeyDown(Key.G))
        {
            ((MyGame)game).FindObjectOfType<Player>().GetHealed(healing);
            Console.WriteLine("CurrentHP: {0}", ((MyGame)game).playerData.lives);
            SubstractCoins();
        }
        if (HasEnoughCoins() && Input.GetKeyDown(Key.T))
        {
            ((MyGame)game).FindObjectOfType<Player>().IncreaseMaxHP(maxHPIncrease);
            ((MyGame)game).FindObjectOfType<Player>().GetHealed(healing);
            Console.WriteLine("MaxHP: {0}", ((MyGame)game).playerData.maxLives);
            SubstractCoins();
        }
        if (Input.GetKeyDown(Key.S))
        {
            ((MyGame)game).ResumeGame();
        }
    }

    bool HasEnoughCoins()
    {
        if (((MyGame)game).playerData.coins >= 300)
        {
            return true;
        }
        return false;
    }

    void SubstractCoins()
    {
        ((MyGame)game).playerData.coins -= 0;
        ((MyGame)game).ResumeGame();
    }

    void Update()
    {
        DoSomething();
    }
}
