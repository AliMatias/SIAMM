//clase normal que sirve de una especie de DTO
public class IsotopoData
{
    private int id;
    private string name;
    private int masa;
    private int estable;

    //constructor
    public IsotopoData()
    {
    }

    /*Get & Set version nueva para funcionalidades extra, la vieja forma de codificacion se modifico*/
    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int Masa { get => masa; set => masa = value; }
    public int Estable { get => estable; set => estable = value; }
}
