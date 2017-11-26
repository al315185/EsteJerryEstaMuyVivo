using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;
using UnityEngine.UI;

public class Unit : MonoBehaviour, ISelectable
{
	public TypeOfUnit unitType;
	public GameObject panel;

	//mientras el proyecto no esté ordenado
	public Sprite icon;
	public string kingdom;
	public float lifeSpawn;
	private float life;

	//para walkable
	public Material Material;
	public int range;
	public float damage;

	void Start ()
	{
		if (unitType.Equals (TypeOfUnit.WalkableUnit)) {
			GameObject model = transform.GetChild (0).gameObject;
			for (int i = 0; i < model.transform.childCount; i++) {
				model.transform.GetChild (i).GetComponent<Renderer> ().material = this.Material;
			}
		}

		life = lifeSpawn;
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

	public void Move ()
	{
	}

	public void WorkOn ()
	{
	}

	public void Attack (GameObject obj)
	{

	}
}

public enum TypeOfUnit
{
	WalkableUnit,
	Building
}
	
