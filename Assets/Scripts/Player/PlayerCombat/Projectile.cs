using UnityEngine;

namespace GameJam.PlayerCombat
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private PlayerSettings _playerSettings;
		[SerializeField] private GameObject impactVFX;
		private bool _collided;

		private void Awake()
		{
			Destroy(this.gameObject, 10f);
		}

		private void OnCollisionEnter(Collision collision)
		{
			Debug.Log("The Object Where Spells Collider: " + collision.collider.name);

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
				if (enemyStats != null)
				{
					enemyStats.TakeDamage(_playerSettings.Damage);
				}
			}
		}
	}
}

