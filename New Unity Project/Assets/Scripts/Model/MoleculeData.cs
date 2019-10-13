// clase normal que sirve de una especie de DTO
using System;

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
    public Nullable<float> DiferenciaElectronegatividad { get; set; }


    //constructor
    public MoleculeData(int Id, string Formula, string SystematicNomenclature, string StockNomenclature, string TraditionalNomenclature, string Caracteristicas, string Propiedades, string Usos, string Clasificacion, Nullable<float> DiferenciaElectronegatividad)
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
        this.DiferenciaElectronegatividad = DiferenciaElectronegatividad != null ? DiferenciaElectronegatividad : 0;
    }

    public new string ToString => "[Molecula]: " + TraditionalNomenclature + " (" + Formula + ").";

    public string ToStringToList => TraditionalNomenclature + " (" + Formula + ")";
}
