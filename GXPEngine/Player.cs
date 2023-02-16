using GXPEngine;
using TiledMapParser;

class Player : AnimationSprite
{
    private float initialSpeed = 1f;
    private float playerSpeed = 1f;
    private float timePlayerGotHit;
    private bool isShooting = false;
    private bool isMoving = false;
    private bool isSprinting = false;
    private int baseBulletDamage;
    private float shotTime; // Time the player pressed the button to shoot
    private PlayerData playerData;
    private HUD playerHUD = null;

    public Player(TiledObject obj) : base("Adventurer_2.0.png", 7, 18)
    {
        SetXY(game.width / 2, game.height / 2);
        playerData = ((MyGame)game).playerData;
        if (obj != null)
        {
            initialSpeed = obj.GetFloatProperty("initialSpeed", 1f);
            playerSpeed = obj.GetFloatProperty("playerSpeed", 1f);
            //playerData._playerAmmo = obj.GetIntProperty("playerAmmo", 20);
            baseBulletDamage = obj.GetIntProperty("baseBulletDamage", 10);
        }
    }
    void Update()
    {
        if (playerHUD == null) playerHUD = game.FindObjectOfType<HUD>();
        if (Time.time >= timePlayerGotHit + 300f)
            SetColor(1f, 1f, 1f);
        Animations();
        Shoot();
        PlayerController();
        SetScore();
    }

    void SetScore()
    {
        playerHUD.SetPlayerScore(playerData.coins);
    }

    //Make a new function (what happens to the player when it collides with the enemy in range) & remove the one in Enemy.cs

    public void TakeDamage(int damage)
    {
        timePlayerGotHit = Time.time;
        SetColor(0.8f, 0, 0);
        playerData.lives -= damage;
        playerHUD.SetPlayerHealth((float)playerData.lives / playerData.startLives);
        if (playerData.lives <= 0)
        {
            ((MyGame)game).ResetCurrentLevel();
        }
    }

    public void GetHealed(int heal)
    {
        if (playerData.lives + heal <= playerData.startLives)
            playerData.lives += heal;
        else
            playerData.lives = playerData.startLives;
        playerHUD.SetPlayerHealth((float)playerData.lives / playerData.startLives);
    }
    void PlayerController()
    {
        float oldX = x;
        float oldY = y;
        bool isMovingP = false;
        float speedOnDelta = playerSpeed * Time.deltaTime;
        //Sprint();
        if (Input.GetKey(Key.W) || Input.GetKey(Key.S) || Input.GetKey(Key.A) || Input.GetKey(Key.D))
        {
            isMovingP = true;
            //isMoving = true;
        }
        if (Input.GetKey(Key.W))
        {
            Mirror(_mirrorX, false);
            Move(0, -speedOnDelta);
        }
        if (Input.GetKey(Key.S))
        {
            Move(0, speedOnDelta);
            Mirror(_mirrorX, false);

        }
        if (Input.GetKey(Key.A))
        {
            Move(-speedOnDelta, 0);
            if (!isShooting)
                Mirror(true, _mirrorY);
        }
        if (Input.GetKey(Key.D))
        {
            Move(speedOnDelta, 0);
            if (!isShooting)
                Mirror(false, _mirrorY);
        }
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is Solid)
            {
                x = oldX;
                y = oldY;
            }
        }
        isMoving = isMovingP;
        //ResetPlayerPosition();
    }
    void Animations()
    {
        if (isShooting)
        {
            SetCycle(112, 6);
        }
        if (isMoving && !isShooting)
        {
            if (isSprinting)
                SetCycle(9, 4);
            else
                SetCycle(34, 4);
        }
        else if (!isMoving && !isShooting)
            SetCycle(0);
        Animate(0.1f);
    }

    void CollisionChecker()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is Solid)
            {
                MoveUntilCollision(x, y);
            }
        }
    }
    //void ResetPlayerPosition()
    //{
    //    if (Input.GetKeyDown(Key.SPACE))
    //    {
    //        SetXY(game.width / 2, game.height / 2);
    //        rotation = 0.0f;
    //    }
    //}

    //void Sprint()
    //{
    //    bool isSprintingP = false;

    //    if (Input.GetKey(Key.LEFT_SHIFT))
    //    {
    //        isSprintingP = true;
    //    }
    //    if (isSprintingP)
    //    {
    //        playerSpeed = initialSpeed + 1.5f;
    //    }
    //    else
    //    {
    //        playerSpeed = initialSpeed;
    //    }
    //    isSprinting = isSprintingP;
    //}

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (Input.mouseX < x + game.FindObjectOfType<Level>().x)
                _mirrorX = true;
            else
                _mirrorX = false;
        }
        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            Sound arrowShot = new Sound("BowDrawn_ArrowRelease.ogg");
            arrowShot.Play();
            shotTime = Time.time;
            isShooting = true;
            Bullet bullet = new Bullet("arrow.png", _mirrorX ? -5 : 5, 0, _mirrorX ? 180 : 0, baseBulletDamage);
            bullet.SetXY(x + (_mirrorX ? -1 : 1) * (width / 2), y);
            parent.AddChild(bullet);
        }
        if (Input.GetMouseButtonDown(1) && !isShooting)
        {
            shotTime = Time.time;
            isShooting = true;
            AtomicBullet bullet = new AtomicBullet("arrow.png", _mirrorX ? -5 : 5, 0, _mirrorX ? 180 : 0, baseBulletDamage);
            bullet.SetXY(x + (_mirrorX ? -1 : 1) * (width / 2), y);
            parent.AddChild(bullet);
        }
        if (isShooting)
        {
            if (Input.mouseX < x + game.FindObjectOfType<Level>().x)
                Mirror(true, _mirrorY);
            else
                Mirror(false, _mirrorY);
        }
        if (Time.time >= shotTime + 1000f && isShooting)
        {
            //playerData._playerAmmo--;
            //playerHUD.SetPlayerAmmo(playerData._playerAmmo);
            isShooting = false;
            //Bullet bullet = new Bullet("arrow.png",_mirrorX ? -5 : 5, 0, _mirrorX ? 180 : 0, baseBulletDamage);
            //bullet.SetXY(x + (_mirrorX ? -1 : 1) * (width / 2), y);
            //parent.AddChild(bullet);
        }
    }

    //int GetHorizontalInput()
    //{
    //    if (Input.GetKeyDown(Key.D))
    //        return 1;
    //    if (Input.GetKeyDown(Key.A))
    //        return -1;
    //    return 0;
    //}
    //int GetVerticalInput()
    //{
    //    if (Input.GetKeyDown(Key.W))
    //        return -1;
    //    if (Input.GetKeyDown(Key.S))
    //        return 1;
    //    return 0;
    //}
}

