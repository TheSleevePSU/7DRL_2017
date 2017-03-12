using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public Stats stats = new Stats(-1, -1, -1);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Attack(Vector2 destination)
    {
        KillAtLocation(destination);
    }

    public Vector2 position
    {
        get
        {
            return this.transform.position;
        }
    }

    public static bool CanWeaponReach(Vector2 destination, Vector2 position, Weapon weapon)
    {
        return Vector2.Distance(position, destination) > weapon.stats.GetSightRange();
    }

    protected void KillAtLocation(Vector2 destination)
    {
        if (!CanWeaponReach(destination, position, this))
        {
            return;
        }

        // This means Enemies can be hit with friendly fire.
        IEntity victim = Entity.GetEntityAt(destination);
        if (victim != null)
        {
            victim.HandleHit(this);
        }
    }
}
