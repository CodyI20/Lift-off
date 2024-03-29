﻿using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Player : AnimationSprite
{
    private float initialSpeed = 1f;
    private float playerSpeed = 1f;
    private bool isShooting = false;
    private bool isMoving = false;
    private bool isSprinting = false;
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
            playerData._playerAmmo = obj.GetIntProperty("playerAmmo", 20);
        }
    }
    void Update()
    {
        if (playerHUD == null) playerHUD = game.FindObjectOfType<HUD>();
        Animations();
        Shoot();
        PlayerController();
        SetScore();
    }

    void SetScore()
    {
        playerHUD.SetPlayerScore(playerData.score);
    }

    //Make a new function (what happens to the player when it collides with the enemy in range) & remove the one in Enemy.cs

    public void TakeDamage(int damage)
    {
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
        Sprint();
        if (Input.GetKey(Key.W) || Input.GetKey(Key.S) || Input.GetKey(Key.A) || Input.GetKey(Key.D))
        {
            isMovingP = true;
            //isMoving = true;
        }
        if (Input.GetKey(Key.W))
        {
            Mirror(_mirrorX, false);
            Move(0, -playerSpeed);
        }
        if (Input.GetKey(Key.S))
        {
            Move(0, playerSpeed);
            Mirror(_mirrorX, false);

        }
        if (Input.GetKey(Key.A))
        {
            Move(-playerSpeed, 0);
            if (!isShooting)
                Mirror(true, _mirrorY);
        }
        if (Input.GetKey(Key.D))
        {
            Move(playerSpeed, 0);
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
        ResetPlayerPosition();
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
    void ResetPlayerPosition()
    {
        if (Input.GetKeyDown(Key.SPACE))
        {
            SetXY(game.width / 2, game.height / 2);
            rotation = 0.0f;
        }
    }

    void Sprint()
    {
        bool isSprintingP = false;

        if (Input.GetKey(Key.LEFT_SHIFT))
        {
            isSprintingP = true;
        }
        if (isSprintingP)
        {
            playerSpeed = initialSpeed + 1.5f;
        }
        else
        {
            playerSpeed = initialSpeed;
        }
        isSprinting = isSprintingP;
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && !isShooting && playerData._playerAmmo > 0)
        {
            Sound arrowShot = new Sound("BowDrawn_ArrowRelease.ogg");
            arrowShot.Play();
            shotTime = Time.time;
            isShooting = true;
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
            playerData._playerAmmo--;
            playerHUD.SetPlayerAmmo(playerData._playerAmmo);
            isShooting = false;
            Bullet bullet = new Bullet(_mirrorX ? -5 : 5, 0, this, _mirrorX ? 180 : 0);
            bullet.SetXY(x + (_mirrorX ? -1 : 1) * (width / 2), y);
            parent.AddChild(bullet);
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

