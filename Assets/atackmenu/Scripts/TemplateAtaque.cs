using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateAtaque : ScriptableObject
{
    public string nombre;
    public float danio;
    public int objetivos;
    public Sprite icono;
    public void  mostrarme()
    {
        Debug.Log("me selecionaron eeeee me llamo:"+nombre);
    }
}


