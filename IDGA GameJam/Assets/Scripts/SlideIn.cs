using UnityEngine;
using System.Collections;

public class SlideIn : MonoBehaviour
{
	
	void Start () {
	
	}

	void Update ()
	{
		if(GetComponentInChildren<LifeProgress>().progress.transform.localScale.x >= 1f)
		{
			if(transform.localScale.x < 1f)
			{
				transform.localScale += new Vector3(Time.deltaTime * 10, 0, 0);
			}
			if(transform.localScale.x > 1f)
			{
				transform.localScale = new Vector3 (1, 1, 1);
			}
		}
	}
}
