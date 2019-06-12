//DTO de info básica
public class ElementInfoBasic 
{
    private int nroatomico;
    private string simbol;
    private string name;
    private float pesoAtomico;
    private int periodo;
    private string fase;
    private string estructura_cristalina;
    private string color;
    private int valencia;
    private string numeros_oxidacion; 
    //configuracion_electronica_abreviada; 
    private string confElectronica;
    private string caracteristicas;
    private string punto_fusion;
    private string punto_ebullicion;
    private string resumen;

    /*Get & Set version nueva para funcionalidades extra, la vieja forma de codificacion se modifico*/
    public int Nroatomico { get => nroatomico; set => nroatomico = value; } 
    public string Simbol    { get => simbol; set => simbol = value; } 
    public string Name    { get => name; set => name = value; } 
    public float PesoAtomico    { get => pesoAtomico; set => pesoAtomico = value; } 
    public int Periodo    { get => periodo; set => periodo = value; } 
    public string Fase { get => fase; set => fase = value; } 
    public string EstructuraCristalina { get => estructura_cristalina; set => estructura_cristalina = value; } 
    public string Color { get => color; set => color = value; } 
    public int Valencia { get => valencia; set => valencia = value; } 
    public string NumerosOxidacion { get => numeros_oxidacion; set => numeros_oxidacion = value; } 
    public string ConfElectronica { get => confElectronica; set => confElectronica = value; } 
    public string Caracteristicas { get => caracteristicas; set => caracteristicas = value; } 
    public string PuntoFusion { get => punto_fusion; set => punto_fusion = value; } 
    public string PuntoEbullicion { get => punto_ebullicion; set => punto_ebullicion = value; } 
    public string Resumen { get => resumen; set => resumen = value; } 

}