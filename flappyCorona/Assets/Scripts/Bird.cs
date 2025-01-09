using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float upForce = 200f;

    private bool isDead = false;
    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false && GameControl.instance.isPaused == false) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Flap");
            } 
            //if (Input.touchCount > 0) 
            //{
            //    Touch touch = Input.GetTouch(0);
            //    if (touch.phase == TouchPhase.Stationary) 
            //    {
            //        rb.velocity = Vector2.zero;
            //        rb.AddForce(new Vector2(0, upForce * 40 * Time.deltaTime));
            //        anim.SetTrigger("Flap");
            //    }
            //}
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        int rnd = Random.Range(0, 10);
        if (rnd < 3)
        {
            transform.position = new Vector3(-1,0,0);
            GameControl.instance.pauseGame();
        } else {
            rb.linearVelocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameControl.instance.BirdDied();
            rb.gravityScale = 1;
        }
    }
}
