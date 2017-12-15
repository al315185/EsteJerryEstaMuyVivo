using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GridPosition
	{
		public Vector3 worldPosition;
		public List<KeyValuePair<int, float>> influences;
		public List<GridPosition> neighbors;
		public Color32 myColor;
		public GameObject worldObject;

		public bool HasInfluenceOfType(int type){
			for (int i = 0; i < influences.Count; i++) {
			
				if (influences [i].Key == type) {
				
					return true;
				}
			}
			return false;
		}


		public KeyValuePair<int, float> GetInfluenceOfType(int type){
		
			for (int i = 0; i < influences.Count; i++) {
			
				if (influences [i].Key == type) {
					return new KeyValuePair<int, float> (i, influences [i].Value);
				}

			}
			return new KeyValuePair<int, float> (0, -1);
		}
	}
}

