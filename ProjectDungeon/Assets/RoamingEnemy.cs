﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingEnemy : MonoBehaviour {


    public Transform startPos;
    public Transform endPos;
    public float speed;
    public float waitTime;
    Transform lastPos;

	// Use this for initialization
	void Start ()
    {
        lastPos = startPos;	
	}

	void Update ()
    {
        float step = speed * Time.deltaTime;

	    if(lastPos.position == startPos.position)
        {
            if (transform.position != endPos.position)
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, endPos.position, step);
                transform.position = newPos;
            }
            else
            {
                StartCoroutine(WaitToMove());
                lastPos = endPos;
            }
        }
        else if(lastPos.position == endPos.position)
        {
            if (transform.position != startPos.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, startPos.position, step);
            }
            else
            {
                StartCoroutine(WaitToMove());
                lastPos = startPos;
            }
        }
	}

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
