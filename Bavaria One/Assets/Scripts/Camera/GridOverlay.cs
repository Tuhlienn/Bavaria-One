using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOverlay : MonoBehaviour 
{
	public Material GridMaterial;
	public Material ConnectionMaterial;

	void OnPostRender () 
	{
		GL.PushMatrix();

		int width = GameManager.Instance.width;
		int height = GameManager.Instance.height;

		GridMaterial.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(Color.white);
		for(int i = -width / 2; i < width / 2; i++)
		{
			GL.Vertex(new Vector3(i, 0, -height / 2));
			GL.Vertex(new Vector3(i, 0, height / 2));
			GL.Vertex(new Vector3(-height / 2, 0, i));
			GL.Vertex(new Vector3(height / 2, 0, i));
		}
		GL.End();
		GL.PopMatrix();
	}
}
