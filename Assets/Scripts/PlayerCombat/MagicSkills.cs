using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Player;

namespace GameJam.PlayerCombat
{
    public class MagicSkills : MonoBehaviour
    {
        [SerializeField] private CharacterControllerSettings _playerSettings;
        [SerializeField] private Animator _animator;
        [SerializeField] private ChargeManager _chargeManager;
        [SerializeField] private Camera _cam;
        [SerializeField] private GameObject[] _projectTiles;
        [SerializeField] private GameObject _fireShield;
        [SerializeField] private GameObject _SpecialAttack;
        [SerializeField] private Transform _LHFirePoint, _RHFirePoint, _MHFirePoint;        
        
        private Vector3 _destination;      
        private bool _leftHand;
        private float _timeToFire;

        public static event System.Action <bool> SetDamage;

        // Inputs
        public bool IsPressLeft => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);

        private void Start() => _chargeManager.DisableShield(_fireShield);    
        
        #region Input
        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && (Time.time >= _timeToFire) && !_animator.GetBool("shield")) 
            {
                _animator.SetBool("attack", true);            
                ShootProjectTile();
                _chargeManager.EnableProjectile();
            }

            else if (Input.GetButtonUp("Fire1"))
            {
                _animator.SetBool("attack", false);
            }

            if (Input.GetButton("Fire2"))
            {
                _animator.SetBool("shield", true);
                _chargeManager.EnableShield(_fireShield);
                SetDamage?.Invoke(_chargeManager.canDefense);
            }

            else if (Input.GetButtonUp("Fire2"))
            {            
                _animator.SetBool("shield", false);
                _chargeManager.DisableShield(_fireShield);
                SetDamage?.Invoke(_chargeManager.canDefense);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _animator.SetBool("ulti", true);
                ShootUlti();
            }

            else if (Input.GetKeyUp(KeyCode.Q))
            {
                _animator.SetBool("ulti", false);
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

        private void ShootUlti()
        {
            //RayMethod();
            var projectileObj = Instantiate(_SpecialAttack, _MHFirePoint.position, Quaternion.identity);
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
