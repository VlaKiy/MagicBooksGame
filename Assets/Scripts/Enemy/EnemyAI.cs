using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public float speed = 4f;
    public float seeDistance = 5f;
    public float magicAttackDistance = 5f;
    public float infightingAttackDistance = 2f;

    private GameObject player;
    private Vector3 startPos;
    private float maxHp = 100f;
    private float health = 1f;
    private bool isHitStarted = false;
    
    Rigidbody2D rb;

    [HideInInspector] public bool isAgressive = false;
    [HideInInspector] public bool isRunsForPlayer = false;
    [HideInInspector] public bool isBulletFly = false;
    [HideInInspector] public GameObject enemy;
    [HideInInspector] public bool enemyMouseDown = false;

    private void OnMouseDown()
    {
        enemyMouseDown = true;
        player.GetComponent<PlayerChangeEnemy>().enm = gameObject;
        player.GetComponent<PlayerUseSpell>().mouseDownOnEnemy = true;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        startPos = transform.position;
    }

    public void Update()
    {
        Stay();
    }

    private void Stay()
    {
        EnemyFollowPlayer();
    }

    public void EnemyFollowPlayer()
    {
        if (!isAgressive)
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= seeDistance)
            {
                rb.velocity = (player.transform.position - transform.position).normalized * speed;
                isRunsForPlayer = true;
                var typeOfAttack = GetComponent<EnemyInfo>().typeOfAttack;

                if (typeOfAttack == EnemyInfo.AttackType.Magic || typeOfAttack == EnemyInfo.AttackType.Ranged)
                {
                    Shoot();
                }

                if (typeOfAttack == EnemyInfo.AttackType.Infighting)
                {
                    if (!isHitStarted)
                    {
                        Hit();
                    }
                }
            }
            else
            {
                ReturnToStartPos();
                isRunsForPlayer = false;
            }
        }
        else if (isAgressive)
        {
            rb.velocity = (player.transform.position - transform.position).normalized * speed;
            isRunsForPlayer = true;
            var typeOfAttack = GetComponent<EnemyInfo>().typeOfAttack;

            if (typeOfAttack == EnemyInfo.AttackType.Magic || typeOfAttack == EnemyInfo.AttackType.Ranged)
            {
                Shoot();
            }

            if (typeOfAttack == EnemyInfo.AttackType.Infighting)
            {
                if (!isHitStarted)
                {
                    Hit();
                }
            }

            if (Vector2.Distance(player.transform.position, transform.position) <= seeDistance)
            {
                ReturnToStartPos();
                isAgressive = false;
                isRunsForPlayer = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        var healthBars = GameObject.FindGameObjectsWithTag("HealthBar");
        var circleChoice = GameObject.FindWithTag("CircleChoice");

        foreach (var healthBar in healthBars)
        {
            var bar = healthBar.transform.GetChild(0);

            if (healthBar.GetComponent<FollowGameobject>().lookAt == transform)
            {
                health -= (damage / maxHp);
                bar.GetComponent<Image>().fillAmount = health;
            }
        }

        if (health <= 0)
        {
            foreach (var healthBar in healthBars)
            {
                if (healthBar.GetComponent<FollowGameobject>().lookAt == transform)
                {
                    player.GetComponent<PlayerUseSpell>().mouseDownOnEnemy = false;
                    Destroy(healthBar);
                    Destroy(circleChoice);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void ReturnToStartPos()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > seeDistance)
        {
            rb.velocity = (startPos - transform.position).normalized * speed;
            if (Vector3.Distance(startPos, transform.position) <= 0.5f)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    private void Shoot()
    {
        if (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= magicAttackDistance)
            {
                var bulletPrefab = GetComponent<EnemyInfo>().bullet;
                var enemyPos = transform.position;
                var bullet = Instantiate(bulletPrefab, enemyPos, new Quaternion(0, 0, 0, 0));

                if (!isBulletFly)
                {
                    bullet.name = "EnemyBullet";

                    bullet.GetComponent<EnemyBulletTouch>().enemy = gameObject;
                    isBulletFly = true;
                    bullet.GetComponent<EnemyBulletTouch>().Update();
                }
                else
                {
                    Destroy(bullet);
                }
            }
        }
    }

    private void Hit()
    {
        if (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= infightingAttackDistance)
            {
                isHitStarted = true;
                StartCoroutine(Waiter());
            }
        }
    }

    private IEnumerator Waiter()
    {
        var waitTimeForAttack = GetComponent<EnemyInfo>().waitTimeForAttack;
        var damage = GetComponent<EnemyInfo>().damage;

        player.GetComponent<PlayerTakeDamage>().TakeDamage(damage);
        yield return new WaitForSeconds(waitTimeForAttack);
        isHitStarted = false;
    }

}