using GXPEngine;
using System;
using System.Drawing.Drawing2D;
using TiledMapParser;

class Bullet : Sprite
{
    protected float vx, vy;
    protected Level level;
    private int bulletDamage;

    public Bullet(string filename, float pVx, float pVy, float pRotation, int bulletDamage) : base(filename)
    {
        level = game.FindObjectOfType<Level>();
        SetOrigin(width / 2, height / 2);
        vx = pVx;
        vy = pVy;
        rotation = pRotation;
        this.bulletDamage = bulletDamage;
    }
    protected void Move()
    {
        x += vx;
        y += vy;
    }
    protected void checkOffScreen()
    {
        if (x + level.x > game.width || x + level.x < 0 || y + level.y < 0 || y + level.y > game.height)
        {
            LateDestroy();
        }
    }
    virtual protected void enemyCollisionCheck(int pBulletDamage)
    {
        GameObject[] enemyHit = GetCollisions();
        for (int i = 0; i < enemyHit.Length; i++)
        {
            if (enemyHit[i] is Enemy)
            {
                Enemy enemy1 = (Enemy)enemyHit[i];
                enemy1.DamageEnemy(pBulletDamage);
                DestroyArrow();
            }
        }
    }
    protected void DestroyArrow()
    {
        Sound arrowHit = new Sound("ArrowImpact.ogg");
        arrowHit.Play();
        LateDestroy();
    }
    void Update()
    {
        if (!((MyGame)game).gameIsPaused)
        {
            Move();
            enemyCollisionCheck(bulletDamage);
            checkOffScreen();
        }
    }
}

