using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPicking : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bird>() != null)
        {
            GameControl.instance.BirdPickMask();
            transform.position = new Vector2(-20f, -25f);
        }
    }
}
