using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Player;
using System;
using Random = UnityEngine.Random;

namespace GameJam.PlayerCombat
{
    public class RangeSpellSkill : MonoBehaviour
    {
        [SerializeField] private GameObject[] _projectTiles;
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private ChargeSkill _charge;
        [SerializeField] private Camera _cam;
        [SerializeField] private GameObject _fireShield;
        [SerializeField] private Transform _LHFirePoint, _RHFirePoint, _MHFirePoint;        
        
        private Animator _animator;
        private Vector3 _destination;      
        private bool _leftHand;
        private float _timeToFire;

        public static event Action <bool> SetDamage;

        private void Awake()
        {
			_animator = GetComponent<Animator>();

		}

        private void Start() => _charge.DisableShield(_fireShield);    
        
        #region Input
        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && (Time.time >= _timeToFire) && !_animator.GetBool("shield")) 
            {
                _animator.SetBool("attack", true);            
                ShootProjectTile();
                _charge.EnableProjectile();
            }

            else if (Input.GetButtonUp("Fire1"))
            {
                _animator.SetBool("attack", false);
            }

            if (Input.GetButton("Fire2"))
            {
                _animator.SetBool("shield", true);
                _charge.EnableShield(_fireShield);
                SetDamage?.Invoke(_charge.canDefense);
            }

            else if (Input.GetButtonUp("Fire2"))
            {            
                _animator.SetBool("shield", false);
                _charge.DisableShield(_fireShield);
                SetDamage?.Invoke(_charge.canDefense);
            }
        }
        #endregion


        #region Projectile

        private void RayMethod()
        {
            Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _destination = hit.point;
            }

            else
            {
                _destination = ray.GetPoint(1000);
            }
        }

        private void ShootProjectTile()
        {
            RayMethod();

            if (_leftHand)
            {
                _leftHand = false;
                InstantiateProjectTile(_LHFirePoint);
            }

            else
            {
                _leftHand = true;
                InstantiateProjectTile(_RHFirePoint);
            }
        }        

        private void InstantiateProjectTile(Transform firePoint)
        {
            if (_playerSettings.Charge > 0)
            {
                _timeToFire = Time.time + 1 / _playerSettings.FireRate;

                // Random sayý alýr.
                var randomProjectile = Random.Range(0, _projectTiles.Length);
                var projectileObj = Instantiate(_projectTiles[randomProjectile], firePoint.position, Quaternion.identity);
                projectileObj.GetComponent<Rigidbody>().velocity = (_destination - firePoint.position).normalized * _playerSettings.ProjectileSpeed;
                iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-_playerSettings.ArcRange, _playerSettings.ArcRange),
                Random.Range(-_playerSettings.ArcRange, _playerSettings.ArcRange), 0), Random.Range(0.5f, 2));
                            
            }            
        }
        #endregion      
    }
}
