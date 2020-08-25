using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

public Transform PlayerPosition;
public GameObject[] Map = new GameObject[3];
private int SpawnedMap;
	// Use this for initialization
	void Start () {
		SpawnedMap = 3;

		
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPosition.position.z >= 50 * (SpawnedMap -2))
		{
			Vector3 NextSpawn = new Vector3(0,0,50 * SpawnedMap);
			Instantiate(Map[Random.Range(0,3)],NextSpawn,transform.rotation);
			SpawnedMap++;
		}
		
	}
}
