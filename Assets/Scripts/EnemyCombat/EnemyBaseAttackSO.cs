using UnityEngine;

public class EnemyBaseAttackSO : ScriptableObject
{
	[Header("Health Settings")]
	[SerializeField] protected int _maxHealth;

	[Header("Attack Settings")]
	[SerializeField] protected int _damage;
	[SerializeField] protected float _attackSpeed;

	[Header("Select Weapon")]
	public AnimatorOverrideController OverrideController;

	public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
	public int Damage { get => _damage; set => _damage = value; }
	public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
}