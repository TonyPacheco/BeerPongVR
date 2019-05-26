using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public AudioSource audio;
	
	Coroutine timer = null;
	
	void OnCollisionEnter(Collision col)
	{
		if(col.collider.gameObject.tag == "boundary")
		{
			PlayGameClip(Game.Sounds.bleep);
			Game.Get.RespawnBallInPlay(true);
		}		
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "cup")
		{
			Cup cup = col.gameObject.GetComponent<Cup>();
			timer = StartCoroutine(scoreCountdown(cup));
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag == "cup")
		{
			StopCoroutine(timer);
		}			
	}
	
	IEnumerator scoreCountdown(Cup cup)
	{
		yield return new WaitForSeconds(1.5f);
		Game.Get.Score(cup);
	}
	
	public void PlayGameClip(Game.Sounds clip)
	{
		audio.clip = Game.Get.sounds[(int)clip];
		audio.Play();
	}
}
