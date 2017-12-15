using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	[System.Serializable]
	public class Originator
	{
		public Vector3 worldPosition;
		public float influence;
		public float influenceRange;
		public int type;
		public Color32 color;
	}
}

