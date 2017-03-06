﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public enum Instruction
    {
        Up, Down, Left, Right, Attack, Hit
    }

    public interface IEntity
    {
        void Update();
        void HandleInstruction(Instruction instruction);
        void HandleHit(Weapon weapon);
        void Attack(Weapon weapon, Vector2 destination);
    }
}
