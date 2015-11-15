using UnityEngine;
using System.Collections;

public class PlanetMovementStats : MonoBehaviour 
{
	public float rotationSpeed;
	public float orbitSpeed;
	public float positionFromSun;
	[SerializeField]
	private float _orbitPos;

	public float orbitPos
	{
		get
		{
			return _orbitPos;
		}

		set
		{
			if(value > 360)
				value = 1;
			_orbitPos = value;

		}
	}

}
