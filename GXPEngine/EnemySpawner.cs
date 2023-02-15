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
    int enemyMaxHealth;
    int enemyDamage;
    float distanceToStopFromFollowingPlayer;
    float distanceToAttackPlayer;
    float enemySpeed;
    float animationTime;
    int coinsAwarded;
    float timeBetweenAttacks;
    float timeUntilSpawnSpeedIncreases;
    float spawnTimeDecreaseValue;
    float increasedSpeedTime;
    public EnemySpawner(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows)
    {
        if (obj != null)
        {
            enemyMaxHealth = obj.GetIntProperty("enemyMaxHealth", 100);
            enemyDamage = obj.GetIntProperty("enemyDamage", 15);
            distanceToStopFromFollowingPlayer = obj.GetFloatProperty("stopDistance", 60f);
            distanceToAttackPlayer = obj.GetFloatProperty("attackDistance", 61f);
            enemySpeed = obj.GetFloatProperty("enemySpeed", 1f);
            animationTime = obj.GetFloatProperty("animationTime", 1000f);
            coinsAwarded = obj.GetIntProperty("coinsAwarded", 10);
            timeBetweenAttacks = obj.GetFloatProperty("timeBetweenAttacks", 1000f);
            spawnEnemyTimeInterval = obj.GetFloatProperty("spawnEnemyTimeInterval", 1000f);
            timeUntilSpawnSpeedIncreases = obj.GetFloatProperty("timeUntilSpawnSpeedIncreases", 1000f);
            spawnTimeDecreaseValue = obj.GetFloatProperty("spawnTimeDecreaseValue", 1000f);
        }
        timeItSpawned = -spawnEnemyTimeInterval;
    }

    void Update()
    {
        SpawnChecker();
    }

    void SpawnChecker()
    {
        if (Time.time >= timeUntilSpawnSpeedIncreases + increasedSpeedTime)
        {
            spawnEnemyTimeInterval -= spawnTimeDecreaseValue;
            increasedSpeedTime = Time.time;
        }
        if (Time.time >= spawnEnemyTimeInterval + timeItSpawned)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Enemy enemy = new Enemy(enemyMaxHealth,enemyDamage,distanceToStopFromFollowingPlayer,distanceToAttackPlayer,enemySpeed,animationTime,coinsAwarded,timeBetweenAttacks);
        enemy.SetXY(x, y);
        parent.AddChild(enemy);
        Console.WriteLine("Enemy Spawned. Interval = {0}",spawnEnemyTimeInterval);
        timeItSpawned = Time.time;
    }

}
