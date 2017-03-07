using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IEntity {

    public Vector2 position
    {
        get
        {
            return this.transform.position;
        }
    }

    public int attackRange, health;

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    // This is where input is received
    public abstract void HandleInstruction(Instruction instruction);

    // Process being hit (by something)
    public virtual void HandleHit(Weapon weapon)
    {
        this.health -= weapon.damage;
    }

    public void Attack(Weapon weapon, Vector2 destination)
    {
        if (!CanWeaponReach(destination, position, weapon))
        {
            return;
        }

        IEntity entity = GetEntityAt(destination);
        if (entity != null)
        {
            entity.HandleHit(weapon);
        }
    }

    public static bool CanWeaponReach(Vector2 destination, Vector2 position, Weapon weapon)
    {
        return Vector2.Distance(position, destination) > weapon.range;
    }

    public static IEntity GetEntityAt(Vector2 destination)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(destination);
        foreach (Collider2D collider in colliders)
        {
            IEntity entity = collider.gameObject.GetComponent<IEntity>();
            return entity;
        }
        return null;
    }

}
