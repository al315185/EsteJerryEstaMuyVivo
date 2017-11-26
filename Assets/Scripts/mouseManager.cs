using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mouseManager : MonoBehaviour {

	// Use this for initialization
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
            Debug.Log("Raycast hit" + hitObject.name);

            if(Input.GetMouseButton(0))
            {
                MeshRenderer mr = hitObject.GetComponentInChildren<MeshRenderer>();
               /* if(hitObject.tag == "tile")
                {
                    Debug.Log("It's a tile");
                }*/

                mr.material.color = Color.red;
            }
        }
    }
}
