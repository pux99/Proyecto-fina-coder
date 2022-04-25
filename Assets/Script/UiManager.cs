using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar,DashBar;
    public float vidaActual,DashCD,dashTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DashCD = PlayerMovement.dashCD;
        dashTimer = PlayerMovement.dashTimer;
        DashBar.fillAmount = dashTimer / DashCD;
        vidaActual = PlayerMovement.life;
        healthBar.fillAmount = vidaActual / 100;
    }
}
