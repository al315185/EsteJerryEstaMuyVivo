using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {

	public GameObject UnitCanvas;
	public GameObject currentSelected;
	public TypeOfAction currentAction;
    public GameObject currentBuilding;

    public ResourceType currentResource;

    public GameObject Barracks;
    public GameObject Tower;

    public TypeOfAction CurrentAction{
		get{ return currentAction; }
		set{ currentAction = value; }
	}

	public GameObject CurrentSelected{
		get{ return currentSelected; }
		set{ currentSelected = value; }
	}

	void Start () {
		UnitCanvas.SetActive (false);
	}


	public Map map;

	void Update () {

//		if (!currentAction.Equals(TypeOfAction.None)){}

       if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            if(Input.GetMouseButton(0))
            {
				Manage (hitObject);
            }
        }
    }

    public void setCurrentBuilding(GameObject building)
    {
        this.currentBuilding = building;
    }

	private void SwitchPanel(Unidad unit){

		unit.Panel.gameObject.SetActive (true);

        if (unit.Panel.name.Equals("Worker Panel"))
        {
            UnitCanvas.transform.GetChild(1).gameObject.SetActive(false);
            UnitCanvas.transform.GetChild(2).gameObject.SetActive(false);
            UnitCanvas.transform.GetChild(3).gameObject.SetActive(false);

        }
        else if (unit.Panel.name.Equals("Soldier Panel"))
        {
            UnitCanvas.transform.GetChild(0).gameObject.SetActive(false);
            UnitCanvas.transform.GetChild(2).gameObject.SetActive(false);
            UnitCanvas.transform.GetChild(3).gameObject.SetActive(false);

        }
        else if (unit.Panel.name.Equals("Building Panel"))
        {
            UnitCanvas.transform.GetChild(0).gameObject.SetActive(false);
            UnitCanvas.transform.GetChild(1).gameObject.SetActive(false);
            UnitCanvas.transform.GetChild(3).gameObject.SetActive(false);
        }
        else {
            UnitCanvas.transform.GetChild(0).gameObject.SetActive(false);
            UnitCanvas.transform.GetChild(1).gameObject.SetActive(false);
            UnitCanvas.transform.GetChild(2).gameObject.SetActive(false);
        }
	}

	private void ActualizePanel(Unidad unit){
		
		SwitchPanel (unit);

		if (unit.UnitType.Equals (TypeOfUnit.WalkableUnit) || unit.UnitType.Equals(TypeOfUnit.Turret)) {			
			unit.Panel.transform.Find ("Icon").GetComponent<Image> ().sprite = unit.Icon;
			unit.Panel.transform.Find ("Kingdom").GetComponent<Text> ().text = unit.Kingodm;
			unit.Panel.transform.Find ("Damage").GetComponent<Text> ().text = unit.Damage.ToString ();
		} 
		else {
			unit.Panel.transform.Find ("Icon").GetComponent<Image> ().sprite = unit.Icon;
			unit.Panel.transform.Find ("Kingdom").GetComponent<Text> ().text = unit.Kingodm;
		}
	}

	public void Manage(GameObject objective){

		//Si no hay nada seleccionado previamente a este click
		if (currentSelected == null) 
		{
			//las unidades son edificios obreros soldados
			if (objective.tag.Equals ("Unit")) {
				currentSelected = objective;

				//si no habia nada seleccionado el Canvas se encuentra desactivado
				//por lo que se activa y se actualiza el panel para la unidad actual
				UnitCanvas.SetActive (true);
				ActualizePanel (objective.GetComponent<Unidad> ());
			}	
		} 

		//Si ya tenemos una unidad seleccionada 
		else 
		{
			Unidad unitActor = currentSelected.GetComponent<Unidad> ();

			//Si lo que se ha seleccionado ahora es una unidad
			if (objective.tag.Equals ("Unit")) {

				Unidad unitReceptor = objective.GetComponent<Unidad> ();

				// ¿es una unidad aliada o una unidad enemiga?
				if (unitActor.Kingodm.Equals (unitReceptor.Kingodm)) {

					//Si son aliados, se cambiará la unidad actual por
					//la última seleccionada, y se ignorará cualquier
					//acción que estuviese realizando la anterior
					currentSelected = unitReceptor.gameObject;
					currentAction = TypeOfAction.None;

					//Por último actualizamos el canvas, para mostrar
					//la información de la nueva unidad clickada
					ActualizePanel (unitReceptor);
				} else {

					//Si es un enemigo lo clickado, verificamos que lo anterior sea
					//un obrero o soldaod pues lo unico que podemos hacer es atacar
					if (unitActor.UnitType.Equals (TypeOfUnit.WalkableUnit) || unitActor.UnitType.Equals(TypeOfUnit.Turret)) {
						if (currentAction.Equals (TypeOfAction.Attack)) {
							unitActor.DoAttack (unitReceptor);
							unitActor.Finished = true;
						}

						//tanto como si hemos atacado, como si la acción seleccionada
						//era moverse o trabajar sobre (cosa que no puede hacerse en una
						//unidad enemiga), eliminamos toda acción
						currentAction = TypeOfAction.None;
					}

				}
			}

			//Si lo que se ha seleccionado NO es una unidad (o suelo o recurso)
			else {

				//Si la unidad anteriormente seleccionada es obrera o soldado
				if (unitActor.UnitType.Equals (TypeOfUnit.WalkableUnit)) {

					if (objective.tag.Equals ("Suelo")) {

                        switch (currentAction)
                        {
                            case TypeOfAction.Move:
                                unitActor.DoMove(objective);
                                currentSelected = null;
                                UnitCanvas.SetActive(false);
                                unitActor.Finished = true;
                                break;
                            case TypeOfAction.WorkOn:
                                unitActor.DoWork(currentBuilding, objective.transform.parent.transform, GameManager.Instance.ActivePlayer);
                                break;
                            case TypeOfAction.Attack:
                                break;
                            default:
                                currentSelected = null;
                                UnitCanvas.SetActive(false);
                                break;

                        }
					} 
					else if (objective.tag.Equals ("Recursos")) {
						if (currentAction.Equals (TypeOfAction.WorkOn)) {
							unitActor.DoWork (currentResource);
							unitActor.Finished = true;

						} else {
							currentSelected = null;
							UnitCanvas.SetActive (false);
						}
					}

					//tanto como si acierta en la ejecución de la acción, como si selecciona algo
					//que no se empareja con su acción, eliminamos la acción actual
					currentAction = TypeOfAction.None;
				} 
				else {
					//si es edificio, quitamos la selección
					currentSelected = null;
					UnitCanvas.SetActive(false);
				}
			}
		}
	}

	//Walkable Functions Button!
	public void Move ()
	{
		currentAction = TypeOfAction.Move;
	}

	public void WorkOn ()
	{
		currentAction = TypeOfAction.WorkOn;
	}

	public void Attack ()
	{
		currentAction = TypeOfAction.Attack;
	}


}
