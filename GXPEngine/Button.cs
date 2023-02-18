using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class Button : AnimationSprite
{
    public Button(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows)
    {
        
    }

    protected void ButtonFunctionality()
    {
        if (this.HitTestPoint(Input.mouseX, Input.mouseY))
        {
            this.SetColor(1, 1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                DoSomething();
            }
        }
        else
        {
            this.SetColor(0.7f, 0.7f, 0.7f);
        }
    }
    protected virtual void DoSomething()
    {
    }
}
