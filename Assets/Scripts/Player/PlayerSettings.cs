using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "PlayerController")]
public class PlayerSettings : ScriptableObject
{
	[Header("Character Settings")]
	public float CurrentSpeed;
	public float MaxSpeed;
	public float AccelerationSpeed;
	public float Gravity;
	public float JumpHeight;
	public float MouseSensitivity;

	[Header("Health Settings")]
	public int Health;
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

	[Header("Defence Settings")]
	public float ShieldAmount;

	[Header("Mana Settings")]
	public float Mana;
	public float MaxMana;
}

