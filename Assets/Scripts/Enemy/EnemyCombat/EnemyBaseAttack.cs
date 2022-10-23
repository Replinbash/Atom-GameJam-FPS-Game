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
		[SerializeField] protected EnemyBaseAttackSO _enemySettings;
		[SerializeField] protected internal Transform Player;

		protected NavMeshAgent _navMesh = null;
        protected Animator _animator = null;
        protected EnemyStats _enemyStats = null;
        protected CharacterStats _playerStats = null;
        protected Coroutine _startAttackProcess;

        protected bool _hasStopped = false;
        protected bool _canAttack = false;
        protected float _timeOfLastAttack = 0;

        private void Awake()
        {           
            _navMesh = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            _enemyStats = GetComponent<EnemyStats>();
            _playerStats = Player.GetComponent<CharacterStats>();            
        }

        protected virtual void Start()
        {
			SpawnWeapon();
			_navMesh.speed = _enemySettings.WalkSpeed;
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
				Time.deltaTime * _enemySettings.TurnRate
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
                _navMesh.speed = _enemySettings.RunSpeed;
                _navMesh.SetDestination(Player.position);
			}  

            // Attack yaptýktan sonra bekleme süresi
            if (destinationToPlayer > _navMesh.stoppingDistance && _canAttack)
            {
                yield return new WaitForSeconds(_enemySettings.AttackCooldown);
                _canAttack = false;
            }

            // Attack aný.
            if (destinationToPlayer < _navMesh.stoppingDistance)
            {
				_navMesh.speed = _enemySettings.StoppedSpeed;
				AttackSequence(_enemySettings);
            }

            // Enemy saldýrýya geciyor
            else
            {
				_hasStopped = false;
			}               
        }        
    }
}

