using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Bar
{
    healthBar,
    manaBar
}

public class BarType : MonoBehaviour
{
    [SerializeField] Bar type;

    [SerializeField] Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        switch (type)
        {
            case Bar.healthBar:
                slider.maxValue = PlayerController.MAX_HEALTH;
                break;
            case Bar.manaBar:
                slider.maxValue = PlayerController.MAX_MANA;
                break;
        }
    }

    private void Update()
    {
        switch (type)
        {
            case Bar.healthBar:
                slider.value = FindObjectOfType<PlayerController>().GetHealth();
                break;
            case Bar.manaBar:
                slider.value = FindObjectOfType<PlayerController>().GetMana();
                break;
        }
    }
}
