﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
	public Text speed, collectibles;
	public CarMovement cm;
    // Start is called before the first frame update
    void Start()
    {
		cm = this.gameObject.GetComponent<CarMovement> ();
    }

    // Update is called once per frame
    void Update()
    {
		speed.text = "Speed: " + Mathf.Round(cm.carVelocity); 
    }
}
