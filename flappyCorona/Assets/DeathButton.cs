using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathButton : MonoBehaviour
{
    // Start is called before the first frame update
    Button myButton;

    void Start()
    {
        myButton = GetComponent<Button>();
        if (myButton) myButton.onClick.AddListener(Skip);
    }

    // Update is called once per frame
    void Skip()
    {
        GameControl.instance.BirdDied();
    }
}
