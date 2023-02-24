using GXPEngine;
using System;
using System.Drawing.Drawing2D;
using TiledMapParser;

class Bullet : Sprite
{
    protected float vx, vy;
    protected Level level;
    private int bulletDamage;
    protected float timeOfImpact;
    protected bool hasImpactedBomb = false;
    protected AnimationSprite explosion;

    public Bullet(string filename, float pVx, float pVy, float pRotation, int bulletDamage) : base(filename)
    {
        level = game.FindObjectOfType<Level>();
        SetOrigin(width / 2, height / 2);
        vx = pVx;
        vy = pVy;
        rotation = pRotation;
        this.bulletDamage = bulletDamage;
        explosion = new AnimationSprite("Boom.png",5,1,-1,false,false);
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
                PlayEffects();
                BulletHit();
            }
        }
    }

    virtual protected void PlayEffects()
    {
    }

    protected void BulletHit()
    {
        timeOfImpact = Time.time;
        hasImpactedBomb = true;
        visible = false;
        collider = null;
    }

    protected void DestroyObject()
    {
        if (hasImpactedBomb && Time.time >= timeOfImpact + 400f)
        {
            if(explosion != null)
            {
                explosion.LateDestroy();
            }
            hasImpactedBomb = false;
            LateDestroy();
        }
    }

    void Update()
    {
        if (!((MyGame)game).gameIsPaused)
        {
            Move();
            enemyCollisionCheck(bulletDamage);
            checkOffScreen();
            DestroyObject();
        }
    }
}

