using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GrowthGage : MonoBehaviour
{
	void Awake()
	{
		timeMod = 1;
	}

	IEnumerator AdjustGage(float a_value)
	{
		float tmp = a_value;

		a_value /= 100;
		a_value *= 180;
		a_value *= -1;

		while(true)
		{
			transform.localEulerAngles = new Vector3(
				transform.localEulerAngles.x,
				transform.localEulerAngles.y,
				a_value);

			temp.text = ((int)tmp).ToString() + " k";

			yield return null;
		}

		/*float tmp = a_value;

		while(transform.localEulerAngles.z != a_value)
		{
			transform.localEulerAngles += new Vector3
				(0, 0, Time.deltaTime * timeMod);

			//temp.text = 
				//((int)(transform.localEulerAngles.z / 180 * 100)).ToString();

			if(Mathf.Abs(Mathf.Abs(a_value) - Mathf.Abs (transform.localEulerAngles.z)) < 1f)
			{
				transform.localEulerAngles = 
					new Vector3 (transform.localEulerAngles.x,
					             transform.localEulerAngles.y,
					             Mathf.Abs (a_value));

				temp.text = ((int)tmp).ToString();

				break;
			}

			yield return null;
		}*/
	}

	private float posNeg;

	[SerializeField] private float m_energy;

	public Text temp;

	public float timeMod;

	public float energy
	{
		get
		{
			return m_energy;
		}

		set
		{
			print ("set");

			if(m_energy == value)
				return;

			m_energy = value;
			StopAllCoroutines();
			StartCoroutine(AdjustGage(m_energy));
		}
	}
}
