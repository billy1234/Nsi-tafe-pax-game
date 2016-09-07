using UnityEngine;
using System.Collections;

public class ReallyPoslishedSpawner : MonoBehaviour {

	public int unitIndex;
	public float spawnInterval;
	private EntitySpawner spawner;

	// Use this for initialization
	void Start () 
	{
		spawner = EntitySpawner.getSpawner ();
		StartCoroutine (spawnLoop());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator spawnLoop()
	{
		while (Application.isPlaying)
		{
			spawner.Instantiate (unitIndex,transform);
			yield return new WaitForSeconds (spawnInterval);
		}

	}


}
