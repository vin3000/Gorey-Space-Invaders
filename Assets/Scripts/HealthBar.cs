using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{


    public Slider slider; //skapar en slider variabel 

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health; 

    }

    public void SetHealth(int health)
    {
        slider.value = health; 
    }


}
