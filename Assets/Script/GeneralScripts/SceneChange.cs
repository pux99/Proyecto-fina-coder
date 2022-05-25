using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int scene;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        SceneManager.LoadScene(scene);
    }
    public void changescene(int number)
    {
        SceneManager.LoadScene(number);
    }
}
