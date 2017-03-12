using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public enum Instruction
    {
        Up, Down, Left, Right, Nothing, Attack, Hit
    }

    public interface IEntity : IHittable
    {
        void Update();
        void HandleInstruction(Instruction instruction);
        void Attack(Weapon weapon, Vector2 destination);
    }
}
