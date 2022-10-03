using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Player;

namespace GameJam.PlayerCombat
{
    public class ProjectTile : MonoBehaviour
    {
        [SerializeField] private CharacterControllerSettings _playerSettings;
        [SerializeField] private GameObject impactVFX;
        private bool _collided;

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Büyülerin Çarpýþtýðý Obje: " + collision.collider.name);

            if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !_collided)
            {               
                var impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;
                Destroy(impact, 2);

                _collided = true;
                Destroy(this.gameObject);                
            }
            
            if (collision.gameObject.CompareTag("Enemy"))
            {
                CharacterStats enemyStats = collision.transform.GetComponent<CharacterStats>();
                if (enemyStats != null) enemyStats.TakeDamage(_playerSettings.Damage);
            }
        }
    }
}

