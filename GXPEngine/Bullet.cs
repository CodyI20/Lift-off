using GXPEngine;
using System;
using TiledMapParser;

class Bullet : Sprite
{
    private float vx, vy;
    private Level level;
    private int bulletDamage = 40;

    GameObject owner;
    public Bullet(float pVx, float pVy, GameObject pOwner, float pRotation, TiledObject obj=null) : base("arrow.png")
    {
        level = game.FindObjectOfType<Level>();
        SetOrigin(width / 2, height / 2);
        vx = pVx;
        vy = pVy;
        owner = pOwner;
        rotation = pRotation;
    }
    void Move()
    {
        x += vx;
        y += vy;
    }
    void checkOffScreen()
    {
        if (x + level.x > game.width || x + level.x < 0 || y + level.y < 0 || y + level.y > game.height)
        {
            LateDestroy();
        }
    }
    void enemyCollisionCheck()
    {
        GameObject[] enemyHit = GetCollisions();
        for (int i = 0; i < enemyHit.Length; i++)
        {
            if (enemyHit[i] is Enemy)
            {
                Enemy enemy1 = (Enemy)enemyHit[i];
                enemy1.DamageEnemy(bulletDamage);
                DestroyArrow();
            }
        }
    }
    void DestroyArrow()
    {
        Sound arrowHit = new Sound("ArrowImpact.ogg");
        arrowHit.Play();
        LateDestroy();
    }
    void Update()
    {
        Move();
        enemyCollisionCheck();
        checkOffScreen();
    }
}

