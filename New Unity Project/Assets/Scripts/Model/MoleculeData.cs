// clase normal que sirve de una especie de DTO
public class MoleculeData
{
    public int Id { get; set; }
    public string Formula { get; set; }
    public string SystematicNomenclature { get; set; }
    public string StockNomenclature { get; set; }
    public string TraditionalNomenclature { get; set; }

    //constructor
    public MoleculeData(int Id, string Formula, string SystematicNomenclature, string StockNomenclature, string TraditionalNomenclature)
    {
        this.Id = Id;
        this.Formula = Formula;
        this.SystematicNomenclature = SystematicNomenclature;
        this.StockNomenclature = StockNomenclature;
        this.TraditionalNomenclature = TraditionalNomenclature;
    }

    public new string ToString => "[Molecula]: " + TraditionalNomenclature + " (" + Formula + ").";

}
