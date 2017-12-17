//@author: M Gavilan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Player Jugador;
    public Player CPU;
    public Player ActivePlayer;

	public GameObject TurnCanvas;
	private Text PlayerText;


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
		ActivePlayer = Jugador;
		ActivePlayer.isMyTurn = true;
		//PlayerText = TurnCanvas.transform.GetChild (1).GetComponent<Text> ();
		//ShowCanvas ();
    }

	public void ChangeTurn(){
		if (ActivePlayer.Equals (Jugador)) {
			ActivePlayer = CPU;
			CPU.ResetUnits ();
		} else {
			ActivePlayer = Jugador;
			Jugador.ResetUnits ();
		}
	}

	public void CheckPlayerTurn(){
		if (ActivePlayer.isEndOfTurn ())
			ChangeTurn ();
	}

	public void ShowCanvas(){
		TurnCanvas.SetActive (true);
		PlayerText.text = ActivePlayer.name;
		StartCoroutine (WaitAndHide (4f));
	}

	public IEnumerator WaitAndHide(float nSeconds){
		yield return new WaitForSeconds (nSeconds);
		TurnCanvas.SetActive (false);
	}

}
