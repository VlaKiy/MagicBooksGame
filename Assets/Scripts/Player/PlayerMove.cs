using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Joystick joystick;
    public Animator animator;
    public AudioSource src;

    private Vector2 moveVelocity;
    private PlayerSoundController sound;

    private void Awake()
    {
        sound = GetComponent<PlayerSoundController>();
        src = GetComponent<AudioSource>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = moveInput.normalized * speed;
        animator.SetFloat("speedUp", joystick.Vertical);
        animator.SetFloat("speedDown", joystick.Vertical);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            src.Play();
        }
        else
        {
            src.Stop();
        }
    }
}
