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


		//TODO Replace Connection appearance
		GL.PushMatrix();
		ConnectionMaterial.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(Color.white);
		for(int x = -width / 2; x < width / 2; x++)
		{
			for(int z = -height / 2; z < height / 2; z++) 
			{
				if(GameManager.Instance.Connections.ConnectionAt(new Vector2(x, z), new Vector2(x, z + 1)) != null)
				{
					GL.Vertex(new Vector3(x, 0, z));
					GL.Vertex(new Vector3(x, 0, z + 1));
				}
			}
		}
		for(int z = -height / 2; z < height / 2; z++) 
		{
			for(int x = -width / 2; x < width / 2; x++)
			{
				if(GameManager.Instance.Connections.ConnectionAt(new Vector2(x, z), new Vector2(x + 1, z)) != null)
				{
					GL.Vertex(new Vector3(x, 0, z));
					GL.Vertex(new Vector3(x + 1, 0, z));
				}
			}
		}
		GL.End();
		GL.PopMatrix();

		/*
		GL.PushMatrix();

		int width = GameManager.Instance.width;
		int height = GameManager.Instance.height;

		GridMaterial.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(Color.white);
		for(int x = -width / 2; x < width / 2; x++)
		{
			GL.Vertex(new Vector3(x, 0, -height / 2));
			GL.Vertex(new Vector3(x, 0, height / 2));
		}
		for(int z = -height / 2; z < height / 2; z++) 
		{
			GL.Vertex(new Vector3(-width / 2, 0, z));
			GL.Vertex(new Vector3(width / 2, 0, z));
		}
		GL.End();
		GL.PopMatrix();

		GL.PushMatrix();
		ConnectionMaterial.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(Color.white);
		for(int x = -width / 2; x < width / 2; x++)
		{
			for(int z = -height / 2; z < height / 2; z++) 
			{
				if(GameManager.Instance.Connections.ConnectionAt(new Vector2(x, z), new Vector2(x, z + 1)) != null)
				{
					GL.Vertex(new Vector3(x, 0, z));
					GL.Vertex(new Vector3(x, 0, z + 1));
				}
			}
		}
		for(int z = -height / 2; z < height / 2; z++) 
		{
			for(int x = -width / 2; x < width / 2; x++)
			{
				if(GameManager.Instance.Connections.ConnectionAt(new Vector2(x, z), new Vector2(x + 1, z)) != null)
				{
					GL.Vertex(new Vector3(x, 0, z));
					GL.Vertex(new Vector3(x + 1, 0, z));
				}
			}
		}
		GL.End();
		GL.PopMatrix();

		GL.PushMatrix();
		ConnectionMaterial.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(Color.white);
		for(int x = -width / 2; x < width / 2; x++)
		{
			for(int z = -height / 2; z < height / 2; z++) 
			{
				var connection = GameManager.Instance.Connections.ConnectionAt(new Vector2(x, z), new Vector2(x, z + 1));
				if(connection != null && connection.isStammstrecke)
				{
					GL.Vertex(new Vector3(x, 0, z));
					GL.Vertex(new Vector3(x, 0, z + 1));
				}
			}
		}
		for(int z = -height / 2; z < height / 2; z++) 
		{
			for(int x = -width / 2; x < width / 2; x++)
			{
				var connection = GameManager.Instance.Connections.ConnectionAt(new Vector2(x, z), new Vector2(x + 1, z));
				if(connection != null && connection.isStammstrecke)
				{
					GL.Vertex(new Vector3(x, 0, z));
					GL.Vertex(new Vector3(x + 1, 0, z));
				}
			}
		}
		GL.End();
		GL.PopMatrix();
		*/
	}
}
