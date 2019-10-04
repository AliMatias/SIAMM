public class MaterialData
{    
    private int id;
    private string name;
    private string modelFile;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string ModelFile { get => modelFile; set => modelFile = value; }

    public MaterialData(int id, string name, string modelFile)
    {
        this.id = id;
        this.name = name;
        this.modelFile = modelFile;
    }

    public new string ToString => "[Material]: " + name;

    public string ToStringToList => name + " (Material)";
}