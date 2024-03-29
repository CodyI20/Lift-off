﻿using System;
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

    virtual protected void GrabPickUP(string SoundFile = null)
    {
        if (SoundFile != null)
        {
            Sound soundToPlay = new Sound(SoundFile);
            soundToPlay.Play();
        }
        LateDestroy();
    }

    void Update()
    {

    }
    //create a virtual class for pickups

}
