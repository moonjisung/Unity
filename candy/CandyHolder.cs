using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyHolder : MonoBehaviour {
	const int DefaultCandyAmount = 30;
	const int RecoverySeconds = 10;
	int counter;
	int candy = DefaultCandyAmount;
	public void ConsumeCandy()
	{
		if(candy > 0) candy--;
	}
	public int GetCandyAmount()
	{
		return candy;
	}
	public void AddCandy(int amount)
	{
		candy += amount;
	}
	void OnGUI()
	{
		GUI.color = Color.black;
		string label = "Candy : "+candy;
		if (counter>0) label = label+"("+counter + "s)";
		GUI.Label(new Rect(0,0,100,30),label);  
		}
		void Update () {
			if(candy < DefaultCandyAmount && counter <= 0)
			{
				StartCoroutine(RecoveryCandy());
			}
	}
	IEnumerator RecoveryCandy()
	{
		counter = RecoverySeconds;
		while (counter >0)
		{
			yield return new WaitForSeconds(1.0f);
			counter --;
		}
		candy++;
	}
}
