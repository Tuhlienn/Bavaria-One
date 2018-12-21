using System;
using UnityEngine;
using System.Reflection;

public class Connection : Ticking
{
    public bool connected;
    public bool isStammstrecke;
    public int upgradeLevel;
    public int vehicleCount;
    public Vector2 left, right;

    public Connection(bool isStammstrecke, int upgradeLevel, Vector2 left, Vector2 right)
    {
        this.connected = true;
        this.isStammstrecke = isStammstrecke;
        this.upgradeLevel = upgradeLevel;
        this.vehicleCount = 0;
        this.left = left;
        this.right = right;
    }

    public bool Equals(Connection other)
    {
        return other.connected == this.connected 
            && other.isStammstrecke == this.isStammstrecke 
            && other.upgradeLevel == this.upgradeLevel; 
    }
    
    override public void Tick()
    {
        int overload = vehicleCount - upgradeLevel;
        ResourceCount energyCost = new ResourceCount(0, 0, 0, 0, (overload <= 0 ? 1 : 1 + overload));
        GameManager.Instance.Resources -= energyCost;
    }

}

