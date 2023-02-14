using GXPEngine;
using System.Drawing;

class HUD : GameObject
{
    EasyDraw playerHealthBar;
    EasyDraw playerAmmoCount;
    EasyDraw playerScore;

    public HUD() : base(false)
    {
        playerScore = new EasyDraw(300, 50, false);
        playerScore.TextAlign(CenterMode.Center,CenterMode.Center);
        playerScore.SetXY(150, 20);
        playerScore.Text("Score: " + ((MyGame)game).playerData.coins, 200, 20);
        AddChild(playerScore);
        playerAmmoCount = new EasyDraw(300, 50, false);
        playerAmmoCount.TextAlign(CenterMode.Center,CenterMode.Center);
        playerAmmoCount.SetXY(50, 20);
        playerAmmoCount.Text("Ammo: " + ((MyGame)game).playerData._playerAmmo, 50, 20);
        AddChild(playerAmmoCount);
        playerHealthBar = new EasyDraw(270, 40, false);
        playerHealthBar.ShapeAlign(CenterMode.Min, CenterMode.Min);
        playerHealthBar.Stroke(0, 155);
        playerHealthBar.Fill(Color.Green);
        playerHealthBar.Rect(0, 0, 270, 40);
        playerHealthBar.SetXY(game.width - 270, 20);
        AddChild(playerHealthBar);
    }

    public void SetPlayerHealth(float health) //health has to be a float number between 0 and 1
    {
        playerHealthBar.Clear(Color.Red);
        playerHealthBar.Fill(Color.Green);
        playerHealthBar.Rect(0, 0, playerHealthBar.width * health, playerHealthBar.height);
    }
    public void SetPlayerAmmo(int ammo)
    {
        playerAmmoCount.ClearTransparent();
        playerAmmoCount.Text("Ammo: "+ammo,50,20);
    }

    public void SetPlayerScore(int score)
    {
        playerScore.ClearTransparent();
        playerScore.Text("Score: " + score, 200, 20);
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
