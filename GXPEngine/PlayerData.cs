﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GXPEngine;

public class PlayerData
{
    const int _startLives = 100;

    int _lives = 0;
    int _score = 0; // should be placed in HUD
    int playerAmmo;
    public int _playerAmmo
    {
        get { return playerAmmo; }
        set { playerAmmo = value; }
    }
    public int startLives
    {
        get
        {
            return _startLives;
        }
    }

    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
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