﻿using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

public class Enemy : Entity {

    // Distance -- how far can I see?
    public int sightRange;

    public Sword sword;

    // Use this for initialization
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        // TODO: Scan (sightRange) for any instances of Players.
    }

    public override void HandleInstruction(Instruction instruction)
    {

    }

    public override void HandleHit(Weapon weapon)
    {
        base.HandleHit(weapon);
    }

}
