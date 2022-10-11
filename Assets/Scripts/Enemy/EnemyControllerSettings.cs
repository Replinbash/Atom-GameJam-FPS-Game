using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Enemies
{
    [CreateAssetMenu(fileName ="Enemy", menuName = "EnemyController")]
    public class EnemyControllerSettings : ScriptableObject
    {
        [Header("Health Settings")]
        public int MaxHealth;

        [Header("Attack Settings")]
        public int Damage;
        public float AttackSpeed;

        [Header("Arrow Settings")]
        public float LaunchSpeed;
        public float FireRate;

        [Header("Select Weapon")]
        public AnimatorOverrideController OverrideController;
    }
}

