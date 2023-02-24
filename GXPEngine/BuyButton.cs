using GXPEngine;
using System;

class BuyButton : Button
{
    const int healing = 20;
    const int damageIncrease = 2;
    const int maxHPIncrease = 20;
    const float speedIncrease = 0.1f;
    const int upgradeCost = 500;
    bool hasBought = false;

    public BuyButton(string filename, int cols, int rows) : base(filename, cols, rows)
    {
        hasBought = false;
    }

    protected override void DoSomething()
    {
        if (HasEnoughCoins() && Input.GetKeyUp(Key.J) && !hasBought)
        {
            ((MyGame)game).playerData.playerDamage += damageIncrease;
            SubstractCoins();
        }
        if (HasEnoughCoins() && Input.GetKeyUp(Key.G) && !hasBought)
        {
            ((MyGame)game).FindObjectOfType<Player>().IncreaseSpeed(speedIncrease);
            Console.WriteLine("CurrentHP: {0}", ((MyGame)game).playerData.lives);
            SubstractCoins();
        }
        if (HasEnoughCoins() && Input.GetKeyUp(Key.T) && !hasBought)
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
        if (((MyGame)game).playerData.coins >= upgradeCost)
        {
            return true;
        }
        return false;
    }

    void SubstractCoins()
    {
        Sound boughtSomething = new Sound("Buy Upgrade.wav");
        boughtSomething.Play();
        hasBought = true;
        ((MyGame)game).playerData.coins -= upgradeCost;
        ((MyGame)game).ResumeGame();
    }

    void Update()
    {
        if (!hasBought)
            DoSomething();
    }
}
