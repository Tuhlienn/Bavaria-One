﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityList<T>{
	private List<KeyValuePair<T, float>> elements = new List<KeyValuePair<T, float>>();

	public int Count
	{
		get { return elements.Count; }
	}

	public void Enqueue(T item, float priority)
	{
		elements.Add(new KeyValuePair<T, float>(item, priority));
	}

	public T Dequeue()
	{
		int bestIndex = 0;

		for (int i = 0; i < elements.Count; i++) 
		{
			if (elements[i].Value < elements[bestIndex].Value) 
			{
				bestIndex = i;
			}
		}

		T bestItem = elements[bestIndex].Key;
		elements.RemoveAt(bestIndex);
		return bestItem;
	}

	public bool isEmpty()
	{
		return (Count == 0);
	}
}
