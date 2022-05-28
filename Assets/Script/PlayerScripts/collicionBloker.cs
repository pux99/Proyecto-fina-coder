using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collicionBloker : MonoBehaviour
{
    public CapsuleCollider characterCollider,charaterBlockerCollider;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(characterCollider, charaterBlockerCollider, true);
    }

}
