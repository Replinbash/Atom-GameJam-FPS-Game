using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Combat", menuName = "Enemy Combat/Malee Attack Settings")]
public class EnemyMaleeAttackSO : EnemyBaseAttackSO
{
	[Header("Attack Range Settings")]
	[SerializeField] private float _attackRange;
	[SerializeField] private LayerMask _targetLayer;
	[SerializeField] private Vector3 _attackPoint;

	public float AttackRange { get => _attackRange; set => _attackRange = value; }
	public LayerMask TargetLayer { get => _targetLayer; set => _targetLayer = value; }	
	public Vector3 AttackPoint { get => _attackPoint; set => _attackPoint = value; }
}
