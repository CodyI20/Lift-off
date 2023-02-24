using GXPEngine;

public class HUD : GameObject
{
    private Sprite buyMenuBackground;
    private Sprite buyMenuForeground;
    private BuyButton[] optionButton;
    private Sprite characterPortrait;
    private Sprite healthBarSprite;
    private Sprite coinSprite;
    private float healthBarInitialWidth;
    //EasyDraw playerHealthBar;
    //EasyDraw playerAmmoCount;
    EasyDraw playerScore;
    EasyDraw playerLives;
    EasyDraw playerEndScore;
    EasyDraw playerHighscore;
    EasyDraw playerCoins;
    private bool hasDisplayedEndUI = false;

    public HUD() : base(false)
    {
        hasDisplayedEndUI = false;
        playerEndScore = new EasyDraw(300, 50, false);
        playerHighscore = new EasyDraw(300, 50, false);
        playerEndScore.Stroke(242, 132, 14, 255);
        playerEndScore.TextFont("8BIT WONDER Nominal Regular.ttf", 20);
        playerEndScore.Text("" + ((MyGame)game).playerData.totalScore);
        playerHighscore.Stroke(245, 249, 46, 255);
        playerHighscore.TextFont("8BIT WONDER Nominal Regular.ttf", 20);
        playerHighscore.Text("" + ((MyGame)game).playerData.playerHighscore);
        playerEndScore.SetOrigin(0, 0);
        playerHighscore.SetOrigin(0, 0);
        playerEndScore.SetXY(700, 306);
        playerHighscore.SetXY(600, 332);
        AddChild(playerEndScore);
        AddChild(playerHighscore);
        playerEndScore.visible = false;
        playerHighscore.visible = false;
        playerLives = new EasyDraw(300, 50, false);
        playerLives.TextAlign(CenterMode.Min, CenterMode.Center);
        playerLives.SetXY(600, 10);
        playerLives.Text("Lives left: " + (((MyGame)game).playerData.maxTries - ((MyGame)game).playerData.tries));
        AddChild(playerLives);
        characterPortrait = new Sprite("player_healthUI.png", false, false);
        characterPortrait.SetOrigin(0, characterPortrait.height);
        characterPortrait.SetXY(100, 100);
        AddChild(characterPortrait);
        healthBarSprite = new Sprite("player_HealthBarFull.png", false, false);
        healthBarSprite.SetOrigin(0, healthBarSprite.height);
        healthBarSprite.SetXY(173, 53); // x = characterPortrait.x + 73; y = characterPortrait.y-47;
        AddChild(healthBarSprite);
        healthBarInitialWidth = healthBarSprite.width;
        playerScore = new EasyDraw(300, 50, false);
        playerScore.TextAlign(CenterMode.Center, CenterMode.Center);
        playerScore.SetXY(150, 20);
        playerScore.Text("Coins: " + ((MyGame)game).playerData.coins, 200, 20);
        AddChild(playerScore);
        coinSprite = new Sprite("Coin.png", false, false);
        coinSprite.SetOrigin(coinSprite.width / 2, coinSprite.height / 2);
        coinSprite.SetXY(555, 200);
        playerCoins = new EasyDraw(300, 50, false);
        playerCoins.SetXY(470, 172);
        //playerAmmoCount = new EasyDraw(300, 50, false);
        //playerAmmoCount.TextAlign(CenterMode.Center,CenterMode.Center);
        //playerAmmoCount.SetXY(50, 20);
        //playerAmmoCount.Text("Ammo: " + ((MyGame)game).playerData._playerAmmo, 50, 20);
        //AddChild(playerAmmoCount);
        //playerHealthBar = new EasyDraw(healthBarX, 40, false);
        //playerHealthBar.ShapeAlign(CenterMode.Min, CenterMode.Min);
        //playerHealthBar.Stroke(0, 155);
        //playerHealthBar.Fill(Color.Green);
        //playerHealthBar.Rect(0, 0, 270, 40);
        //playerHealthBar.SetXY(game.width - 270, 20);
        //AddChild(playerHealthBar);
        buyMenuBackground = new Sprite("Vignette.png", false, false);
        buyMenuForeground = new Sprite("BuyMenu.png", false, false);
        buyMenuForeground.SetOrigin(buyMenuForeground.width / 2, buyMenuForeground.height / 2);
        buyMenuForeground.SetXY(game.width / 2, game.height / 2);
        optionButton = new BuyButton[3];
        SetupBuyButtons();
    }

    void SetupBuyButtons()
    {
        optionButton[0] = new BuyButton("AttackBuff.png", 1, 1);
        optionButton[1] = new BuyButton("SpeedBuff.png", 1, 1);
        optionButton[2] = new BuyButton("HealthBuff.png", 1, 1);
    }

    public void SetPlayerHealth(float health) //health has to be a float number between 0 and 1
    {
        if (healthBarSprite.width * health <= healthBarInitialWidth)
            healthBarSprite.width = healthBarInitialWidth * health;
        else
            healthBarSprite.width = healthBarInitialWidth;
    }
    //public void SetPlayerAmmo(int ammo)
    //{
    //    playerAmmoCount.ClearTransparent();
    //    playerAmmoCount.Text("Ammo: "+ammo,50,20);
    //}

    public void SetPlayerTries(int tries)
    {
        playerLives.ClearTransparent();
        playerLives.Text("Lives left: " + tries);
    }

    public void SetPlayerScore(int score)
    {
        playerScore.ClearTransparent();
        playerScore.Text("Score: " + score, 200, 20);
    }

    void BuyMenu()
    {
        if (((MyGame)game).gameIsPaused)
        {
            AddChild(buyMenuForeground);
            playerCoins.ClearTransparent();
            playerCoins.TextAlign(CenterMode.Center, CenterMode.Center);
            playerCoins.Text("" + ((MyGame)game).playerData.coins);
            AddChild(playerCoins);
            AddChild(coinSprite);
            for (int i = 0; i < 3; i++)
            {
                optionButton[i].width = 64;
                optionButton[i].height = 64;
                optionButton[i].SetXY((game.width + optionButton[i].width) / 2.2f, 140 + (i + 1) * 110);
                AddChild(optionButton[i]);
            }
            AddChild(buyMenuBackground);
        }
        else
        {
            for (int i = 2; i >= 0; i--)
            {
                RemoveChild(optionButton[i]);
            }
            RemoveChild(buyMenuBackground);
            RemoveChild(buyMenuForeground);
            RemoveChild(coinSprite);
            RemoveChild(playerCoins);
        }
    }

    public void EndScreenUI()
    {
        characterPortrait.visible = false;
        playerScore.visible = false;
        playerLives.visible = false;
        healthBarSprite.visible = false;
        playerEndScore.ClearTransparent();
        playerEndScore.Text("" + ((MyGame)game).playerData.totalScore);
        playerEndScore.visible = true;
        playerHighscore.ClearTransparent();
        if (((MyGame)game).playerData.totalScore > ((MyGame)game).playerData.playerHighscore)
            playerHighscore.Text("" + ((MyGame)game).playerData.totalScore);
        else
            playerHighscore.Text("" + ((MyGame)game).playerData.playerHighscore);
        playerHighscore.visible = true;
    }

    void Update()
    {
        BuyMenu();
        if (((MyGame)game).currentLevel == "EndScreen.tmx" && !hasDisplayedEndUI)
        {
            EndScreenUI();
            hasDisplayedEndUI = true;
        }
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
