// @author: M Gavilan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    public Resource oxygen;
    public Resource enzymes;
    public bool isMyTurn;

    public List<Unit> Squad = new List<Unit>();

    private void Start()
    {
        //Inicializamos los recursos a 0
        oxygen = new Resource(ResourceType.oxygen);
        enzymes = new Resource(ResourceType.enzyme);
    }

    public void EnlistUnit(Unit u)
    {
        Squad.Add(u);
    }

	public void DeleteUnit(Unit u){
		Squad.Remove (u);
	}

    public void AddResources(ResourceType r, int i)
    {
        switch (r)
        {
            case ResourceType.enzyme:
                enzymes.quantity += i;
                break;
            case ResourceType.oxygen:
                oxygen.quantity += i;
                break;
        }
    }

	public void ResetUnits(){
		foreach (Unit u in Squad) {
			u.finished = false;
		}
	}

	public bool isEndOfTurn(){
		foreach (Unit u in Squad) {
			if (!u.finished) {
				isMyTurn = false;
				return false;
			}
		}

		return true;
	}
}
