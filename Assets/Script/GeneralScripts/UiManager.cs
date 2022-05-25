using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar,DashBar,manaBar;
    public  TextMeshProUGUI quest;
    float vidaActual,manaActual,DashCD,dashTimer;
    bool neckles;
    Scene scene;
    public GameObject Pausa, victoria;
    // Update is called once per frame
    private void Start()
    {
        neclesOFGlory.GotTheNeckles += changeText;
        scene = SceneManager.GetActiveScene();
    }
    void Update()
    {
        if (Pausa.activeSelf||victoria.activeSelf)
            Time.timeScale = 0;
        else Time.timeScale = 1;

        DashCD = PlayerMovement.dashCD;
        dashTimer = PlayerMovement.dashTimer;
        DashBar.fillAmount = dashTimer / DashCD;
        vidaActual = PlayerMovement.displayLife;
        healthBar.fillAmount = vidaActual / 100;
        manaActual = PlayerMovement.displayMana;
        manaBar.fillAmount = manaActual / 100;
        if(!neckles&&scene.name=="CaveMap")
            quest.text = "Encuentra el amuleto robado";
        if(scene.name == "ForestMap")
            quest.text="Explora el bosque y encuentra la guarida de los goblins";
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pausa.SetActive(true);
        }
    }
    void changeText(string text)
    {
        neckles = true;
        quest.text = text;
    }
    void deadMenu()
    {
        victoria.SetActive(true);
    }

}
