using UnityEngine;
using System.Collections;

public struct MoleculeElementAmountStruct
{
    private int idElement;
    private int amount;

    public int IdElement { get => idElement; }
    public int Amount { get => amount; }

    public MoleculeElementAmountStruct(int idElement, int amount)
    {
        this.idElement = idElement;
        this.amount = amount;
    }
}
