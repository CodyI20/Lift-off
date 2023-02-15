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
        this.bulletDamage = bulletDamage * 2;
    }
    void Update()
    {
        enemyCollisionCheck(bulletDamage);
        Move();
        checkOffScreen();
    }
    protected override void enemyCollisionCheck(int pBulletDamage)
    {
        base.enemyCollisionCheck(pBulletDamage);
    }
}
