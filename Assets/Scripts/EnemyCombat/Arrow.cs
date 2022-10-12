using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Enemies;

public class Arrow : MonoBehaviour
{
    [SerializeField] private EnemyControllerSettings _enemySettings;
    [SerializeField] private CharacterStats _playerStats = null;
    
    [HideInInspector] public RangeAttack _rangeAttack = null;
    [HideInInspector] public Transform _arrowTransformParent;

    private float currentTime = 0;
    private float timeLimitToDePool = 2f;

    private void Awake()
    {
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        _rangeAttack = GameObject.FindGameObjectWithTag("Enemy").GetComponent<RangeAttack>();
    }

    private void Start()
    {
        currentTime = 0;        
    }       

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _enemySettings.LaunchSpeed * 1500);
        if (transform.parent != null)
        {
            currentTime += Time.deltaTime;

            if (currentTime > timeLimitToDePool)
            {
                Debug.Log("oklar yeniden sýraya sokuluyor");
                currentTime = 0;

                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;                
                transform.parent = _arrowTransformParent;
                gameObject.SetActive(false);
                // _rangeAttack.pooledObjects.Enqueue(gameObject);  // Sorun var.
                Destroy(gameObject);
            }
        }        
    }

   private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Arrow Çarpýþtðý Obje: " + collision.transform.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            // CharacterStats _playerstats = collision.transform.GetComponent<CharacterStats>();

            if (_playerStats != null)
            {
                _playerStats.TakeDamage(_enemySettings.Damage);      

            }
        }
    }
}