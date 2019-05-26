using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public bool playersCup;
	bool awaitingDrink = false;
	
	public void AwaitDrink()
	{
		awaitingDrink = true;
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(!awaitingDrink)
			return;
		
		if(col.gameObject.tag == "playershead")
		{
			Destroy(gameObject);
			Game.Get.OnCupDrank();
		}
	}
}
