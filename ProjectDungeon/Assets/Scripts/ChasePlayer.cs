using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour {
    GameObject Player;
    bool foundPlayer = false;
    public float detectionRadius;
    public Collider2D[] searchForPlayer;
    public int speed;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void Update() {
        if (!foundPlayer)
        {
            searchForPlayer = Physics2D.OverlapCircleAll(this.gameObject.transform.position, detectionRadius);
            foreach (Collider2D hit in searchForPlayer)
            {
                if (hit.gameObject.tag == "Player")
                {
                    StartCoroutine(Chase());
                    foundPlayer = true;
                }
            }
        }
    }
    IEnumerator Chase()
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            yield return null;
        }
        
    }
}
