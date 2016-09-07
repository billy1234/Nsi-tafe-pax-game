using UnityEngine;
using System.Collections;

public class SpawnableEntity : MonoBehaviour 
{
	private int prefabIndex = -1;
	private EntitySpawner spawner;

	public void Initalize(int prefabIndex)
	{
		this.prefabIndex = prefabIndex;
		spawner = EntitySpawner.getSpawner ();
	}
		

	void OnDisable()
	{
		
		if (prefabIndex == -1) 
		{
			Debug.LogError (this + " was not initalized properly");
		}
		if (spawner != null)
		{
			spawner.deactivateGameObject (gameObject, prefabIndex);
		}
	}
}
