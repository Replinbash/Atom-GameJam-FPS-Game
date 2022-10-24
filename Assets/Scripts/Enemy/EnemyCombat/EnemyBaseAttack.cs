using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using GameJam.EnemyCore;

namespace GameJam.EnemyCombat
{
    public abstract class EnemyBaseAttack : MonoBehaviour
    {
		[SerializeField] protected Transform[] _weaponTransform;
		[SerializeField] protected GameObject[] _weaponPrefab;
		[SerializeField] protected EnemyBaseAttackSO _baseSettings;
		[SerializeField] protected internal Transform Player;

		protected NavMeshAgent _navMesh = null;
        protected EnemyStats _enemyStats = null;
        protected CharacterStats _playerStats = null;
        protected EnemyAnimatonController _animatonController = null;

        protected bool _hasStopped = false;
        protected bool _canAttack = false;
        protected float _timeOfLastAttack = 0;

        private void Awake()
        {           
            _navMesh = GetComponent<NavMeshAgent>();
            _enemyStats = GetComponent<EnemyStats>();
            _playerStats = Player.GetComponent<CharacterStats>();
            _animatonController = GetComponent<EnemyAnimatonController>();
        }

        protected virtual void Start()
        {
			SpawnWeapon();
			_navMesh.speed = _baseSettings.WalkSpeed;
		}

        protected void SpawnWeapon()
        {
            for (int i = 0; i < _weaponPrefab.Length; i++)
            {
                Instantiate(_weaponPrefab[i], _weaponTransform[i]);
            }
        }

        protected void RotateToPlayer()
        {
			var towardsPlayer = Player.transform.position - transform.position;

			transform.rotation = Quaternion.RotateTowards(
				transform.rotation,
				Quaternion.LookRotation(towardsPlayer),
				Time.deltaTime * _baseSettings.TurnRate
			);
		}

		protected abstract void AttackSequence(EnemyBaseAttackSO enemySettings);

		public IEnumerator AttackProcess()
        {
            RotateToPlayer();
            var destinationToPlayer = Vector3.Distance(transform.position, Player.position);

            // Enemy playera doðru koþuyor.
            if (!_canAttack)
            {
                _navMesh.speed = _baseSettings.RunSpeed;
                _navMesh.SetDestination(Player.position);
			}  

            // Attack yaptýktan sonra bekleme süresi
            if (destinationToPlayer > _navMesh.stoppingDistance && _canAttack)
            {
                yield return new WaitForSeconds(_baseSettings.AttackCooldown);
                _canAttack = false;
            }

            // Attack aný.
            if (destinationToPlayer < _navMesh.stoppingDistance)
            {
				_navMesh.speed = _baseSettings.StoppedSpeed;
				AttackSequence(_baseSettings);
            }

            // Enemy saldýrýya geciyor
            else
            {
				_hasStopped = false;
			}               
        }        
    }
}

