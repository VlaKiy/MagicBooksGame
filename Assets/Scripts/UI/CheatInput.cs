using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatInput : MonoBehaviour
{
    public GameObject input;
    public GameObject inputText;
    private GameObject player;
    private bool isActive = false;

    // Cheats
    [Header("Cheats")]
    public bool isTimerOn = true;
    public bool isUnlimitedHealth = false;
    public bool isSkipTalks = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            input.SetActive(true);
            isActive = true;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isActive)
            {
                CheatCheck();
                input.SetActive(false);
                inputText.GetComponent<Text>().text = "";
                isActive = false;
            }
        }
    }

    private void CheatCheck()
    {
        var cheat = inputText.GetComponent<Text>().text;

        // Выключение и включение таймера способности
        if (cheat == "disableSpellTimer")
        {
            isTimerOn = false;
            Debug.Log("Чит включен!");
        }
        if (cheat == "enableSpellTimer")
        {
            isTimerOn = true;
            Debug.Log("Чит выключен!");
        }

        // Бесконечные жизни
        if (cheat == "unlimitedHealth")
        {
            isUnlimitedHealth = true;
            Debug.Log("Чит включен!");
        }
        if (cheat == "limitedHealth")
        {
            isUnlimitedHealth = false;
            Debug.Log("Чит выключен!");
        }
    }
}
