using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public int attackDamage;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealth>().health -= attackDamage;
            col.gameObject.GetComponent<PlayerController>().isHit = true;
            col.gameObject.GetComponent<PlayerController>().hitTimer = 10;
        }
    }
}
