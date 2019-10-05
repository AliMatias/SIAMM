using System.Collections.Generic;

public class MoleculeMappingData
{
    private int idMolecule;
    private int idElement;
    private int amount;

    public int IdMolecule { get => idMolecule; set => idMolecule = value; }
    public int IdElement { get => idElement; set => idElement = value; }
    public int Amount { get => amount; set => amount = value; }

    public MoleculeMappingData(int idMolecule, int idElement, int amount)
    {
        this.idMolecule = idMolecule;
        this.idElement = idElement;
        this.amount = amount;
    }
}