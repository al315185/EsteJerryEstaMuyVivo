﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unidad : MonoBehaviour
{
	public SelectionManager selectionManager;

	public TypeOfUnit unitType;
	public GameObject panel;
    public Player Owner;

	public bool finished;

    [Header("UI")]
	//mientras el proyecto no esté ordenado
	public Sprite icon;
	public string kingdom;
	public float lifeSpawn;
	private float life;

	//para walkable
	public Material Material;
	public int range;
	public float damage;

	Pathfinding pathfinding;


	void Awake(){
		selectionManager = GameObject.Find ("Selection Manager").GetComponent<SelectionManager> ();
		pathfinding = GetComponent<Pathfinding> ();
	}

	void Start ()
	{
		if (unitType.Equals (TypeOfUnit.WalkableUnit)) {
			GameObject model = transform.GetChild (0).gameObject;
			for (int i = 0; i < model.transform.childCount; i++) {
				model.transform.GetChild (i).GetComponent<Renderer> ().material = this.Material;
			}
		}

		life = lifeSpawn;
		finished = false;
	}

	void Update(){
		

	}

	public bool Finished {
		get{ return finished; }
		set{ finished = value; }
	}

	public float Damage {
		get{ return damage; }
		set{ damage = value; }
	}

	public string Kingodm {
		get{ return kingdom; }
		set{ kingdom = value; }
	}

	public TypeOfUnit UnitType{
		get{ return unitType; }
		set{ unitType = value; }
	}

	public GameObject Panel{
		get{ return panel; }
		set{ panel = value; }
	}

	public Sprite Icon{
		get{ return icon; }
		set{ icon = value; }
	}

	public float LifeSpawn{
		get{ return lifeSpawn; }
		set{ lifeSpawn = value; }
	}

	//Walkable Functions!
	public void DoAttack(Unidad unit){
		//quitar daño de unit con damage
	}

	public void DoMove(GameObject hitObject){
		pathfinding.SetSelected (gameObject);
		pathfinding.TileAction (hitObject);
		pathfinding.NextTurn ();
	}

	public void DoWork(GameObject building, Transform parent, Player currentPlayer){
        GameObject newObject = Instantiate(building, parent);
        newObject.transform.parent = parent;
        if(newObject.GetComponent<Cuartel>()) newObject.GetComponent<Cuartel>().owner = currentPlayer;
        // TODO else torre
        //float margin = newObject.GetComponent<Renderer>().bounds.size.y / 2;
        //Transform unitTransform = newObject.transform;
        //unitTransform.position = new Vector3(unitTransform.position.x, unitTransform.position.y + margin + 1, unitTransform.position.z);
    }

    public void DoWork(ResourceType _type)
    {
        switch (_type)
        {
            case ResourceType.enzyme:
                //TODO
                break;
            case ResourceType.oxygen:
                //TODO
                break;
        }
    }
}

public enum TypeOfUnit
{
	WalkableUnit,
	Building,
    Turret
}

public enum TypeOfAction{
	Attack,
	Move,
	WorkOn,
	None
}

public enum TypeOfBuilding
{
    Tower,
    Barracks
}


	
