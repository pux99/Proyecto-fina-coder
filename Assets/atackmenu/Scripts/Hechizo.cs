using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="nuevo hechizo",menuName ="hechizos")]
public class Hechizo : TemplateAtaque
{
    public string elemento,tipo;
    public float CD;
    
}
