﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class Projectile : Weapon
    {
        private int speed = 2;

        private bool inTransit = false;

        private Vector2 destination;

        public Projectile()
        {
            this.damage = 1;
            this.speed = 2;
        }

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            this.inTransit = this.position != this.destination;
            if (this.inTransit)
            {
                HandleMovement(destination);
            }
        }
        
        public void OnTriggerEnter2D(Collider2D collider)
        {
            KillAtLocation(collider.transform.position);
        }

        public void Attack(Vector2 destination)
        {
            this.destination = destination;
            this.inTransit = true;
        }

        public void HandleMovement(Vector2 destination)
        {
            Vector2 updatedLocation = Vector2.MoveTowards(transform.position, destination, this.speed * Time.deltaTime);
            transform.position = updatedLocation;
        }

        private void KillAtLocation(Vector2 destination)
        {
            // This means Enemies can be hit with friendly fire.
            IEntity victim = Entity.GetEntityAt(destination);
            if (victim != null)
            {
                victim.HandleHit(this);
                this.inTransit = false;
            }
        }
    }
}
