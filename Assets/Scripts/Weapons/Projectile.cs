using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class Projectile : Weapon, ITurnTaker
    {
        private int speed = 2;

        private bool inTransit = false;
        private bool isTurnFinished = false;
        private Vector2 trajectory;

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
            if (!IsMyTurn())
            {
                return;
            }

            this.inTransit = this.position != this.destination;
            if (this.inTransit)
            {
                HandleMovement(destination);
            }
            else
            {
                this.isTurnFinished = true;
            }
        }
        
        public void OnTriggerEnter2D(Collider2D collider)
        {
            KillAtLocation(collider.transform.position);
        }

        public override void Attack(Vector2 destination)
        {
            this.destination = destination;
            this.inTransit = true;
        }

        public void HandleMovement(Vector2 destination)
        {
            Vector2 updatedLocation = Vector2.MoveTowards(transform.position, destination, this.speed * Time.deltaTime);
            transform.position = updatedLocation;
        }

        public bool IsMyTurn()
        {
            return GameManager.instance.gameState == GameManager.GameState.ProjectileExecuteAfterAi || GameManager.instance.gameState == GameManager.GameState.ProjectileExecuteAfterPlayer;
        }

        public bool IsTurnFinished()
        {
            return isTurnFinished;
        }
    }
}
