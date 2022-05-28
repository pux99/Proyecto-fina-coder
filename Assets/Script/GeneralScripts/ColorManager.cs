using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public static bool darken;
    public Image blackout;
    byte trasparence;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(darken)
        {
            if (trasparence >= 240)
                trasparence = 255;
            else
                trasparence += 10;
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, trasparence);
        }
        else
        {
            if (trasparence <= 20)
                trasparence = 0;
            else
                trasparence -= 10;
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, trasparence);
        }
    }
}
