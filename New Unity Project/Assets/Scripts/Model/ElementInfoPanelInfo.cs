//DTO de info básica
public class ElementInfoPanelInfo
{
    private string clasificacion;
    private string clasificacion_grupo;
    private string numeros_oxidacion; 
    private string punto_fusion;
    private string punto_ebullicion;
    private string distribucion_de_electrones;

    //constructor
    public ElementInfoPanelInfo()
    {
    }

    public ElementInfoPanelInfo(string clasificacion, string clasificacion_grupo, string numeros_oxidacion, string punto_fusion, string punto_ebullicion, string distribucion_de_electrones)
    {
        this.clasificacion = clasificacion;
        this.clasificacion_grupo = clasificacion_grupo;
        this.numeros_oxidacion = numeros_oxidacion;
        this.punto_fusion = punto_fusion;
        this.punto_ebullicion = punto_ebullicion;
        this.distribucion_de_electrones = distribucion_de_electrones;

    }

    /*Get & Set version nueva para funcionalidades extra, la vieja forma de codificacion se modifico*/
    public string Clasificacion { get => clasificacion; set => clasificacion = value; }
    public string Clasificacion_grupo { get => clasificacion_grupo; set => clasificacion_grupo = value; } 
    public string NumerosOxidacion { get => numeros_oxidacion; set => numeros_oxidacion = value; } 
    public string PuntoFusion { get => punto_fusion; set => punto_fusion = value; } 
    public string PuntoEbullicion { get => punto_ebullicion; set => punto_ebullicion = value; }
    public string DistribucionDeelectrones { get => distribucion_de_electrones; set => distribucion_de_electrones = value; }
}