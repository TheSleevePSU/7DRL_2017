using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class Projectile : Weapon
    {
        private bool inTransit = false;
        private Vector2 trajectory;

        public void HandleMovement(Vector2 destination)
        {

        }
    }
}
