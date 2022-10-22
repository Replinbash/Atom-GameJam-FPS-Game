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
        protected bool _startAttack = false;
        protected bool _canAttack = false;
        protected float _timeOfLastAttack = 0;

        protected const int RUN_SPEED = 10;
        protected const int STOPPED_SPEED = 0;
		protected static int ANIMATOR_PARAM_NPC_SPEED = Animator.StringToHash("Speed");


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
		}

        private void Update()
        {
			if (Input.GetKeyDown(KeyCode.T))
            {
                _startAttack = true;
            }

            if (_startAttack)
            {
				_startAttackProcess = StartCoroutine(AttackProcess());
            }

			_animator.SetFloat(ANIMATOR_PARAM_NPC_SPEED, _navMesh.velocity.magnitude);
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
            Vector3 direction = Player.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
            //transform.LookAt(_player);
        }

		protected abstract void AttackSequence(EnemyBaseAttackSO enemySettings);

		protected IEnumerator AttackProcess()
        {
            RotateToPlayer();
            var destinationToPlayer = Vector3.Distance(transform.position, Player.position);

            // Enemy playera doðru koþuyor.
            if (!_canAttack)
            {
                _navMesh.SetDestination(Player.position);
                _navMesh.speed = RUN_SPEED;
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
				_navMesh.speed = STOPPED_SPEED;
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

