using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharater : MonoBehaviour
{
    public float axiX;
	protected void Turning(float angle)
	{
        int direction;
		
        if (angle == 90)
            direction = -1;
        else direction = 1;
		float rotacion = gameObject.transform.eulerAngles.y;
		if (angle ==rotacion)
			return;
		if (rotacion > 90 && rotacion <= 120)
		{
			gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
		}
		if (rotacion < 270 && rotacion >= 250)
		{
			gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
		}
		if ((rotacion < 90 && rotacion > -1) || (rotacion < 360 && rotacion > 270) || (rotacion == 90 && angle >= 90) || (rotacion == 270 && angle <= 270))
			gameObject.transform.Rotate(0, 500 * -direction * Time.deltaTime, 0);
		
	}
}
