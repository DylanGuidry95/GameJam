using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
	void Start ()
	{
	
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			TimeKeeper.SetTimeScale(0);
		
		}
	}

	float currentScale = 1;
}