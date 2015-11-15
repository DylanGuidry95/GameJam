using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProgressBar : MonoBehaviour
{
	void Start()
	{
		max = transform.localScale.x;
		transform.localScale =
			new Vector3(0, transform.localScale.y, transform.localScale.z);

		time = time == 0 ? 1 : time;
	}

	void Update ()
	{
		if(transform.localScale.x <= max)
		{
			transform.localScale += new Vector3(Time.deltaTime, 0, 0) / time * TimeKeeper.globalScale;
		}
	}

	float max;
	public float time;
}
