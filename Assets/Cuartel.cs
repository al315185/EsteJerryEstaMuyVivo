using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuartel : MonoBehaviour {

    [Tooltip("Coste de oxigeno para generar")]
    public int costeOxigeno;
    [Tooltip("Coste de enzimas para generar")]
    public int costeEnzimas;
    [Tooltip("Unidad que genera el cuartel")]
    public GameObject unidad;
    public AdaptedMap mapa;
    private Hex[,] hex;

    public Player owner;
    int tileX, tileY;

    public int[,] currentTile;

    private void Start()
    {
        tileX = this.gameObject.transform.parent.GetComponent<Hex>().tileX;
        tileY = this.gameObject.transform.parent.GetComponent<Hex>().tileY;
        //hex = mapa.map;

    }

    public void ConstruirUnidad()
    {
        if( owner.oxygen >= costeOxigeno && owner.enzymes >= costeEnzimas)
        {
            // TODO: Metodo en AdaptedMap que rellene datos del mapa sin generarlo.
            Transform spawnHex = hex[tileX + 1, tileY].transform;
            GameObject newUnit = Instantiate(unidad, spawnHex);
            float margin = newUnit.GetComponent<Renderer>().bounds.size.y / 2;
            Transform unitTransform = newUnit.transform;
            unitTransform.position = new Vector3(unitTransform.position.x, unitTransform.position.y + margin + 1, unitTransform.position.z);
            owner.Squad.Add(newUnit.GetComponent<Unidad>());
        }
    }

}
