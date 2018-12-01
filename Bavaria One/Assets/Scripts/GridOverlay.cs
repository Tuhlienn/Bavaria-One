using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOverlay : MonoBehaviour 
{
	public Material GridMaterial;
	public int gridCount = 10;
	public float gridSize = 1.0f;
	 
	void OnPostRender () 
	{
		GL.PushMatrix();
		GridMaterial.SetPass(0);
		GL.Begin(GL.LINES);		
		
		GL.Color(Color.white);

		Vector3 center = new Vector3((int)Mathf.Round(transform.position.x), 0, (int)Mathf.Round(transform.position.z));
		int lineCount = gridCount + 1;
		for (int i = -lineCount; i <= lineCount; i++) 
		{
			GL.Vertex(center + new Vector3(i * gridSize, 0, -lineCount * gridSize));
			GL.Vertex(center + new Vector3(i * gridSize, 0, lineCount * gridSize));
			
			GL.Vertex(center + new Vector3(-lineCount * gridSize, 0, i * gridSize));
			GL.Vertex(center + new Vector3(lineCount * gridSize, 0, i * gridSize));
		}
	
		GL.End();
		GL.PopMatrix();
	}
}
