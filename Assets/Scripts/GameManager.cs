//@author: M Gavilan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Player Player;
    public Player CPU;
    public Player ActivePlayer;

    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () {
        ActivePlayer = Player;
    }

	public void ChangeTurn(){
		if (ActivePlayer.Equals (Player)) {
			ActivePlayer = CPU;
			CPU.ResetUnits ();
		} else {
			ActivePlayer = Player;
			Player.ResetUnits ();
		}
	}

	public void CheckPlayerTurn(){
		if (ActivePlayer.isEndOfTurn ())
			ChangeTurn ();
	}

}
