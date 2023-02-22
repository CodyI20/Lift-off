using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    private int enemySpawnedID = 0;
    Random rand = new Random();
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
        if (!((MyGame)game).gameIsPaused)
            SpawnChecker();
    }

    void SpawnChecker()
    {
        if (Time.time >= timeUntilSpawnSpeedIncreases + increasedSpeedTime && spawnEnemyTimeInterval - spawnTimeDecreaseValue>=1000)
        {
            spawnEnemyTimeInterval -= spawnTimeDecreaseValue;
            increasedSpeedTime = Time.time;
        }
        if (Time.time >= spawnEnemyTimeInterval + timeItSpawned)
        {
            enemySpawnedID = rand.Next(0, 901);
            Console.WriteLine("Spawn enemy with ID: {0}", enemySpawnedID);
            SpawnEnemy(enemySpawnedID);
        }
    }

    void SpawnEnemy(int i)
    {
        string filename = null;
        int cols = 1;
        int rows = 1;
        int enemyMaxHealthSave = enemyMaxHealth;
        int enemyDamageSave = enemyDamage;
        float enemySpeedSave = enemySpeed;
        if (i >= 0 && i<=299)
        {
            filename = "Enemy.png"; //DOG
            cols = 5;
            rows = 5;
        }
        if (i >= 300 && i<=599)
        {
            filename = "Enemy.png"; //MANTIS
            cols = 5;
            rows = 5;
            enemyDamageSave *= 2;
            enemyMaxHealthSave /= 2;
            enemySpeedSave *= 1.3f;
        }
        if(i>= 600 && i<=900)
        {
            filename = "Enemy.png"; //ZOMBIE
            cols = 5;
            rows = 5;
            enemyDamageSave *= 5;
            enemyMaxHealthSave *= 4;
            enemySpeedSave /= 1.5f;
        }
        Enemy enemy = new Enemy(filename, cols, rows,enemyMaxHealthSave,enemyDamageSave,distanceToStopFromFollowingPlayer,distanceToAttackPlayer,enemySpeedSave,animationTime,coinsAwarded,timeBetweenAttacks);
        enemy.SetXY(x, y);
        parent.AddChild(enemy);
        Console.WriteLine("Enemy Spawned. Interval = {0}",spawnEnemyTimeInterval);
        timeItSpawned = Time.time;
    }

}
