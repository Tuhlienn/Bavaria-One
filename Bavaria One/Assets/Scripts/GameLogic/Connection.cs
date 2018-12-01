using System;
using UnityEngine;
using System.Reflection;

public class Connection : Ticking
{
    public bool connected;
    public bool isStammstrecke;
    public int upgradeLevel;
    public int vehicleCount;

    public Connection(bool connected, bool isStammstrecke, int upgradeLevel){
        this.connected = connected;
        this.isStammstrecke = isStammstrecke;
        this.upgradeLevel = upgradeLevel;
        this.vehicleCount = 0;
    }

    public bool Equals ( Connection other){
        return      other.connected == this.connected && 
                    other.isStammstrecke == this.isStammstrecke && 
                    other.upgradeLevel == this.upgradeLevel; 
    }
    override
    public void Tick()
    {
        ResourceCount EnergyCost;
        int Overload = this.vehicleCount - this.upgradeLevel * 10;
        if (Overload < 0)
        {
            EnergyCost = new ResourceCount(0, 0, 0, 0, -1);
        }
        else
        {
            EnergyCost = new ResourceCount(0, 0, 0, 0, -1 - Overload);
        }
        GameManager.Instance.Resources += EnergyCost;
    }
}

