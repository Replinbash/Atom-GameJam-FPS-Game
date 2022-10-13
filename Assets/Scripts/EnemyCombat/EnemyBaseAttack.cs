using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using GameJam.Enemies;

namespace GameJam.EnemyCombat
{
    public abstract class EnemyBaseAttack : MonoBehaviour
    {
		[SerializeField] protected Transform[] _weaponTransform;
		[SerializeField] protected GameObject[] _weaponPrefab;
		[SerializeField] protected Transform _player;        

        protected NavMeshAgent _navMesh = null;
        protected Animator _animation = null;
        protected EnemyStats _enemyStats = null;
        protected CharacterStats _playerStats = null;

        protected bool _hasStopped = false;
        protected bool _startAttack = false;
        protected bool _canAttack = false;
        protected float _timeOfLastAttack = 0;

        private void Awake()
        {           
            _navMesh = GetComponent<NavMeshAgent>();
            _animation = GetComponentInChildren<Animator>();
            _enemyStats = GetComponent<EnemyStats>();
            _playerStats = _player.GetComponent<CharacterStats>();            
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
                StartCoroutine(Attack());
            }
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
            Vector3 direction = _player.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
            //transform.LookAt(_player);
        }

		protected abstract void AttackSequence();

		protected IEnumerator Attack()
        {
            RotateToPlayer();
            var destinationToPlayer = Vector3.Distance(transform.position, _player.position);

            // Enemy playera doðru koþuyor.
            if (!_canAttack)
            {
                _navMesh.SetDestination(_player.position);
                _animation.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
            }  

            // Attack yaptýktan sonra bekleme süresi
            if (destinationToPlayer > _navMesh.stoppingDistance && _canAttack)
            {
                yield return new WaitForSeconds(0.80f);
                _canAttack = false;
            }

            // Attack aný.
            if (destinationToPlayer < _navMesh.stoppingDistance)
            {
                _animation.SetFloat("Speed", 0f);
                AttackSequence();
            }

            // Enemy saldýrýya geciyor
            else if (_hasStopped) 
                _hasStopped = false;

        }   

        
    }
}

