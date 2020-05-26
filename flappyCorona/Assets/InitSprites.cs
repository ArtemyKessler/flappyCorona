using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSprites : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite Virus1;
    public Sprite Virus2;
    public Sprite Virus3;
    public Sprite Virus4;
    public Sprite Virus5;

    void Start()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            int rnd = Random.Range(0, 5);
            switch (rnd)
            {
                case 0:
                    renderer.sprite = Virus1;
                    break;
                case 1:
                    renderer.sprite = Virus2;
                    break;
                case 2:
                    renderer.sprite = Virus3;
                    break;
                case 3:
                    renderer.sprite = Virus4;
                    break;
                case 4:
                    renderer.sprite = Virus5;
                    break;
                default:
                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
