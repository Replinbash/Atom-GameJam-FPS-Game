using UnityEngine;
using Random = UnityEngine.Random;

namespace GameJam.PlayerCombat
{
	public class RangeSpellSkill : BaseSkill
	{
		[SerializeField] private GameObject[] _projectTiles;
		[SerializeField] private Transform[] _spawnPoint;
		[SerializeField] private PlayerSettings _playerSettings;
		[SerializeField] private Camera _cam;

		private GameObject _holder;
		private Vector3 _destination;
		private bool _leftHand;
		private float _timeToFire;

		protected override void OnEnable()
		{
			base.OnEnable();
			_inputReader.AttackEvent += ShootProjectTile;
			DefenceSkill.DefenceActivatedEvent += DefenseActive;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_inputReader.AttackEvent -= ShootProjectTile;
			DefenceSkill.DefenceActivatedEvent -= DefenseActive;
		}

		private void Start()
		{
			_holder = new GameObject("Spells Pool");
		}

		private void CreateRay()
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

		private void DefenseActive(bool isDefense)
		{
			if (isDefense)
			{
				_inputReader.AttackEvent -= ShootProjectTile;
			}

			else
			{
				_inputReader.AttackEvent += ShootProjectTile;
			}
		}

		private void ShootProjectTile()
		{
			CreateRay();

			if (Time.time >= _timeToFire)
			{
				if (_leftHand)
				{
					_leftHand = false;
					InstantiateProjectTile(_spawnPoint[0]);
				}

				else
				{
					_leftHand = true;
					InstantiateProjectTile(_spawnPoint[1]);
				}
			}
		}

		private void InstantiateProjectTile(Transform firePoint)
		{
			if (_playerSettings.Mana > 0)
			{
				_timeToFire = Time.time + 1 / _playerSettings.FireRate;
				var randomProjectile = Random.Range(0, _projectTiles.Length);
				var projectileObj = Instantiate(_projectTiles[randomProjectile], firePoint.position, Quaternion.identity, _holder.transform);
				projectileObj.GetComponent<Rigidbody>().velocity = (_destination - firePoint.position).normalized * _playerSettings.ProjectileSpeed;
				iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-_playerSettings.ArcRange, _playerSettings.ArcRange),
				Random.Range(-_playerSettings.ArcRange, _playerSettings.ArcRange), 0), Random.Range(0.5f, 2));
				ReduceProjectileMana();
			}
		}
		public void ReduceProjectileMana()
		{
			_playerSettings.Mana -= _playerSettings.ProjectileAmount;
			if (_playerSettings.Mana <= 0)
				_playerSettings.Mana = 0;
		}
	}
}
