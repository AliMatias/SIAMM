public class MaterialMappingData{

    private int idMaterial;
    private int idElement;
    private int idMolecule;
    private int amount;

    //constructor
    public MaterialMappingData()
    {
    }

    public MaterialMappingData(int idMaterial, int idElement, int idMolecule, int amount)
    {
        this.idMaterial = idMaterial;
        this.idElement = idElement;
        this.idMolecule = idMolecule;
        this.amount = amount;
    }

    public int IdMaterial { get => idMaterial; set => idMaterial = value; }
    public int IdElement { get => idElement; set => idElement = value; }
    public int IdMolecule { get => idMolecule; set => idMolecule = value; }
    public int Amount { get => amount; set => amount = value; }

  
}