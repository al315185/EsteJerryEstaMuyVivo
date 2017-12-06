using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionArea : MonoBehaviour {

    [Tooltip("Qué tipo de recurso ofrece.")]
    public ResourceType areaResource;
    [Tooltip("Cantidad de recurso que ofrece por turno.")]
    public int quantityOfResource;
    //Lista de unidades que se encuentran en este momento en el área de recogida.
    [SerializeField]
    [Tooltip("Lista de unidades que se encuentran en el recurso.")]
    private List<Unidad> unitList = new List<Unidad>();


    public void addResourcesToUnit(Unidad u)
    {
        u.Owner.AddResources(areaResource, quantityOfResource);
		u.finished = true;
		GameManager.Instance.CheckPlayerTurn();
    }

}
