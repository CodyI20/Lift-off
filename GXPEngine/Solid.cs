using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Solid : Sprite
{
    public Solid(TiledObject obj=null) : base("empty.png") {
        //A game object that has a collider which doesn't allow passing through
    }
}
