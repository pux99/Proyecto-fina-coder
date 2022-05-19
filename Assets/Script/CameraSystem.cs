using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject muro1;
    public GameObject muro2;
    public GameObject character;
    public Vector3 characterPosition;
    public Vector3 exteriorPosition;
    public float camara=1;
   
    void Update()
    {
        InputCamara();
        CambioPerspectiva();
    }

    public void InputCamara()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            camara = 1;
            CambioCamara();
            ApagarMuros();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camara = 2;
            CambioCamara();
            ApagarMuros();
        }
    }

    public void CambioCamara()
    {

        if (camara == 1)
        {           
            camera1.SetActive(true);
            camera2.SetActive(false);
        }

        if (camara == 2)
        {            
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
    }

    public void ApagarMuros()
    {
        
        if (camara == 1)
        {
            muro1.SetActive(true);
            muro2.SetActive(true);
        }

        if (camara == 2)
        {
            muro1.SetActive(false);
            muro2.SetActive(false);
        }
    }

    public void CambioPerspectiva()
    {
        characterPosition = character.transform.position;
        exteriorPosition = muro1.transform.position;
        Debug.Log("Posicion del personaje: "+ characterPosition + " Posicion del exterior: " + exteriorPosition);

        if (characterPosition.y < exteriorPosition.y )
        {
            camara = 2;
            CambioCamara();
            ApagarMuros();
        }

        if (character.transform.position.y >= muro1.transform.position.y)
        {
            camara = 1;
            CambioCamara();
            ApagarMuros();
            
        }

        
    }

    

    // Tareas:
    // 1. Alternar camaras
    // 2. Hacer que en la camara 2 los objetos (a) Muro 1, Muro 2, Muro 3 // b. "Muro Removible" desaparezcan
}
