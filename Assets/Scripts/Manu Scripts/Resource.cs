// @author: M Gavilan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    public ResourceType resourceType;
    public int quantity;

    public Resource(ResourceType _resourceType, int _quantity = 0)
    {
        this.resourceType = _resourceType;
        this.quantity = _quantity;
    }


}

public enum ResourceType
{
    oxygen, enzyme
}
