[System.Serializable]
public struct ResourceCount {
    public float money;
    public float beer;
    public float steel;
    public float concrete;
    public float energy;

    public ResourceCount MultiResources
    {
        get
        { 
            ResourceCount result = new ResourceCount(0,0,0,0,0);
            if (this.money >= 2)
            {
                result.money = this.money;
            }
            if (this.beer >= 2)
            {
                result.beer = this.beer;
            }
            if (this.steel >= 2)
            {
                result.steel = this.steel;
            }
            if (this.concrete >= 2)
            {
                result.concrete = this.concrete;
            }
            if (this.energy >= 2)
            {
                result.money = this.energy;
            }
            return result;
        }
    }

    public ResourceCount(float money, float beer, float steel, float concrete, float energy)
    {
        this.money = money;
        this.beer = beer;
        this.steel = steel;
        this.concrete = concrete;
        this.energy = energy;
    }

    public static ResourceCount operator * (ResourceCount r, int x)
    {
        return x * r;
    }
    public static ResourceCount operator * (int x, ResourceCount r) {
        r.money *= x;
        r.beer *= x;
        r.steel *= x;
        r.concrete *= x;
        r.energy *= x;
        return r;
    }
    public static ResourceCount operator * (ResourceCount r, float x)
    {
        return x * r;
    }
    public static ResourceCount operator * (float x, ResourceCount r)
    {
        r.money *= x;
        r.beer *= x;
        r.steel *= x;
        r.concrete *= x;
        r.energy *= x;
        return r;
    }

    public static ResourceCount operator + (ResourceCount l, ResourceCount r)
    {
        l.money += r.money;
        l.beer += r.beer;
        l.steel += r.steel;
        l.concrete += r.concrete;
        l.energy += r.energy;
        return l;
    }

    public static ResourceCount operator - (ResourceCount l, ResourceCount r)
    {
        l.money = l.money >= r.money ? l.money - r.money : 0;
        l.beer = l.beer >= r.beer ? l.beer - r.beer : 0;
        l.steel = l.steel >= r.steel ? l.steel - r.steel : 0;
        l.concrete = l.concrete >= r.concrete ? l.concrete - r.concrete : 0;
        l.energy = l.energy >= r.energy ? l.energy - r.energy : 0;
        return l;
    }

    public static bool operator < (ResourceCount l, ResourceCount r)
    {
        return l.money < r.money 
            || l.beer < r.beer 
            || l.steel < r.steel 
            || l.concrete < r.concrete 
            || l.energy < r.energy;
    }

    public static bool operator > (ResourceCount l, ResourceCount r)
    {
        return l.money > r.money 
            && l.beer > r.beer 
            && l.steel > r.steel 
            && l.concrete > r.concrete 
            && l.energy > r.energy;
    }
}
