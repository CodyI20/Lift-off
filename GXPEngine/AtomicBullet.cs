using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class AtomicBullet : Bullet
{
    int bulletDamage;
    public AtomicBullet(string filename, float pVx, float pVy, float pRotation, int bulletDamage) : base(filename, pVx, pVy, pRotation, bulletDamage)
    {
        this.bulletDamage = bulletDamage * 3;
    }

    void DestroyArrow()
    {
        timeOfImpact = Time.time;
        hasImpactedBomb = true;
        Sound bombHit = new Sound("Explosion.wav");
        bombHit.Play();
        explosion.SetXY(x, y);
        explosion.SetCycle(0, 5);
        parent.AddChild(explosion);
    }

    void Update()
    {
        if (!((MyGame)game).gameIsPaused)
        {
            enemyCollisionCheck(bulletDamage);
            Move();
            checkOffScreen();
            DestroyObject();
            explosion.Animate(0.1f);
        }
    }
    protected override void enemyCollisionCheck(int pBulletDamage)
    {
        base.enemyCollisionCheck(pBulletDamage);
    }

    protected override void PlayEffects()
    {
        Sound arrowHit = new Sound("Explosion.wav");
        arrowHit.Play();
        explosion.SetXY(x, y);
        explosion.SetCycle(0, 5);
        parent.AddChild(explosion);
        explosion.Animate(0.2f);
    }
}
