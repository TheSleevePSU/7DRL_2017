using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IEntity {

    public int attackRange, health;

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    // This is where input is received
    public abstract void HandleInstruction(Instruction instruction);

    // Process being hit (by something)
    public void HandleHit(Weapon weapon)
    {
        this.health -= weapon.damage;
    }

    public void Attack(Weapon weapon, Vector3 destination)
    {

    }
}
