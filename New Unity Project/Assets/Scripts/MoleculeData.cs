// clase normal que sirve de una especie de DTO
public class MoleculeData
{
    public string Formula { get; set; }
    public string SystematicNomenclature { get; set; }
    public string StockNomenclature { get; set; }
    public string TraditionalNomenclature { get; set; }

    //constructor
    public MoleculeData(string Formula, string SystematicNomenclature, string StockNomenclature, string TraditionalNomenclature)
    {
        this.Formula = Formula;
        this.SystematicNomenclature = SystematicNomenclature;
        this.StockNomenclature = StockNomenclature;
        this.TraditionalNomenclature = TraditionalNomenclature;
    }

    public new string ToString => "[Molecula]: " + TraditionalNomenclature + " (" + Formula + ").";

}
