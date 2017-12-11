using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCellShaderData : MonoBehaviour {

//	Texture2D cellTexture;
//	Color32[] cellTextureData;
//
//	void Start () {
//		
//	}
//	
//	void Update () {
//		
//	}
//
//	// Each pixel of the texture will hold the data of one cell.
//	public void Initialize(int x, int z){
//		if (cellTexture) {
//			cellTexture.Resize (x, z);
//		} else {
//			cellTexture = new Texture2D (x, z, TextureFormat.RGBA32, false, true);
//			cellTexture.filterMode = FilterMode.Point;
//			cellTexture.wrapMode = TextureWrapMode.Clamp;
//		}
//
//		if (cellTextureData == null || cellTextureData.Length != x * z) {
//			cellTextureData = new Color32[x * z];
//		} else {
//			for (int i = 0; i < cellTextureData.Length; i++) {
//				cellTextureData [i] = new Color32 (0, 0, 0, 0);
//			}
//		}
//	}
//
//	public void RefreshVisibility(Hex cell){
//		cellTextureData [cell.Index].r = cell.IsVisible ? (byte)255 : (byte)0;
//		enabled = true;
//	}
}
