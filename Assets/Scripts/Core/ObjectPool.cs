using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

	public GameObject GetObject(GameObject gameObject)
	{
		if (pool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
		{
			if (objectList.Count == 0)
			{
				return CreateNewObject(gameObject);
			}

			else
			{
				GameObject obj = objectList.Dequeue();
				obj.SetActive(true);
				return obj;
			}
		}

		else
		{
			return CreateNewObject(gameObject);
		}
	}

	private GameObject CreateNewObject(GameObject gameObject)
	{
		GameObject obj = Instantiate(gameObject);
		obj.name = gameObject.name;
		return obj;
	}

	public void ReturnGameObject(GameObject gameObject)
	{
		if (pool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
		{
			objectList.Enqueue(gameObject);
		}

		else
		{
			Queue<GameObject> newObjectQueue = new Queue<GameObject>();
			newObjectQueue.Enqueue(gameObject);
			pool.Add(gameObject.name, newObjectQueue);
		}

		gameObject.SetActive(false);
	}
}
