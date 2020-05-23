using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashHands : MonoBehaviour
{
    // Start is called before the first frame update
    public void Wash()
    {
        Debug.Log("WASH");
        gameObject.SetActive(false);
        GameControl.instance.washHands();
    }
}
