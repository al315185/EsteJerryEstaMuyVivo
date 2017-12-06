using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour {

    public ResourceType m_ResourceType;
    public int ResourceCapacity;
    private bool collectionEnable;
    // Lista de usuarios que están recogiendo recursos.
    private List<GameObject> ActiveCollectors = new List<GameObject>();

	public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Unit"))
        {
            ActiveCollectors.Add(col.gameObject);
            collectionEnable = true;
        };
    }

	public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Equals("Unit"))
        {
            ActiveCollectors.Remove(col.gameObject);
            if (ActiveCollectors.Count == 0)
            {
                collectionEnable = false;
            }
        };
    }
}