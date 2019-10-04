//clase normal que sirve de una especie de DTO
public class IsotopoAllData
{
    private int id;
    private int numeroAtomico;
    private int numeroCorrelativo;
    private string isotopo;
    private int numeroMasa;
    private string masaAtomicaRelativa;
    private string composicionIsotopica;
    private string pesoAtomicoEstandar;

    /*Get & Set version nueva para funcionalidades extra, la vieja forma de codificacion se modifico*/
    public int Id { get => id; set => id = value; }
    public int NumeroAtomico { get => numeroAtomico; set => numeroAtomico = value; }
    public int NumeroCorrelativo { get => numeroCorrelativo; set => numeroCorrelativo = value; }
    public string Isotopo { get => isotopo; set => isotopo = value; }
    public int NumeroMasa { get => numeroMasa; set => numeroMasa = value; }
    public string MasaAtomicaRelativa { get => masaAtomicaRelativa; set => masaAtomicaRelativa = value; }
    public string ComposicionIsotopica {get => composicionIsotopica; set => composicionIsotopica = value; }
    public string PesoAtomicoEstandar { get => pesoAtomicoEstandar; set => pesoAtomicoEstandar = value; }

    }
