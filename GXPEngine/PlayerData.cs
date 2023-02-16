using System;
using GXPEngine;
public class PlayerData
{
    const int _startLives = 50;

    int _lives = 0;
    int _coins = 0; // should be placed in HUD
    int _playerDamage = 15;
    //int playerAmmo;

    //public int _playerAmmo
    //{
    //    get { return playerAmmo; }
    //    set { playerAmmo = value; }
    //}

    public int playerDamage
    {
        get { return _playerDamage; }
        set { _playerDamage = value; }
    }
    public int startLives
    {
        get
        {
            return _startLives;
        }
    }

    public int coins
    {
        get
        {
            return _coins;
        }
        set
        {
            _coins = value;
        }
    }
    public int lives
    {
        get
        {
            return _lives;
        }
        set
        {
            int oldLives = _lives;
            _lives = value;
            if (_lives < 0)
            {
                _lives = 0;
                Console.WriteLine("Warning: lives cannon be negative: was {0}, new value: {1}.", oldLives, value);
            }
            Console.WriteLine("Player lives: " + _lives);
        }
    }

    public PlayerData()
    {
        Reset();
    }

    public void Reset()
    {
        _lives = _startLives;
        Console.WriteLine("Resetting player data. Lives = {0}", _lives);
    }
}