using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public Slider slider; // Reference to the UI s

    public void SetMaxAmmo(float ammo)
    {
        slider.maxValue = ammo;
        slider.value = ammo; 
    }

    // Updaterar ammobaren med hur mycket ammo man har kvar  
    public void SetAmmo(float ammo)
    {
        slider.value = ammo; 
    }
}
