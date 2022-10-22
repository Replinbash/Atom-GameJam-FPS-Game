using UnityEngine;

public class EnemyBaseAttackSO : ScriptableObject
{
	[Header("Health Settings")]
	[SerializeField] protected int _maxHealth;

	[Header("AI Settings")]
	[SerializeField] protected float _attackCooldown;

	[Header("Attack Settings")]
	[SerializeField] protected int _damage;
	[SerializeField] protected float _attackSpeed;

	[Header("Weapon Spawn Settings")]
	public Vector3[] WeaponSpawnPosition;
	public Quaternion[] WeaponSpawnRotation;

	[Header("Select Weapon")]
	public AnimatorOverrideController OverrideController;

	public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
	public int Damage { get => _damage; set => _damage = value; }
	public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
	public float AttackCooldown { get => _attackCooldown; set => _attackCooldown = value; }
}