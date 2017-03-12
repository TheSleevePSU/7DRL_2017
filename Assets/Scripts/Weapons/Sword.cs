using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public Sword()
    {
        this.stats = new Assets.Scripts.Entities.Stats(-1, 1, 1);
    }
}
