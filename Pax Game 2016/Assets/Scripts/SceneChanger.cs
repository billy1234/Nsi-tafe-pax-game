using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{


	void Awake() 
	{
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.Keypad0))
		{
			SceneManager.LoadScene (0);
		}
		else if(Input.GetKey (KeyCode.Keypad1))
		{
			SceneManager.LoadScene (1);
		}
	
	}
}
