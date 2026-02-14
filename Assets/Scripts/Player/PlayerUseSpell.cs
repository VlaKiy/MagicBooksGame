using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Audio;

public class PlayerUseSpell : MonoBehaviour
{
    public GameObject contSpellAsk;
    public GameObject contSpellOvercrowding;
    public GameObject overcrowdingHelper;
    public int timer = 5;
    public float attackDistance = 5f;
    public float bulletSpeed = 7f;

    private GameObject btn;
    private GameObject[] enemies;

    [HideInInspector] public PlayerSoundController sound;
    [HideInInspector] public GameObject enemyGameObject;
    [HideInInspector] public bool mouseDownOnEnemy = false;

    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        sound = GetComponent<PlayerSoundController>();
    }

    public void UseSpell()
    {
        btn = EventSystem.current.currentSelectedGameObject;

        if (btn.name == "SpellHave" && btn != null)
        {
            Shot();
        }
    }

    private void SpellRegen()
    {
        var isTimerOn = Camera.main.GetComponent<CheatInput>().isTimerOn;

        if (isTimerOn)
        {
            StartCoroutine(waiter());
        }
    }

    private IEnumerator waiter()
    {
        var btnComp = btn.GetComponent<Button>();
        var textComp = btn.GetComponentInChildren<Text>();
        var rusNameSpell = btn.GetComponent<SpellBtnInfo>().rusNameSpell;

        for (int i = timer; i >= 1; i--)
        {
            btnComp.interactable = false;
            
            textComp.text = $"{i}";
            textComp.fontSize = 20;
            textComp.color = Color.white;
            yield return new WaitForSeconds(1);
        }

        btnComp.interactable = true;
        textComp.text = rusNameSpell;
        textComp.fontSize = 14;
        textComp.color = Color.black;
    }

    private void Shot()
    {
        var wasOneShot = false;

        if (!mouseDownOnEnemy)
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null && !wasOneShot)
                {
                    if (Vector2.Distance(transform.position, enemy.transform.position) <= attackDistance)
                    {
                        if (btn.GetComponent<SpellBtnInfo>().killAtTime == SpellInfo.KillAtTime.One)
                        {
                            enemyGameObject = enemy;
                            wasOneShot = true;

                            var bulletPrefab = btn.GetComponent<SpellBtnInfo>().bullet;
                            var playerPos = transform.position;
                            var bullet = Instantiate(bulletPrefab, playerPos, new Quaternion(0, 0, 0, 0));

                            bullet.name = "bullet";

                            gameObject.GetComponent<PlayerChangeEnemy>().enm = enemy;

                            bullet.GetComponent<BulletTouch>().Update();
                            sound.PlaySound(1);
                            SpellRegen();
                        }
                    }
                }
            }
        }
        else if (mouseDownOnEnemy)
        {
            var enemy = gameObject.GetComponent<PlayerChangeEnemy>().enm;

            if (enemy != null)
            {
                if (enemy.GetComponent<EnemyAI>().enemyMouseDown)
                {
                    if (!wasOneShot)
                    {
                        if (Vector2.Distance(transform.position, enemy.transform.position) <= attackDistance)
                        {
                            if (btn.GetComponent<SpellBtnInfo>().killAtTime == SpellInfo.KillAtTime.One)
                            {
                                enemyGameObject = enemy;
                                wasOneShot = true;

                                var bulletPrefab = btn.GetComponent<SpellBtnInfo>().bullet;
                                var playerPos = transform.position;
                                var bullet = Instantiate(bulletPrefab, playerPos, new Quaternion(0, 0, 0, 0));

                                bullet.name = "bullet";

                                bullet.GetComponent<BulletTouch>().Update();
                                sound.PlaySound(1);
                                SpellRegen();
                            }
                        }
                    }
                }
            }
        }
    }
}
