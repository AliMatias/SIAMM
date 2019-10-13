// clase normal que sirve de una especie de DTO
public class TipsData
{
    private int id;
    private string temaRelacionado;
    private string temaTratado;
    private string descripcion;

    /*Get & Set version nueva para funcionalidades extra, la vieja forma de codificacion se modifico*/
    public int Id { get => id; set => id = value; }
    public string TemaRelacionado { get => temaRelacionado; set => temaRelacionado = value; }
    public string TemaTratado  { get => temaTratado; set => temaTratado = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    //constructor
    public TipsData(int id, string temaRelacionado, string temaTratado, string descripcion)
    {
        this.id = id;
        this.temaRelacionado = temaRelacionado;
        this.temaTratado = temaTratado;
        this.descripcion = descripcion; 
    }

    public new string ToString => "["+TemaRelacionado+ " - " +TemaTratado+"]: \n" + Descripcion +".";

}
