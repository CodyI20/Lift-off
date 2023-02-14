using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Solid : AnimationSprite
{
    public Solid(string filename, int cols, int rows, TiledObject obj=null) : base(filename, cols, rows) {
        //A game object that has a collider which doesn't allow passing through
    }
}
