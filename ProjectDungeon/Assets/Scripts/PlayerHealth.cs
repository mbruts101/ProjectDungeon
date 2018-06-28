using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public Slider healthSlider;

    public int health;

	// Use this for initialization
	void Start ()
    {
        health = 100;
        healthSlider = GameObject.Find("PlayerHealthSlider").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health > 0)
        {
            healthSlider.value = health;
        }
        else
        {
            healthSlider.value = 0.0f;
            GetComponent<PlayerController>().isDead = true;
        }
	}
}
