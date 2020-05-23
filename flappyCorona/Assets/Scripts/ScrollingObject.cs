using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rb;
    private float savedScrollSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        savedScrollSpeed = GameControl.instance.scrollSpeed;
        rb.velocity = new Vector2(savedScrollSpeed, 0);
    }

    void Update()
    {
        float newSpeed = GameControl.instance.scrollSpeed;
        if (newSpeed != savedScrollSpeed)
        {
            savedScrollSpeed = newSpeed;
            rb.velocity = new Vector2(savedScrollSpeed, 0);
        }
        if (GameControl.instance.isGameOver == true) 
        {
            rb.velocity = Vector2.zero;
        } 
    }
}
