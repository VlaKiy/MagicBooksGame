using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartHelper : MonoBehaviour
{
    [HideInInspector] public string text;
    [HideInInspector] public float timer;
    public GameObject helper;
    private Text helperText;

    private void Awake()
    {
        helperText = helper.GetComponentInChildren<Text>();
    }
    public void goHelper()
    {
        helperText.text = text;
        helper.SetActive(true);
        Invoke("NotShowHelper", timer);
    }

    private void NotShowHelper()
    {
        helper.SetActive(false);
    }
}
