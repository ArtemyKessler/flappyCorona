using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    private float direction;
    private float speed = 30f;
    private float timeBeforeSwitch = 1f;
    private float time = 0;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            Debug.Log("dir 1");
            direction = 1;
        }
        else
        {
            Debug.Log("dir -1");
            direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * direction * speed * Time.deltaTime);
        time += Time.deltaTime;
        if (time >= timeBeforeSwitch)
        {
            direction *= -1;
            time = 0;
        }
    }
}
