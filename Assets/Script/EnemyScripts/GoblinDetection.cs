using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDetection : MonoBehaviour
{
    void OnTriggerStay(Collider Player)
    {
        if (Player == null)
            return;
        if (Player.CompareTag("Player")&& GetComponentInParent<GoblinScript>().type != GoblinScript.GoblinType.chaman)
            GetComponentInParent<GoblinScript>().Combat(Player.gameObject);
        if (GetComponentInParent<GoblinScript>().type == GoblinScript.GoblinType.chaman)
        {
            if (Player.CompareTag("Player"))
                GetComponentInParent<GoblinScript>().MagicAtack(Player.gameObject);
        }
    }
     void OnTriggerExit(Collider Player)
    {
        if (Player.CompareTag("Player"))
        {
            GetComponentInParent<GoblinScript>().battle_state = false;
            GetComponentInParent<GoblinScript>().ChangeAnimationState("moving", 0);
        }
    }
}
