using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPool : MonoBehaviour
{
    public GameObject maskPrefab;
    public int maskPoolSize = 5;
    public float spawnRate = 5f;
    public float maskMin = -1f;
    public float maskMax = 2f;
    public float maskMinX = -1f;
    public float maskMaxX = 1f;

    private GameObject[] masks;
    private Vector2 objectPoolPosition = new Vector2(-20f, -25f);
    private float timeSinceLastSpawn;
    private int currentMask = 0;

    void Start()
    {
        masks = new GameObject[maskPoolSize];
        for (int i = 0; i < maskPoolSize; i++)
        {
            masks[i] = (GameObject)Instantiate(maskPrefab, objectPoolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameControl.instance.isGameOver == false && GameControl.instance.isPaused == false)
            timeSinceLastSpawn += Time.deltaTime;
        if (GameControl.instance.isGameOver == false && GameControl.instance.isPaused == false && timeSinceLastSpawn >= spawnRate)
        {
            timeSinceLastSpawn = 0;
            float spawnY = Random.Range(maskMin, maskMax);
            float spawnX = Random.Range(maskMinX, maskMaxX);
            masks[currentMask].transform.position = new Vector2(10f, spawnY);
            currentMask++;
            if (currentMask >= maskPoolSize)
            {
                currentMask = 0;
            }
        }
    }
}
