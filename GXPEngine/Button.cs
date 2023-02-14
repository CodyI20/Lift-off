using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class Button : AnimationSprite
{
    //Sprite visualButton;
    string loadFilename;

    public Button(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    {
        //this.visualButton = visualButton;
        loadFilename = obj.GetStringProperty("load", "RPGMap");
    }

    void Update()
    {
        if (this.HitTestPoint(Input.mouseX, Input.mouseY))
        {
            this.SetColor(1, 1, 1);
            if(Input.GetMouseButtonDown(0))
            {
                ((MyGame)game).LoadLevel(loadFilename + ".tmx");
            }
        }
        else
        {
            this.SetColor(0.7f, 0.7f, 0.7f);
        }
    }
}
