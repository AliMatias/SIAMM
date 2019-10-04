// clase normal que sirve de una especie de DTO
public class MoleculeData
{
    public int Id { get; set; }
    public string Formula { get; set; }
    public string SystematicNomenclature { get; set; }
    public string StockNomenclature { get; set; }
    public string TraditionalNomenclature { get; set; }
    public string Caracteristicas { get; set; }
    public string Propiedades { get; set; }
    public string Usos { get; set; }
    public string Clasificacion { get; set; }


    //constructor
    public MoleculeData()
    {
    }

    public MoleculeData(int Id, string Formula, string SystematicNomenclature, string StockNomenclature, string TraditionalNomenclature, string Caracteristicas, string Propiedades, string Usos, string Clasificacion)
    {
        this.Id = Id;
        this.Formula = Formula;
        this.SystematicNomenclature = SystematicNomenclature;
        this.StockNomenclature = StockNomenclature;
        this.TraditionalNomenclature = TraditionalNomenclature;
        this.Caracteristicas = Caracteristicas;
        this.Propiedades = Propiedades;
        this.Usos = Usos;
        this.Clasificacion = Clasificacion;
    }

    public new string ToString => "[Molecula]: " + TraditionalNomenclature + " (" + Formula + ").";

    public string ToStringToList => TraditionalNomenclature + " (" + Formula + ")";
}
