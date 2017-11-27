using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fog : MonoBehaviour {

	// Crear para cada casilla del tablero un atributo (Sprite) que sea niebla.
	// Cuando una unidad se acerque a una casilla, ésta eliminará su propia niebla
	// y la de los vecinos (el script que detecta la colision con el jugador).

	void OnTriggerEnter(Collider c){

		if (c.tag.Equals ("Unit")) {

			// Buscamos las casillas de alrededor a ésta.
			// Eliminamos esas casillas y después ésta.
		}
	}

}