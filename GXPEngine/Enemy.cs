﻿using GXPEngine;
using TiledMapParser;
class Enemy : AnimationSprite
{
    private int enemyMaxHealth;
    private int enemyHealth;
    private float scaleModifier = 1f;
    private float distanceToDetectPlayer;
    private float distanceToStopFromFollowingPlayer;
    private float distanceToAttackPlayer;
    private float enemySpeed;
    private float timeUntilResetPosition;
    private float timeBetweenAttacks;
    private float timeItAttacked;
    private float timeWhenItFollows;
    private int enemyDamage;
    private bool enemyIsMoving = false;
    private bool enemyIsAttacking = false;
    private float timeOfDeath = -1f;

    private float initialX;
    private float initialY;
    private Player oPlayer;
    private HUD enemyHUD = null;

    public Enemy(TiledObject obj = null) : base("MinotaurSpriteSheet.png", 9, 10)
    {
        if (obj != null)
        {
            enemyMaxHealth = obj.GetIntProperty("enemyMaxHealth", 100);
            enemyDamage = obj.GetIntProperty("enemyDamage", 15);
            distanceToDetectPlayer = obj.GetFloatProperty("distanceToDetectPlayer", 150f);
            distanceToStopFromFollowingPlayer = obj.GetFloatProperty("distanceToStopFromFollowingPlayer", 60f);
            distanceToAttackPlayer = obj.GetFloatProperty("distanceToAttackPlayer", 50f);
            enemySpeed = obj.GetFloatProperty("enemySpeed", 1f);
            timeUntilResetPosition = obj.GetFloatProperty("timeUntilResetPosition", 1000f);
            timeBetweenAttacks = obj.GetFloatProperty("timeBetweenAttacks", 1000f);
        }
        timeWhenItFollows = -timeUntilResetPosition;
        initialX = obj.X;
        initialY = obj.Y;
        enemyHealth = enemyMaxHealth;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(scaleModifier);
        timeItAttacked = -timeBetweenAttacks;
    }
    void Update()
    {
        if (oPlayer == null)
        {
            oPlayer = parent.FindObjectOfType<Player>();
        }
        if (enemyHUD == null) enemyHUD = game.FindObjectOfType<HUD>();
        if (timeOfDeath == -1f)
        {
            FollowPlayer();
            AttackPlayer();
        }
        EnemyAnimations();
    }
    private bool WillFollowPlayer() // Function to detect if the player is in the follow range
    {
        if (DistanceTo(game.FindObjectOfType(typeof(Player))) <= distanceToDetectPlayer)
            return true;
        return false;
    }

    private void EnemyAnimations()
    {
        if (timeOfDeath == -1f)
        {
            if (enemyIsAttacking)
            {
                SetCycle(27, 7);
            }
            if (enemyIsMoving && !enemyIsAttacking)
            {
                SetCycle(9, 8);
            }
            else if (!enemyIsMoving && !enemyIsAttacking)
                SetCycle(0, 5);
        }
        else
        {
            SetCycle(81, 6);
            if (Time.time >= timeOfDeath + 900f)
                LateDestroy();
        }
        Animate(0.1f);
    }

    void FollowPlayer() // Function that makes the AI follow the player, as well as flip it accordingly using Mirror and Move.
    {
        if (WillFollowPlayer())
        {
            if (oPlayer.x < x)
                Mirror(true, _mirrorY);
            else
                Mirror(false, _mirrorY);
            if (DistanceTo(game.FindObjectOfType(typeof(Player))) >= distanceToStopFromFollowingPlayer)
            {
                Move(Mathf.Sign(oPlayer.x - x) * enemySpeed, Mathf.Sign(oPlayer.y - y) * enemySpeed);
                enemyIsMoving = true;
            }
            enemyIsMoving = false;
            timeWhenItFollows = Time.time;
        }
        else
        {
            if (Time.time >= timeUntilResetPosition + timeWhenItFollows)
                ResetEnemyPosition();
        }
    }

    void AttackPlayer()
    {
        if (DistanceTo(game.FindObjectOfType(typeof(Player))) <= distanceToAttackPlayer)
        {
            if (Time.time >= timeBetweenAttacks + timeItAttacked && !enemyIsAttacking)
            {
                Sound enemySwing = new Sound("EnemyAxeSwing.ogg");
                enemySwing.Play();
                enemyIsAttacking = true;
                timeItAttacked = Time.time;
                oPlayer.TakeDamage(enemyDamage);
            }
        }
        if (Time.time >= timeItAttacked + 1000f && enemyIsAttacking)
        {
            enemyIsAttacking = false;
        }

    }

    public void DamageEnemy(int damage)
    {
        enemyHealth -= damage; // The enemy will be able to die only when in combat with you (unless you one-shot). This is due to ResetEnemyPosition() being called whenever the player is not in range to be followed. This prevents the player from killing the enemy without being chased(again, unless he one shots it).
        if (enemyHealth <= 0)
        {
            Sound enemyDeath = new Sound("EnemyDeathSound.ogg");
            enemyDeath.Play();
            ((MyGame)game).playerData.score += 100;
            timeOfDeath = Time.time;
        }
    }

    void ResetEnemyPosition()
    {
        bool hasResetPosition = true;
        if (x > initialX)
            Mirror(true, _mirrorY);
        else
            Mirror(false, _mirrorY);
        if (Mathf.Abs(x - initialX) > 0.5f)
        {
            Move(Mathf.Sign(x - initialX) * (-enemySpeed), 0);
            hasResetPosition = false;
        }
        if (Mathf.Abs(y - initialY) > 0.5f)
        {
            Move(0, Mathf.Sign(y - initialY) * (-enemySpeed));
            hasResetPosition = false;
        }
        if (hasResetPosition)
            Mirror(false, _mirrorY);
        enemyHealth = enemyMaxHealth;
    }
}
