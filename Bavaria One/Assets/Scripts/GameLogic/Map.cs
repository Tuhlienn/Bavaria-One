public struct Map{
    
    public Tile[,] tiles;

    public Map(int width, int height) {
        tiles = new Tile[width,height];
    }
}
