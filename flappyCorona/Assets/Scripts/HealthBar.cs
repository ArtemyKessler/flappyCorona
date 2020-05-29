using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private HP hp;
    private Image barImage;
    // Start is called before the first frame update
    private void Awake()
    {
        barImage = GetComponent<Image>();
        barImage.fillAmount = 1.0f;
        hp = new HP();
    }

    private void Update()
    {
        if (GameControl.instance.isGameOver == false && GameControl.instance.isPaused == false)
        {
            hp.Update();
            float currHp = hp.GetHpNormalized();
            barImage.fillAmount = currHp;
            if (currHp <= 0f)
            {
                GameControl.instance.pauseGame();
            }
        }
    }

    public void Heal(float amount)
    {
        hp.Heal(amount);
    }

}

public class HP {
    public const int MAX_HP = 100;
    private float HPAmount;
    private float HPDegen;

    public HP() 
    {
        HPAmount = MAX_HP;
        HPDegen = 5f;
    }

    public void Update() {
        HPAmount -= HPDegen * Time.deltaTime;
        HPAmount = Mathf.Clamp(HPAmount, 0F, MAX_HP);
    }

    public void Heal(float amount) 
    {
        HPAmount += amount;
    }

    public float GetHpNormalized() 
    {
        return HPAmount / MAX_HP;
    }
}
