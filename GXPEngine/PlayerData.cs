using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using GXPEngine;
public class PlayerData
{
    const int _startLives = 100;
    const int _startingDamage = 15;
    const int _playerMaxTries = 1;
    public int playerHighscore;

    int _lives = 0;
    int _maxLives = _startLives;
    int _coins = 0; // should be placed in HUD
    int _tries = 0;
    int _playerDamage = 15;
    public int totalScore = 0;
    //int playerAmmo;

    //public int _playerAmmo
    //{
    //    get { return playerAmmo; }
    //    set { playerAmmo = value; }
    //}

    public int maxTries
    {
        get { return _playerMaxTries; }
    }
    public int tries
    {
        get { return _tries; }
        set { _tries = value; }
    }
    public int playerDamage
    {
        get { return _playerDamage; }
        set { _playerDamage = value; }
    }
    public int maxLives
    {
        get
        {
            return _maxLives;
        }
        set { _maxLives = value; }
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

    public bool SaveData(string filename, int score)
    {
        try
        {
            // StreamWriter: For writing to a text file - requires System.IO namespace:
            // Note: the "using" block ensures that resources are released (writer.Dispose is called) when an exception occurs
            using (StreamWriter writer = new StreamWriter(filename))
            {

                writer.WriteLine("highscore=" + score);

                writer.Close();

                Console.WriteLine("Highscore succesfully uploaded to file " + filename);
                return true;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine("Error while trying to save: {0}", error.Message);
            return false;
        }
    }

    public bool LoadData(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("No save file found!");
            return false;
        }
        try
        {
            // StreamReader: For reading a text file - requires System.IO namespace:
            // Note: the "using" block ensures that resources are released (reader.Dispose is called) when an exception occurs
            using (StreamReader reader = new StreamReader(filename))
            {

                string line = reader.ReadLine();
                while (line != null)
                {
                    // Here's a demo of different string parsing methods:

                    // Find the position of the first '=' symbol (-1 if doesn't exist)
                    int splitPos = line.IndexOf('=');
                    if (splitPos >= 0)
                    {
                        // Everything before the '=' symbol:
                        string key = line.Substring(0, splitPos);
                        // Everything after the '=' symbol:
                        string value = line.Substring(splitPos + 1);

                        // Split value up for every comma:
                        string[] numbers = value.Split(',');

                        switch (key)
                        {
                            case "highscore":
                                if (numbers.Length == 1)
                                {
                                    // These may trigger an exception if the string doesn't represent a float value:
                                    playerHighscore = int.Parse(numbers[0]);
                                    Console.WriteLine("Player highscore has been updated to: ", playerHighscore);
                                }
                                break;
                        }
                    }
                    line = reader.ReadLine();
                }
                reader.Close();

                Console.WriteLine("Load from {0} successful ", filename);
                return true;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine("Error while reading save file: {0}", error.Message);
        }
        return false;
    }

    public PlayerData()
    {
        Reset();
        ResetScore();
    }

    public void Reset()
    {
        _lives = _startLives;
        _coins = 0;
        _playerDamage = _startingDamage;
        Console.WriteLine("Resetting player data. Lives = {0}", _lives);
    }

    public void ResetScore()
    {
        if (_tries >= _playerMaxTries)
        {
            totalScore = 0;
            _tries = 0;
            Console.WriteLine("Resetting player score. TotalScore = {0}", totalScore);
        }
    }
}