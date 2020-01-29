using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Utils
{
	public class GameObjectPool : MonoBehaviour
	{
		static public GameObjectPool Instance { private set; get; }

		[SerializeField] private List<GameObject> prefabs = null;

		private Dictionary<string, List<GameObject>> objectLists = new Dictionary<string, List<GameObject>>();
		private Dictionary<string, Transform> objectHolders = new Dictionary<string, Transform>();

		protected void Awake()
		{
			Instance = this;

			// Create pool holders gameObjects
			foreach (GameObject prefab in prefabs)
			{
				GameObject o = new GameObject(prefab.name);
				o.transform.SetParent(transform, false);
				objectHolders.Add(prefab.name, o.transform);
			}
		}

		public GameObject GetObject<T>(T type) where T : struct, IConvertible => GetObjectInternal(type.ToString());

		private GameObject GetObjectInternal(string typeName = "")
		{
			// If the pool is Empty, you have to instantiate from a prefab 
			if (!objectLists.ContainsKey(typeName))
			{
				objectLists.Add(typeName, new List<GameObject>());
			}

			List<GameObject> list = objectLists[typeName];
			if (list.IsEmpty())
			{
				// Instantiating from a prefab
				GameObject prefab = GetPrefab(typeName);
				GameObject newObject = Instantiate(prefab);
				newObject.transform.SetParent(objectHolders[typeName], false);
				newObject.name = prefab.name;

				return newObject;
			}
			else
			{
				// If the pool is not empty, extract one and return it back
				int lastCellIndex = list.Count - 1;
				GameObject obj = list[lastCellIndex];
				list.RemoveAt(lastCellIndex);
				obj.SetActive(true);

				return obj;
			}
		}

		private GameObject GetPrefab(string prefabName)
		{
			if (prefabs == null)
			{
				throw new Exception("GameObjectPool-> Prefab List is empty");
			}
			else
			{
				GameObject requestedPrefab = prefabs.Find(x => x.name == prefabName);
				if (requestedPrefab)
				{
					return requestedPrefab;
				}
				else
				{
					throw new Exception("GameObjectPool->GetPrefab : Could not find " + prefabName + " prefab.");
				}
			}
		}

		/// <summary>
		/// This function returns the object back to the pool, so they can be used for further need 
		/// </summary>
		public void ReturnToRepository<T>(GameObject obj, T type) where T : struct, IConvertible
		{
			string groupName = type.ToString();

			// Change objects parent and reset it
			obj.transform.SetParent(objectHolders[groupName]);
			obj.transform.position = Vector3.zero;
			obj.gameObject.SetActive(false);

			// Add object to its related list
			if (!objectLists.ContainsKey(groupName))
			{
				objectLists.Add(groupName, new List<GameObject>());
			}
			objectLists[groupName].Add(obj.gameObject);
		}
	}
}