using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Orbiting : MonoBehaviour 
{
	public List<GameObject> planetList = new List<GameObject>();
	List<GameObject> planetsInOrbit = new List<GameObject>();

	//public float distanceApart;
	void Start()
	{
		SpawnPlanet(5);
		SpawnPlanet(10);
	}


	// Update is called once per frame
	void Update ()
	{

	
		foreach(GameObject ps in planetsInOrbit)
			OrbitPlanet(ps);

	}

	void OrbitPlanet(GameObject plan)
	{ 
		float speedrot = 1;
		float speedorb = 0;
		if(plan.GetComponent<PlanetMovementStats>())
		{
			speedrot = plan.GetComponent<PlanetMovementStats>().rotationSpeed;
			speedorb = plan.GetComponent<PlanetMovementStats>().orbitSpeed;
		}

		plan.transform.rotation = new Quaternion(plan.transform.rotation.x, 
		                                         plan.transform.rotation.y+speedrot, 
		                                         plan.transform.rotation.z, 
		                                      Random.rotation.w);
		
		Vector3 centre = gameObject.transform.position;
		Vector3 reletivePos = plan.transform.position - gameObject.transform.position;
		float distance = Vector3.Distance(reletivePos, transform.position);

		if(speedorb > 0)
		{
			float radien = plan.GetComponent<PlanetMovementStats>().orbitPos * (3.14f/180f);
			Vector2 newxz = Vector2.zero;

			newxz.y = (centre.z + distance * Mathf.Sin(radien));
			newxz.x = (centre.x + distance * Mathf.Cos(radien));

			plan.GetComponent<Transform>().position = new Vector3(newxz.x, gameObject.transform.position.y, newxz.y);

			plan.GetComponent<PlanetMovementStats>().orbitPos += speedorb * Time.timeScale;

		}
	}
	public void SpawnPlanet(float distance)
    {

		int r = Random.Range(0, planetList.Count-1);

		//if(planetList.Count > 1)
		//planetsInOrbit.Add((GameObject)Instantiate(planetList[r], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + planetsInOrbit[planetsInOrbit.Count-1].GetComponent<PlanetMovementStats>().positionFromSun + distance), Random.rotation));

		//else 
			planetsInOrbit.Add((GameObject)Instantiate(planetList[r], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + distance ), Random.rotation));

		planetsInOrbit[planetsInOrbit.Count-1].GetComponent<PlanetMovementStats>().positionFromSun = planetsInOrbit[planetsInOrbit.Count -1].transform.position.z - transform.position.z;
	}

}
