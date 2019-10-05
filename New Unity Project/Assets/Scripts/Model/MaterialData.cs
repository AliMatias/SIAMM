public class MaterialData{
    
    private int id;
    private string name;
    private string modelFile;
    private string clasificacion;
    private string caracteristicas;
    private string propiedades;
    private string usos;
    private string notas;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string ModelFile { get => modelFile; set => modelFile = value; }
    public string Clasificacion { get => clasificacion; set => clasificacion = value; }
    public string Caracteristicas { get => caracteristicas; set => caracteristicas = value; }
    public string Propiedades { get => propiedades; set => propiedades = value; }
    public string Usos { get => usos; set => usos = value; }
    public string Notas { get => notas; set => notas = value; }   

    public MaterialData(int id, string name, string modelFile, string clasificacion, string caracteristicas, string propiedades, string usos, string notas)
    {
        this.id = id;
        this.name = name;
        this.modelFile = modelFile;
        this.clasificacion = clasificacion;
        this.caracteristicas = caracteristicas;
        this.propiedades = propiedades;
        this.usos = usos;
        this.notas = notas;
    }
}