using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Player
{
    [CreateAssetMenu(menuName = "PlayerController")]
    public class CharacterControllerSettings : ScriptableObject
    {
        [Header("Character Settings")]
        public float CurrentSpeed;
        public float MaxSpeed;
        public float AccelerationSpeed;
        public float Gravity;
        public float JumpHeight;
        public float MouseSensitivity;

        [Header("Health Settings")]
        public int MaxHealth;

        [Header("Stamina Settings")]
        public float Stamina;
        public float MaxStamina;
        public float StaminaAmount;

        [Header("Projectile Settings")]
        public int Damage;
        public float ProjectileSpeed; 
        public float FireRate; 
        public float ArcRange;
        public float ProjectileAmount;
        public float ShieldAmount;
        public float Charge;        
        public float MaxCharge;
    }
}
