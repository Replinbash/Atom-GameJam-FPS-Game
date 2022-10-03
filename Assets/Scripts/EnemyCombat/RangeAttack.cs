using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Enemies
{
    public class RangeAttack : EnemyController
    {
        [SerializeField] private Queue<GameObject> pooledObjects; // object pooling queue
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform _arrowTransform;
        [SerializeField] private int poolSize;

        private Arrow _arrowScript;
        private GameObject poolHolder;
        private Transform poolHolderTrans;
        private float _timeToFire;

        private void Start()
        {
            CombatBehaviour += AttackSequence;           

            poolHolder = new GameObject(transform.name + " Arrow Pool");
            poolHolderTrans = poolHolder.gameObject.transform;

            CreatePool();
        }

        protected override void AttackSequence()
        {
            //player durursa
            if (!_hasStopped)
            {
                _hasStopped = true;
                _timeOfLastAttack = Time.time - 1.5f;
            }

            else if (Time.time >= _timeOfLastAttack + _enemySettings.AttackSpeed)
            {
                _timeOfLastAttack = Time.time;
                _canAttack = true;

                if (_animation != null) _animation.SetTrigger("rangeAttack");

                if (Time.time >= _timeToFire)
                {
                    Debug.Log("Bowcu Attack Yaptý");
                    StartCoroutine(GetArrow(1));
                }
            }
        }

        #region Pooling

        public IEnumerator GetArrow(int waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            GetPooledObject(_arrowTransform.position, _player.transform.position + Vector3.up * 2.60f);
        }           

        public void CreatePool()
        {
            pooledObjects = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject _obj = Instantiate(arrowPrefab, poolHolderTrans); //Obje oluþtur
                _arrowScript = _obj.GetComponent<Arrow>();
                _arrowScript._rangeAttack = this;
                _arrowScript._arrowTransformParent = poolHolderTrans;
                
                _obj.SetActive(false); //Objeyi kapat
                pooledObjects.Enqueue(_obj); //Objeyi listeye ekle               
            }
        }

        public void AddExtraArrowPool()
        {
            GameObject _obj = Instantiate(arrowPrefab); //Obje oluþtur
            _obj.transform.parent = poolHolderTrans; // Objeleri boþ bir objenin altýna toplar.
            _obj.SetActive(false); //Objeyi kapat
            pooledObjects.Enqueue(_obj); //Objeyi listeye ekle
            poolSize++; //Objeyi çoðalt
        }

        public GameObject GetPooledObject(Vector3 position, Vector3 direction)
        {
            // If the queue is empty
            if (pooledObjects.Count == 0)
            {
                AddExtraArrowPool();
            }

            GameObject _obj = pooledObjects.Dequeue(); //Objeyi sýradan çýkarýyoruz

            //Pozisyonunu deðiþtirip aktif hale getiriyoruz.
            _obj.transform.position = position;
            _obj.transform.LookAt(direction);
            _obj.SetActive(true);

            //Debug.Log("havuzdaki ok sayýsý: " + pooledObjects.Count);
            return _obj;
        }

        #endregion
    }
}

