using System;

public struct ResourceCount {
    public int money;
    public int beer;
    public int steel;
    public int concrete;
    public int energy;

    public static ResourceCount operator * (int x, ResourceCount r) {
        r.money *= x;
        r.beer *= x;
        r.steel *= x;
        r.concrete *= x;
        r.energy *= x;
        return r;
    }

    public static ResourceCount operator +(ResourceCount l, ResourceCount r)
    {
        l.money += r.money;
        l.beer += r.beer;
        l.steel += r.steel;
        l.concrete += r.concrete;
        l.energy += r.energy;
        return l;
    }
}
