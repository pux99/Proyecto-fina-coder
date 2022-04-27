using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtacksDisplay : MonoBehaviour
{
    public Text stats;
    public Hechizo[]  spell=new Hechizo[5];
    public AtaquesFisicos[] atack = new AtaquesFisicos[5];
    public Image test;
    public Dropdown drop;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(drop.value)
        {
            case 0:
                stats.text = ("Nombre:" + spell[i].nombre + "\n\nTipo:" + spell[i].tipo + "\n\nElemento:" + spell[i].elemento + "\n\nDaño:" + spell[i].danio+"\n\nEnfriamiento:" + spell[i].CD);
                test.sprite = spell[i].icono;
                spell[i].mostrarme();
                break;
            case 1:
                stats.text = ("Nombre:" + atack[i].nombre + "\n\nTipo:" +  "\n\nDaño:" + atack[i].danio + "\n\nFuerza Regerida:" + atack[i].fuerzaRequerida);
                test.sprite = atack[i].icono;
                atack[i].mostrarme();
                break;
        }
        }

    public void next()
    {
        i++;
        if (i == 5)
            i = 0;
    }
    public void prev()
    {
        i--;
        if(i==-1)
        {
            i = 4;
        }
    }
}
