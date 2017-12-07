using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class mouseManager : MonoBehaviour {

    // Use this for initialization
    public Map map;
    public Button button;
	void Start () {
    
		
	}
	
	// Update is called once per frame
	void Update () {

       if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.collider.transform.parent.gameObject;
            // Debug.Log("Raycast hit" + hitObject.name);
            MeshRenderer mr = hitObject.GetComponentInChildren<MeshRenderer>();
            Color oldColor = mr.material.color;

            if (hitObject.tag == "unit")
            {
                
                Debug.Log("UNIT");
                SelectUnit(hitObject);
               
               

            }
           

            if (hitObject.tag == "tile")
            {
               
                //Debug.Log("Raycast hit" + hitObject.name);
                TileAction(hitObject);
            }
           

            /*if(Input.GetMouseButton(0))
            {
                MeshRenderer mr = hitObject.GetComponentInChildren<MeshRenderer>();
               /* if(hitObject.tag == "tile")
                {
                    Debug.Log("It's a tile");
                }

                mr.material.color = Color.red;
            }*/
        }
    }

    private void TileAction(GameObject hitObject)
    {
        if (Input.GetMouseButton(0))
        {
            int x = hitObject.GetComponent<Hex>().tileX;
            int y = hitObject.GetComponent<Hex>().tileY;

            map.GeneratePathTo(x, y);
        }
    }

    void SelectUnit(GameObject hitObject)
    {
        if (Input.GetMouseButton(0))
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(hitObject.GetComponent<Unit>().NextTurn);
            map.selectedUnit = hitObject.gameObject;
        }

    }
}
