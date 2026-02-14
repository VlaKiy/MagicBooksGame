using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerTakeDamage : MonoBehaviour
{
    private float health = 1f;
    private float maxHp = 100f;
    private float regenHealth = 5f;
    private float timer = 6f;
    private bool isFullHealth = true;

    public void TakeDamage(float damage)
    {
        if (!Camera.main.GetComponent<CheatInput>().isUnlimitedHealth)
        {
            var healthBars = GameObject.FindGameObjectsWithTag("HealthBar");

            foreach (var healthBar in healthBars)
            {
                var bar = healthBar.transform.GetChild(0);

                if (healthBar.GetComponent<FollowGameobject>().lookAt == transform)
                {
                    health -= (damage / maxHp);
                    bar.GetComponent<Image>().fillAmount = health;
                    isFullHealth = false;
                }
            }

            
            StartCoroutine(RegenHealth());
            

            // Игрок умер
            if (health <= 0)
            {
                foreach (var healthBar in healthBars)
                {
                    if (healthBar.GetComponent<FollowGameobject>().lookAt == transform)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
            }
        }
    }

    private IEnumerator RegenHealth()
    {
        while (!isFullHealth)
        {
            yield return new WaitForSeconds(timer);
            if (health < 1f)
            {
                var healthBars = GameObject.FindGameObjectsWithTag("HealthBar");

                // Восстановление здоровья
                foreach (var healthBar in healthBars)
                {
                    var bar = healthBar.transform.GetChild(0);

                    if (healthBar.GetComponent<FollowGameobject>().lookAt == transform)
                    {
                        health += (regenHealth / maxHp);
                        bar.GetComponent<Image>().fillAmount = health;
                    }
                }
            }

            if (health > 1f)
            {
                health = 1f;
            }

            if (health == 1f)
            {
                isFullHealth = true;
            }
        }
    }
}
