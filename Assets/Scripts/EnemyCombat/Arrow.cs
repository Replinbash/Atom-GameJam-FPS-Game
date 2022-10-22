using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private EnemyRangeAttackSO m_EnemyRangeAttackSO;
    private ObjectPool _pool;
    private Rigidbody _rb;

	private float currentTime = 0;
	private float timeLimitToDePool = 5f;

	private void Awake()
    {
		_pool = FindObjectOfType<ObjectPool>();   
        _rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		currentTime = 0;
	}

	private void OnDisable()
	{
		if (_pool != null)
		{
			_rb.velocity = Vector3.zero;
			_rb.angularVelocity = Vector3.zero;
			_pool.ReturnGameObject(this.gameObject);
		}
	}

	private void Update()
    {
		_rb.AddRelativeForce(Vector3.forward * m_EnemyRangeAttackSO.LaunchSpeed);

		currentTime += Time.deltaTime;

		if (currentTime > timeLimitToDePool)
		{
			Debug.Log("Reload Arrow");
			currentTime = 0;
			OnDisable();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Arrow hit collider name: " + collision.transform.name);

		if (collision.gameObject.CompareTag("Player"))
		{
			CharacterStats _playerstats = collision.transform.GetComponent<CharacterStats>();

			if (_playerstats != null)
			{
				_playerstats.TakeDamage(m_EnemyRangeAttackSO.Damage);
				OnDisable();
			}
		}
	}

}