using System;
using UnityEngine;
using System.Reflection;

public struct Connection
{
    public bool connected;
    public bool isStammstrecke;
    public int upgradeLevel;

    public Connection(bool connected, bool isStammstrecke, int upgradeLevel){
        this.connected = connected;
        this.isStammstrecke = isStammstrecke;
        this.upgradeLevel = upgradeLevel;
    }

    public bool Equals ( Connection other){
        return      other.connected == this.connected && 
                    other.isStammstrecke == this.isStammstrecke && 
                    other.upgradeLevel == this.upgradeLevel; 
    }
}

