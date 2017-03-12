using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Weapon {

    public Potion () {
        Random rando = new Random();
        foreach (StatKey key in this.stats.Keys)
        {
            this.stats[key] = Random.Range(-1, 2);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
