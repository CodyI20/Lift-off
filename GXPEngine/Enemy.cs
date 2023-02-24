using GXPEngine;
class Enemy : AnimationSprite
{
    private int enemyMaxHealth;
    private int enemyHealth;
    private float scaleModifier = 1f;
    //private float distanceToDetectPlayer;
    private float distanceToStopFromFollowingPlayer;
    private float distanceToAttackPlayer;
    private float animationTime;
    private int coinsAwarded;
    private float enemySpeed;
    private float timeBetweenAttacks;
    private float timeItAttacked;
    private int enemyDamage;
    private bool enemyIsMoving = false;
    private bool enemyIsAttacking = false;
    private float timeEnemyGotHit;
    private float timeOfDeath = -1f;
    private bool hasFoundPlayer = false;
    public bool enemyIsDying = false;
    private string filename;

    private Player oPlayer;
    private HUD enemyHUD = null;

    public Enemy(string filename, int cols, int rows, int enemyMaxHealth, int enemyDamage, float distanceToStopFromFollowingPlayer, float distanceToAttackPlayer, float enemySpeed, float animationTime, int coinsAwarded, float timeBetweenAttacks) : base(filename, cols, rows)
    {
        this.enemyMaxHealth = enemyMaxHealth;
        this.enemyDamage = enemyDamage;
        this.distanceToStopFromFollowingPlayer = distanceToStopFromFollowingPlayer;
        this.distanceToAttackPlayer = distanceToAttackPlayer;
        this.enemySpeed = enemySpeed;
        this.animationTime = animationTime;
        this.coinsAwarded = coinsAwarded;
        this.timeBetweenAttacks = timeBetweenAttacks;
        this.filename = filename;
        //timeWhenItFollows = -timeUntilResetPosition;
        enemyHealth = enemyMaxHealth;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(scaleModifier);
        timeItAttacked = -timeBetweenAttacks;
    }
    void Update()
    {
        if (!((MyGame)game).gameIsPaused)
        {
            CheckEnemyHit();
            FindPlayer();
            FindHUD();
            AliveActions();
            EnemyAnimations();
        }
    }

    protected void CheckEnemyHit()
    {
        if (Time.time >= 300f + timeEnemyGotHit)
        {
            SetColor(1f, 1f, 1f);
        }
    }

    protected void FindPlayer()
    {
        if (!hasFoundPlayer && oPlayer == null)
        {
            oPlayer = parent.FindObjectOfType<Player>();
        }
        else if (oPlayer != null)
            hasFoundPlayer = true;
    }

    protected void FindHUD()
    {
        if (enemyHUD == null) enemyHUD = game.FindObjectOfType<HUD>();
    }

    protected void AliveActions()
    {
        if (timeOfDeath == -1f)
        {
            FollowPlayer();
            AttackPlayer();
        }
    }
    //private bool WillFollowPlayer() // Function to detect if the player is in the follow range
    //{
    //    if (DistanceTo(game.FindObjectOfType(typeof(Player))) <= distanceToDetectPlayer)
    //        return true;
    //    return false;
    //}

    private void EnemyAnimations()
    {
        if (timeOfDeath == -1f)
        {
            if (filename == "Enemy.png")
            {
                if (enemyIsAttacking)
                {
                    SetCycle(8, 12);
                }
                if (enemyIsMoving && !enemyIsAttacking)
                {
                    SetCycle(0, 8);
                }
                else if (!enemyIsMoving && !enemyIsAttacking)
                    SetCycle(8, 2);
            }
            if (filename == "Zombie.png")
            {
                if (enemyIsAttacking)
                {
                    SetCycle(11, 9);
                }
                if (enemyIsMoving && !enemyIsAttacking)
                {
                    SetCycle(0, 10);
                }
                else if (!enemyIsMoving && !enemyIsAttacking)
                {
                    SetCycle(6, 3);
                }
            }
            if (filename == "Mantis.png")
            {
                if (enemyIsAttacking)
                    SetCycle(8, 9);
                if (enemyIsMoving && !enemyIsAttacking)
                    SetCycle(0, 8);
                else if (!enemyIsMoving && !enemyIsAttacking)
                    SetCycle(0, 3);
            }

        }
        else
        {
            if (filename == "Enemy.png")
            {
                SetCycle(20, 4);
                if (Time.time >= timeOfDeath + 600f)
                    LateDestroy();
            }
            if (filename == "Zombie.png")
            {
                SetCycle(21, 8);
                if (Time.time >= timeOfDeath + 1300f)
                    LateDestroy();
            }
            if (filename == "Mantis.png")
            {
                SetCycle(18, 7);
                if (Time.time >= timeOfDeath + 1100f)
                    LateDestroy();
            }
        }
        Animate(0.1f);
    }

    protected void FollowPlayer() // Function that makes the AI follow the player, as well as flip it accordingly using Mirror and Move.
    {
        float deltaSpeed = enemySpeed * Time.deltaTime;
        if (oPlayer.x - x < 20f)
            Mirror(true, _mirrorY);
        else
            Mirror(false, _mirrorY);
        enemyIsMoving = false;
        if (DistanceTo(game.FindObjectOfType(typeof(Player))) >= distanceToStopFromFollowingPlayer)
        {
            if (Mathf.Abs(oPlayer.y - y) > 65f && Mathf.Abs(oPlayer.y - y) < 300f)
                Move(Mathf.Sign(oPlayer.x - x) * deltaSpeed/1.6f, Mathf.Sign(oPlayer.y - y) * deltaSpeed / 2.3f);
            else
                Move(Mathf.Sign(oPlayer.x - x) * deltaSpeed, Mathf.Sign(oPlayer.y - y) * deltaSpeed / 2.3f);
            enemyIsMoving = true;
        }
    }

    protected void AttackPlayer()
    {
        if (DistanceTo(game.FindObjectOfType(typeof(Player))) <= distanceToAttackPlayer)
        {
            if (Time.time >= timeBetweenAttacks + timeItAttacked && !enemyIsAttacking)
            {
                Sound enemySwing = new Sound("Enemy attack.wav");
                enemySwing.Play();
                enemyIsAttacking = true;
                timeItAttacked = Time.time;
                oPlayer.TakeDamage(enemyDamage);
            }
        }
        if (Time.time >= timeItAttacked + animationTime && enemyIsAttacking)
        {
            enemyIsAttacking = false;
        }

    }

    public void DamageEnemy(int damage)
    {
        Sound enemyHit = new Sound("Enemy hit.wav");
        enemyHit.Play();
        timeEnemyGotHit = Time.time;
        SetColor(0.3f, 0f, 0f);
        enemyHealth -= damage; // The enemy will be able to die only when in combat with you (unless you one-shot). This is due to ResetEnemyPosition() being called whenever the player is not in range to be followed. This prevents the player from killing the enemy without being chased(again, unless he one shots it).
        if (enemyHealth <= 0)
        {
            Sound enemyDeath = new Sound("Enemy dead.wav");
            enemyDeath.Play();
            CoinPickUp coin = new CoinPickUp("Coin.png", 1, 1, coinsAwarded, oPlayer);
            coin.SetXY(x, y);
            parent.AddChild(coin);
            timeOfDeath = Time.time;
            enemyIsDying = true;
        }
    }

    //void ResetEnemyPosition()
    //{
    //    bool hasResetPosition = true;
    //    if (x > initialX)
    //        Mirror(true, _mirrorY);
    //    else
    //        Mirror(false, _mirrorY);
    //    if (Mathf.Abs(x - initialX) > 0.5f)
    //    {
    //        Move(Mathf.Sign(x - initialX) * (-enemySpeed), 0);
    //        hasResetPosition = false;
    //    }
    //    if (Mathf.Abs(y - initialY) > 0.5f)
    //    {
    //        Move(0, Mathf.Sign(y - initialY) * (-enemySpeed));
    //        hasResetPosition = false;
    //    }
    //    if (hasResetPosition)
    //        Mirror(false, _mirrorY);
    //    enemyHealth = enemyMaxHealth;
    //}
}
