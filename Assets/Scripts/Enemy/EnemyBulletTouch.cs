using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTouch : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private GameObject player;
    private Vector2 pos;
    private float damage;

    [HideInInspector] public GameObject enemy;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.GetComponent<PlayerTakeDamage>().TakeDamage(damage);
            enemy.GetComponent<EnemyAI>().isBulletFly = false;
        }
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        if (enemy)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        damage = enemy.GetComponent<EnemyInfo>().damage;
        bulletSpeed = enemy.GetComponent<EnemyInfo>().bulletSpeed;

        if (player != null)
        {
            pos.x = player.transform.position.x - transform.position.x;
            pos.y = player.transform.position.y - transform.position.y;

            pos.Normalize();
            rb.velocity = new Vector2(pos.x * bulletSpeed, pos.y * bulletSpeed);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
