using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTouch : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private GameObject enemy;
    private GameObject player;
    private Vector2 pos;
    private float damage;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            /*player.GetComponent<PlayerUseSpell>().sound.src.Stop();*/
            Destroy(gameObject);
            enemy.GetComponent<EnemyAI>().TakeDamage(damage);
            enemy.GetComponent<EnemyAI>().Update();
            enemy.GetComponent<EnemyAI>().isAgressive = true;
        }
        else if (coll.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        bulletSpeed = player.GetComponent<PlayerUseSpell>().bulletSpeed;
        enemy = player.GetComponent<PlayerUseSpell>().enemyGameObject;
        damage = GetComponent<BulletInfo>().damage;
    }

    public void Update()
    {
        FollowEnemy();
    }

    private void FollowEnemy()
    {
        if (enemy != null)
        {
            pos.x = enemy.transform.position.x - transform.position.x;
            pos.y = enemy.transform.position.y - transform.position.y;

            pos.Normalize();
            rb.velocity = new Vector2(pos.x * bulletSpeed, pos.y * bulletSpeed);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
