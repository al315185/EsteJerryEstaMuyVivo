using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour {

//	public GameObject currentSelected;
	public GameObject currentPanel;

	public GameObject UnitCanvas;

	void Start () {
		UnitCanvas.SetActive (false);
	}

	void Update () {

       if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.collider./*transform.parent.*/gameObject;
            Debug.Log("Raycast hit" + hitObject.name);

            if(Input.GetMouseButton(0))
            {
				//Si se trata de una unidad o de un edificio (del que podemos ver stats)
				if (hitObject.tag.Equals("Selectable")) {

					if (currentPanel != null) {
						currentPanel.SetActive (false);
					}

					if (!UnitCanvas.activeSelf) UnitCanvas.SetActive(true);

					Unit currentUnit = hitObject.GetComponent<Unit> ();
					currentPanel = currentUnit.Panel;
					currentPanel.SetActive (true);

					//scrollbar de vida

					if (currentUnit.UnitType.Equals (TypeOfUnit.WalkableUnit)) {
						currentPanel.transform.Find ("Icon").GetComponent<Image> ().sprite = currentUnit.Icon;
						currentPanel.transform.Find ("Kingdom").GetComponent<Text> ().text = currentUnit.Kingodm;
						currentPanel.transform.Find ("Damage").GetComponent<Text> ().text = currentUnit.Damage.ToString ();
					} else {
						currentPanel.transform.Find ("Icon").GetComponent<Image> ().sprite = currentUnit.Icon;
						currentPanel.transform.Find ("Kingdom").GetComponent<Text> ().text = currentUnit.Kingodm;
					}

				} else if (UnitCanvas.activeSelf) {
					UnitCanvas.SetActive (false);
					currentPanel.SetActive (false);
				}		

            }
        }
    }

}
