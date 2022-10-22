using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Combat", menuName = "Enemy Combat/Range Attack Settings")]
public class EnemyRangeAttackSO : EnemyBaseAttackSO
{
	[Header("Arrow Settings")]
	[SerializeField] protected float _launchSpeed;
	[SerializeField] protected float _fireRate;

	public float LaunchSpeed { get => _launchSpeed; set => _launchSpeed = value; }
	public float FireRate { get => _fireRate; set => _fireRate = value; }


}