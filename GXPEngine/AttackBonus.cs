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

    void Update()
    {
        if (HitTest(parent.FindObjectOfType<Player>()))
        {
            GrabPickUP();
        }
    }

    protected override void GrabPickUP(string SoundFile = null)
    {
        ((MyGame)game).playerData.playerDamage += attackIncreaseValue;
        Console.WriteLine("Damage = {0}", ((MyGame)game).playerData.playerDamage);
        base.GrabPickUP(SoundFile);
    }
}
