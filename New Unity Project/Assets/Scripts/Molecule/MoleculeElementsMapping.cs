using System.Collections.Generic;

public class MoleculeElementsMapping
{
    private int idMolecule;
    private List<MoleculeElementAmountStruct> elements;

    public int IdMolecule { get => idMolecule; }
    public List<MoleculeElementAmountStruct> Elements { get => elements; }

    public MoleculeElementsMapping(int idMolecule, List<MoleculeElementAmountStruct> elements)
    {
        this.idMolecule = idMolecule;
        this.elements = elements;
    }

    public static List<MoleculeElementsMapping> FormatMoleculeMapping(List<MoleculeMappingData> mappings)
    {
        if (mappings.Count <= 0) return null;

        List<MoleculeElementsMapping> formattedMappings = new List<MoleculeElementsMapping>();
        int currentMolecule = 0;
        List<MoleculeElementAmountStruct> elementList = new List<MoleculeElementAmountStruct>();
        mappings.ForEach(mapping =>
        {
            if (mapping.IdMolecule != currentMolecule)
            {
                if (currentMolecule != 0)
                {
                    MoleculeElementsMapping newMappingStruct = new MoleculeElementsMapping(currentMolecule, elementList);
                    formattedMappings.Add(newMappingStruct);
                }
                currentMolecule = mapping.IdMolecule;
                elementList = new List<MoleculeElementAmountStruct>();
            }
            elementList.Add(new MoleculeElementAmountStruct(mapping.IdElement, mapping.Amount));
        });

        return formattedMappings;
    }

    public int totalElementsAmount()
    {
        int total = 0;
        elements.ForEach(element =>
        {
            total += element.Amount;
        });
        return total;
    }
}
