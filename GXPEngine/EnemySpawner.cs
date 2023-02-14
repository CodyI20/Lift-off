using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class EnemySpawner : AnimationSprite
{
    private float spawnEnemyTimeInterval;
    private float timeItSpawned;
    public EnemySpawner(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    {
        if (obj != null)
        {
            spawnEnemyTimeInterval = obj.GetFloatProperty("spawnInterval", 1000f);
            timeItSpawned = -spawnEnemyTimeInterval;
        }
    }

    void Update()
    {
        if(Time.time>=spawnEnemyTimeInterval + timeItSpawned)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Enemy enemy = new Enemy();
        enemy.SetXY(x, y);
        parent.AddChild(enemy);
        timeItSpawned = Time.time;
    }

}
