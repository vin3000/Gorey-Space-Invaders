using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrentWeapon : MonoBehaviour
{

    public Player player;


    public Image currentWeaponUI;

    public Sprite glock;
    public Sprite sniper;
    public Sprite tempWeapon;
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponUI.sprite = glock;

        //första ska vara glock 
    }

    void Update()
    {
        

        // imageUI.sprite = ; 


        //Ändra UI sprite beroende på vilket vapen man har 
    }
    public void UpdateWeaponUI(Weapon weapon)
    { 

        if (weapon.name == "glock")
        {
            currentWeaponUI.sprite = glock;  // Set Glock sprite 
        }
        else if (weapon.name == "Sniper")
        {
            currentWeaponUI.sprite = sniper;  // Set Sniper sprite
        }
        else
        {
            Debug.Log("???");
        }


        // Update is called once per frame
        
    }
}
