using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IEntity {

    public Vector3 position;

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
        float dx = (this.position.x > destination.x) ? this.position.x - destination.x : destination.x - this.position.x;
        float dy = (this.position.y > destination.y) ? this.position.y - destination.y : destination.y - this.position.y;
        // TODO: Write better tangent math
        if ((dx + dy) > weapon.range)
        {
            return;
        }

        // TODO: Find and interact with any other Entities at `destination`.
        // If there is an Entity @ desitnation -- call entity.HandleHit(weapon)
    }
}
