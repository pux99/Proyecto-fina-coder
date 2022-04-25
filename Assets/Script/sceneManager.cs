using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    
   
    public void SceneChange(int a)
    {
        SceneManager.LoadScene(a);
    }
    public void CloseDeGame() 
    {
        Application.Quit();
    }

}
