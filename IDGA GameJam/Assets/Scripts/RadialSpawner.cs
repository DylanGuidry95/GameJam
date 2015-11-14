﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadialSpawner : MonoBehaviour
{
	[SerializeField]
	   bool pauseSpawn;

	[SerializeField]
		List<GameObject> unitTypes = new List<GameObject>();

	List<Vector3> spawnLocation = new List<Vector3>();

	List<GameObject> unitsSpawned = new List<GameObject>();

	[SerializeField]
	 int maxCount;

	[SerializeField]
	 float spawnRate;

	[SerializeField]
	 Vector2 spawnDistance;

	[SerializeField]
	float despawnDistance;


	void Start()
	{

		spawnLocation.Add(new Vector3(gameObject.transform.position.x + spawnDistance.x, gameObject.transform.position.y, 0)); //Right (0)
		spawnLocation.Add(new Vector3(-(gameObject.transform.position.x + spawnDistance.x), gameObject.transform.position.y, 0)); //Left (1)
		spawnLocation.Add(new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z + spawnDistance.y));	//Top (2)
		spawnLocation.Add(new Vector3(0, gameObject.transform.position.y, -(gameObject.transform.position.z + spawnDistance.y))); //Bot (3)
	}


	void Update()
	{
		if(unitTypes.Count >0)
		{ 

		for(int i = 0; i < unitsSpawned.Count; i++)
			{
		    if(unitsSpawned[i] == null)
				{
					unitsSpawned.Remove(unitsSpawned[i]);
					i--;
			}
			else
				{ 
				if(Vector3.Distance(this.gameObject.transform.position, unitsSpawned[i].transform.position) > despawnDistance || !unitsSpawned[i].name.Contains(unitTypes[0].name) )
					{
						Destroy(unitsSpawned[i]);
						unitsSpawned.Remove(unitsSpawned[i]);
						i--;
				}

			}
		}

		if(unitsSpawned.Count < maxCount && !pauseSpawn)
		{
			int location = Random.Range(0,4);
			Vector3 spawn = spawnLocation[location];
			if(location == 0 || location == 1)
				{
					spawn.z = Random.Range(-(gameObject.transform.position.z + spawnDistance.y), gameObject.transform.position.z + spawnDistance.y);	
				}
			else if(location == 3 || location == 4)
				{
					spawn.x = Random.Range(-(gameObject.transform.position.x + spawnDistance.x), gameObject.transform.position.x + spawnDistance.x);	
				}

			unitsSpawned.Add((GameObject)Instantiate(unitTypes[0],spawn, new Quaternion(0,Random.rotation.y,0,Random.rotation.w)));

		}
		}
	}

	[ContextMenu("Invoke")]
	public void NextUnit()
	{
		unitTypes.RemoveAt(0);
	}
	
}