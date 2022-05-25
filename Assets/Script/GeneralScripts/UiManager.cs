using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar,DashBar,manaBar;
    public float vidaActual,manaActual,DashCD,dashTimer;
    // Update is called once per frame
    void Update()
    {

        DashCD = PlayerMovement.dashCD;
        dashTimer = PlayerMovement.dashTimer;
        DashBar.fillAmount = dashTimer / DashCD;
        vidaActual = PlayerMovement.displayLife;
        healthBar.fillAmount = vidaActual / 100;
        manaActual = PlayerMovement.displayMana;
        manaBar.fillAmount = manaActual / 100;
    }
}
