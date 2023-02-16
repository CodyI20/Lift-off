using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class AttackBonus : Pickup
{
    int attackIncreaseValue;
    public AttackBonus(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows)
    {
        if (obj != null)
        {
            attackIncreaseValue = obj.GetIntProperty("attackIncreaseValue", 1);
        }
    }

    protected override void GrabPickUP(string SoundFile = null)
    {
        //player.
        base.GrabPickUP(SoundFile);
    }
}
