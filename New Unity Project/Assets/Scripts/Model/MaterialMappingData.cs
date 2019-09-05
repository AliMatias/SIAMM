public class MaterialMappingData{

    private int id_material { get; set; }
    private int id_element { get; set; }
    private int id_molecule { get; set; }
    private int amount;

    public MaterialMappingData(int id_material, int id_element, int id_molecule, int amount)
    {
        this.id_material = id_material;
        this.id_element = id_element;
        this.id_molecule = id_molecule;
        this.amount = amount;
    }
}