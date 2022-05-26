using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RcokSpawner : MonoBehaviour
{
    public Transform PlayerPosition;
    public GameObject Piedra0, Piedra1, Piedra2, Piedra3, Piedra4, Piedra5;
    float timer;
    bool lado;
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            if(lado)
            {
                transform.position = PlayerPosition.position + new Vector3(Random.Range(-7f, 7f), 7, Random.Range(-2.5f, -.5f));
            }else
                transform.position = PlayerPosition.position + new Vector3(Random.Range(-7f, 7f), 7, Random.Range(.5f, 2.5f));
            switch (Random.Range(0,6))
            {
                case 0:SpawnRock(Piedra0);
                    break;
                case 1:
                    SpawnRock(Piedra1);
                    break;
                case 2:
                    SpawnRock(Piedra2);
                    break;
                case 3:
                    SpawnRock(Piedra3);
                    break;
                case 4:
                    SpawnRock(Piedra4);
                    break;
                case 5:
                    SpawnRock(Piedra5);
                    break;
            }
            timer = .5f;
        }
    }
    void SpawnRock(GameObject type)
    {
        Instantiate(type, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        lado = !lado;
    }
}
