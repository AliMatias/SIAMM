public class MaterialData{
    
    private int id;
    private string name;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }

    public MaterialData(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}