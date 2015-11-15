using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeProgress : MonoBehaviour
{
	void Start()
	{
		max = transform.localScale.y;
		transform.localScale =
			new Vector3(transform.localScale.x, 0.3f, transform.localScale.z);
		
		time = time == 0 ? 1 : time;
	}

	void Update ()
	{
		if(progress.transform.localScale.x >= 1)
		{
			if(transform.localScale.y < 1f)
			{
				if(TimeKeeper.globalScale > 1.75)
				{
					transform.localScale -= new Vector3 (0, Time.deltaTime, 0)
						/ time * TimeKeeper.globalScale;
				}
				else
				{
					transform.localScale += new Vector3 (0, Time.deltaTime, 0)
						/ time * TimeKeeper.globalScale;
				}
			}
			else
			{
				//win
			}
		}
	}

	
	public float time;
	public GameObject progress;
	float max;
}
