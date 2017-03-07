using System;
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
        ScanForPlayers(this.position, this.sightRange);
    }

    public override void HandleInstruction(Instruction instruction)
    {
        base.HandleInstruction(instruction);
    }

    public override void HandleHit(Weapon weapon)
    {
        base.HandleHit(weapon);
    }

    private void ScanForPlayers(Vector2 origin, float range)
    {
        // TODO: Scan (sightRange) for any instances of Players.
    }

}
