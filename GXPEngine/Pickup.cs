using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class Pickup : AnimationSprite
{
    protected Player player1;
    public Pickup(string filename, int cols, int rows, Player player) : base(filename,cols,rows)
    {
        player1= player;
        collider.isTrigger= true;
    }

    protected void GrabPickUP(string SoundFile = null)
    {
        if (player1!=null && HitTest(player1))
        {
            if (SoundFile != null)
            {
                Sound soundToPlay = new Sound(SoundFile);
                soundToPlay.Play();
            }
            DoSomething();
            LateDestroy();
        }
        else
        {
            return;
        }
    }

    protected virtual void DoSomething()
    {
    }

    void Update()
    {
    }
}
