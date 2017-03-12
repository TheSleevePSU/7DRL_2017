using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Entities
{
    public enum StatKey
    {
        Health, SightRange, AttackStrength, Speed
    }

    public class Stats : Dictionary<StatKey, int>
    {

        public Stats(int health, int range, int strength)
        {
            this[StatKey.Health] = health;
            this[StatKey.SightRange] = range;
            this[StatKey.AttackStrength] = strength;
        }

        public int GetHealth()
        {
            return this[StatKey.Health];
        }

        public void SetHealth(int value)
        {
            this[StatKey.Health] = value;
        }

        public int GetSightRange()
        {
            return this[StatKey.SightRange];
        }

        public int GetAttackStrength()
        {
            return this[StatKey.AttackStrength];
        }
        public void SetAttackStrength(int value)
        {
            this[StatKey.AttackStrength] = value;
        }

        public int GetSpeed()
        {
            return this[StatKey.Speed];
        }
        public void SetSpeed(int value)
        {
            this[StatKey.Speed] = value;
        }
    }
}
