using GameJam.EnemyCombat;
using UnityEngine;

namespace GameJam.EnemyCore
{
	public class EnemySightSensor : MonoBehaviour
	{
		[SerializeField] private LayerMask _ignoreMask;

		private Ray _ray;
		private EnemyBaseAttack enemyBaseAttack;

		private void Awake()
		{
			enemyBaseAttack = GetComponent<EnemyBaseAttack>();
		}

		public bool Ping()
		{
			if (enemyBaseAttack.Player == null)
				return false;

			_ray = new Ray(this.transform.position, enemyBaseAttack.Player.position - this.transform.position);

			var direction = new Vector3(_ray.direction.x, 0, _ray.direction.z);

			var angle = Vector3.Angle(direction, this.transform.forward);

			if (angle > 60)
				return false;

			if (!Physics.Raycast(_ray, out var hit, 100, ~_ignoreMask))
			{
				return true;
			}

			if (hit.collider.CompareTag("Player"))
			{
				return true;
			}

			return false;
		}

		private void OnDrawGizmos()
		{
			// player ray line
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_ray.origin, _ray.origin + _ray.direction * 100);

			// enemy tranform ray line
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 100);
		}
	}
}
