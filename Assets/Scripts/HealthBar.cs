using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    public Slider slider; //Refererar till UI slidern  

    
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

    }

    public void SetHealth(float health)
    {
        slider.value = health;


    }


}
