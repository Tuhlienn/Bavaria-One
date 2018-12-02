using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour, ISelectHandler, IDeselectHandler {

    private ButtonManager bm;

    public void OnDeselect(BaseEventData eventData)
    {
        bm.jpopUpFixed = true;
    }

    public void OnSelect(BaseEventData eventData)
    {
        bm.jpopUpFixed = true;
    }

    // Use this for initialization
    void Start () {
        bm = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
