﻿using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;
using Assets.Scripts.Weapons;

public abstract class Entity : MonoBehaviour, IEntity, ITurnTaker {
    
    public Vector2 position
    {
        get
        {
            return this.transform.position;
        }
    }
    
    public int attackRange, health;
    public Weapon weapon;
    public bool isTurnFinished = false;

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    // This is where input is received
    public virtual void HandleInstruction(Instruction instruction)
    {
        this.transform.position = GameManager.GetTransformForInstruction(instruction, transform.position);
    }

    // Process being hit (by something)
    public virtual void HandleHit(Weapon weapon)
    {
        this.health -= weapon.damage;
    }

    public void Attack(Weapon weapon, Vector2 destination)
    {
        weapon.Attack(destination);
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

    public bool IsTurnFinished()
    {
        return isTurnFinished;
    }
}
