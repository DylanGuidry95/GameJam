using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{


	private float speed;
	// Update is called once per frame
	void Update ()
	{
		speed = GetComponentInParent<Stats>().currentEnergy;

		//gameObject.GetComponent<Texture>().
		//gameObject.GetComponent<Material>().color = new Color(gameObject.GetComponent<Material>().color.r,
		//                                                        gameObject.GetComponent<Material>().color.g,
		//                                                        gameObject.GetComponent<Material>().color.b,
		//                                                        ((GetComponentInParent<Stats>().currentEnergy/gameObject.GetComponent<Material>().color.a) * GetComponentInParent<Stats>().currentEnergy));
		transform.RotateAround(transform.localPosition, transform.up, speed * Time.deltaTime); //= new Quaternion(transform.rotation ;
	}
}
