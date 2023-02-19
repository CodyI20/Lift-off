using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class Pickup : AnimationSprite
{
    public Pickup(string filename, int cols, int rows, TiledLoader obj = null) : base(filename,cols,rows)
    {
        collider.isTrigger= true;
    }

    protected void GrabPickUP(string SoundFile = null)
    {
        if(parent.FindObjectOfType<Player>()!=null && HitTest(parent.FindObjectOfType<Player>()))
        {
            if (SoundFile != null)
            {
                Sound soundToPlay = new Sound(SoundFile);
                soundToPlay.Play();
            }
            DoSomething();
            LateDestroy();
        }
    }

    protected virtual void DoSomething()
    {
    }

    void Update()
    {
    }
}
