using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	public static Game Get;
	
    public GameObject cupPrefab;
	public GameObject ballPrefab;
	
    public Transform[] playersSpawns;
    public Transform[] oponentsSpawns;
	public Transform playersBallSpawn;
	public Transform oponentsBallSpawn;
	
	private GameObject ballInPlay;
	
	public AudioClip[] sounds;
	public enum Sounds {
		bleep,
		horn
	}

	void Awake()
	{
		Get = this;
	}
	
    void Start()
    {
		StartGame();
    }
	
	public void StartGame()
	{
		foreach (var spawn in playersSpawns)
        {
            GameObject cup = Instantiate(cupPrefab, spawn);
            Cup script = cup.GetComponent<Cup>();
            script.playersCup = true;
        }
        foreach (var spawn in oponentsSpawns)
        {
            GameObject cup = Instantiate(cupPrefab, spawn);
            Cup script = cup.GetComponent<Cup>();
            script.playersCup = false;
        }
		SpawnNewBall(true);
	}
	
	public void SpawnNewBall(bool forPlayer)
	{
		ballInPlay = Instantiate(ballPrefab, forPlayer 
										   ? playersBallSpawn 
										   : oponentsBallSpawn);
	}

	public void RespawnBallInPlay(bool forPlayer)
	{
		ballInPlay.transform.parent = forPlayer 
									? playersBallSpawn 
							        : oponentsBallSpawn;
									
		ballInPlay.transform.localPosition = Vector3.zero;
		Rigidbody rb = ballInPlay.GetComponent<Rigidbody>();
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}
	
	public void Score(Cup cup)
	{
		ballInPlay.GetComponent<Ball>().PlayGameClip(Sounds.horn);
		if(cup.playersCup)
		{
			cup.AwaitDrink();
		}
		else
		{
			RespawnBallInPlay(true);
			Destroy(cup.gameObject);
		}
	}
	
	public void OnCupDrank()
	{
		RespawnBallInPlay(true);
	}
}
