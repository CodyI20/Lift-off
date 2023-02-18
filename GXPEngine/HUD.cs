﻿using GXPEngine;
using System;
using System.Drawing;

class HUD : GameObject
{
    private Sprite buyMenuBackground;
    private BuyButton[] optionButton;
    int healthBarX;
    EasyDraw playerHealthBar;
    //EasyDraw playerAmmoCount;
    EasyDraw playerScore;

    public HUD() : base(false)
    {
        healthBarX = 270;
        playerScore = new EasyDraw(300, 50, false);
        playerScore.TextAlign(CenterMode.Center,CenterMode.Center);
        playerScore.SetXY(150, 20);
        playerScore.Text("Score: " + ((MyGame)game).playerData.coins, 200, 20);
        AddChild(playerScore);
        //playerAmmoCount = new EasyDraw(300, 50, false);
        //playerAmmoCount.TextAlign(CenterMode.Center,CenterMode.Center);
        //playerAmmoCount.SetXY(50, 20);
        //playerAmmoCount.Text("Ammo: " + ((MyGame)game).playerData._playerAmmo, 50, 20);
        //AddChild(playerAmmoCount);
        playerHealthBar = new EasyDraw(healthBarX, 40, false);
        playerHealthBar.ShapeAlign(CenterMode.Min, CenterMode.Min);
        playerHealthBar.Stroke(0, 155);
        playerHealthBar.Fill(Color.Green);
        playerHealthBar.Rect(0, 0, 270, 40);
        playerHealthBar.SetXY(game.width - 270, 20);
        AddChild(playerHealthBar);
        buyMenuBackground = new Sprite("Vignette.png", false, false);
        optionButton = new BuyButton[5]; 
        SetupBuyButtons();
    }

    void SetupBuyButtons()
    {
        optionButton[0] = new BuyButton("square.png", 1, 1);
        optionButton[1] = new BuyButton("colors.png", 1, 1);
        optionButton[2] = new BuyButton("circle.png", 1, 1);
        optionButton[3] = new BuyButton("triangle.png", 1, 1);
        optionButton[4] = new BuyButton("arrow.png", 1, 1);
    }

    public void SetPlayerHealth(float health) //health has to be a float number between 0 and 1
    {
        //playerHealthBar = new EasyDraw((int)(healthBarX * health), 40, false);
        playerHealthBar.Clear(Color.Red);
        playerHealthBar.Fill(Color.Green);
        //AddChild(playerHealthBar);
        playerHealthBar.Rect(0, 0, playerHealthBar.width * health, playerHealthBar.height);
    }
    //public void SetPlayerAmmo(int ammo)
    //{
    //    playerAmmoCount.ClearTransparent();
    //    playerAmmoCount.Text("Ammo: "+ammo,50,20);
    //}

    public void SetPlayerScore(int score)
    {
        playerScore.ClearTransparent();
        playerScore.Text("Score: " + score, 200, 20);
    }

    void BuyMenu()
    {
        if (((MyGame)game).gameIsPaused)
        {
            for(int i = 0; i < 3; i++)
            {
                optionButton[i].width = 100;
                optionButton[i].height = 30;
                optionButton[i].SetXY(game.width / 2, (i+1) * 200);
                AddChild(optionButton[i]);
            }
            AddChild(buyMenuBackground);
        }
        else
        {
            for(int i = 2; i >= 0; i--)
            {
                RemoveChild(optionButton[i]);
            }
            RemoveChild(buyMenuBackground);
        }
    }

    void Update()
    {
        BuyMenu();
    }
    //public void SetEnemyHealth(AnimationSprite owner, float health, bool hasAddedChild = false)
    //{
    //    enemyHealthBar.ShapeAlign(CenterMode.Min, CenterMode.Min);
    //    enemyHealthBar.NoStroke();
    //    enemyHealthBar.Fill(Color.DarkGreen);
    //    enemyHealthBar.Rect(owner.x, owner.y - owner.height / 2, 120, 40);
    //    if (!hasAddedChild)
    //    {
    //        AddChild(enemyHealthBar);
    //        hasAddedChild = true;
    //    }

    //}
}
