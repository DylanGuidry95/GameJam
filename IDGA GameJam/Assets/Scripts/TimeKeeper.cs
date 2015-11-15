using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeKeeper
{
	void Awakes ()
	{
		SetTimeScale(0);
	}

	static public void SetTimeScale(float a_value)
	{
		if(a_value < 0.25f && a_value != 0)
		{
			a_value = 0.25f;
		}

		Time.timeScale = a_value;
		globalScale = a_value;
	}

	static public float globalScale;
}