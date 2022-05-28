using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instans;
    public float life, mana;
    private void Awake()
    {
        if (CharacterManager.instans == null)
        {
            CharacterManager.instans = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetValues()
    {
        life = 100;
        mana = 100;
    }
}
