using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    const int sphereCandyFrequency = 3;
    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;
    int shotPower = MaxShotPower;
    int sampleCandyCount;
    AudioSource shotSound;
    public GameObject[] candyPrefabs;
    public GameObject[] candySquarePrefabs;
    public float shotspeed;
    public float shotTorque;
    public float baseWidth;
    public CandyHolder candyHolder;

	// Use this for initialization
	void Start () {
		shotSound = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shot();
    }
    GameObject SampleCandy ()
        {
            GameObject prefab = null;
            if (sampleCandyCount % sphereCandyFrequency == 0)
            {
                int index = Random.Range(0, candyPrefabs.Length);
                prefab = candyPrefabs[index];
            }
            else
            {
                int index = Random.Range(0, candySquarePrefabs.Length);
                prefab = candySquarePrefabs[index];
            }
            sampleCandyCount++;
            return prefab;
		}
    Vector3 GetInstantiatePosition()
    {
        float x = baseWidth * (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }
    public void Shot()
    {
        if(candyHolder.GetCandyAmount()<=0) return; 
        if(shotPower <=0) return;
        GameObject candy = (GameObject)Instantiate(SampleCandy(), GetInstantiatePosition(), Quaternion.identity);
        candy.transform.parent = candyHolder.transform;
        Rigidbody candyRigidbody = candy.GetComponent<Rigidbody>();
        candyRigidbody.AddForce(transform.forward * shotspeed);
        candyRigidbody.AddTorque(new Vector3(0, shotTorque, 0));
        candyHolder.ConsumeCandy();
        ConsumePower();
        shotSound.Play();
    }
    void OnGUI()
    {
        GUI.color = Color.black;
        string label = "";
        for (int i = 0; i < shotPower; i++) label = label + "+";
        GUI.Label(new Rect(0,15,100,30),label);
    }
    void ConsumePower()
    {
        shotPower--;
        StartCoroutine(RecoverPower());
    }
    IEnumerator RecoverPower()
    {
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;

    }
}
