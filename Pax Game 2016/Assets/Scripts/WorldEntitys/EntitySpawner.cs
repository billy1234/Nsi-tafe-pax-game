using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntitySpawner : MonoBehaviour 
{
	public entityInfo[] spawnables;
	public struct spawnableEntityInfo
	{
		public string name;
		public GameObject prefab;
	}

	[System.Serializable]
	public class entityInfo 
	{
		public string name;
		public GameObject prefab;
		public int initalAmount = 100;
		[HideInInspector]
		public List<GameObject> activeObjects;
		[HideInInspector]
		public List<GameObject> inactiveObjects;

	}


	public static EntitySpawner getSpawner()
	{
		return GameObject.FindObjectOfType<EntitySpawner>();
	}



	public int GetPrefabIndex(GameObject prefab)
	{
		//spawnables.
		for (int i = 0; i < spawnables.Length; i++) 
		{
			if (prefab == spawnables [i].prefab)
			{
				return i;
			}
		}
		Debug.LogError ("cant find prefab: " + prefab);
		return -1;
	}
	public GameObject Instantiate(int spawnableIndex)
	{
		return Instantiate (spawnableIndex,Vector3.zero,Quaternion.identity);
	}

	public GameObject Instantiate(int spawnableIndex,Transform orientation)
	{
		return Instantiate (spawnableIndex,orientation.position,orientation.rotation);
	}
	public GameObject Instantiate(int spawnableIndex,Vector3 position, Quaternion rotation)
	{
		GameObject g = activateGameObject (spawnableIndex);
		g.transform.position = position;
		g.transform.rotation = rotation;
		return g;
	}	

	private GameObject activateGameObject(int index)
	{
		if (spawnables [index].inactiveObjects.Count ==0) 
		{
			instanciateSpawnable (index);
			Debug.LogWarning("Carefull object spawner ran out of: " + spawnables [index].prefab +" maybe try increasing the inital spawn amount");
		}
		GameObject g = spawnables[index].inactiveObjects[0];
		g.SetActive (true);
		spawnables [index].activeObjects.Add (g);
		spawnables [index].inactiveObjects.RemoveAt (0);
		StartCoroutine( parentNextFrame (g, null));
		return g;
	}

	private void instanciateSpawnable(int index)
	{
		GameObject g = Instantiate(spawnables [index].prefab) as GameObject;
		g.AddComponent<SpawnableEntity>().Initalize(index);
		g.SetActive (false);
		StartCoroutine(parentNextFrame(g,transform));
		spawnables[index].inactiveObjects.Add(g);
	}

	public void deactivateGameObject(GameObject g,int index)
	{
		spawnables [index].inactiveObjects.Add (g);
		spawnables [index].activeObjects.Remove (g);
		StartCoroutine (parentNextFrame (g, transform));
	}

	IEnumerator parentNextFrame(GameObject g,Transform t)
	{
		yield return new WaitForSeconds (0f);
		if (g != null) 
		{
			g.transform.parent = t;
		}

	}

	private void InitalizeHeap()
	{
		for (int i = 0; i < spawnables.Length; i++)
		{
			spawnables [i].inactiveObjects = new List<GameObject>(spawnables[i].initalAmount);
			for(int x=0; x < spawnables[i].initalAmount; x++)
			{				
				instanciateSpawnable (i);
			}
		}

	}

	void Awake ()
	{
		if (GetComponents<EntitySpawner> ().Length > 1)
		{
			Debug.LogError ("this is not the only entity spaner in the scene");

		}
	
		InitalizeHeap ();

	
	}

}
