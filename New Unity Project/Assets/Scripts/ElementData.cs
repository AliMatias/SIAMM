//clase normal que sirve de una especie de DTO
public class ElementData
{
    private string name;
    private string simbol;
    private int protons;
    private int neutrons;
    private int electrons;

    /*Get & Set version nueva para funcionalidades extra, la vieja forma de codificacion se modifico*/
    public string Name { get => name; set => name = value; }
    public int Protons { get => protons; set => protons = value; }
    public int Neutrons { get => neutrons; set => neutrons = value; }
    public int Electrons  { get => electrons; set => electrons = value; }
    public string Simbol { get => simbol; set => simbol = value; }

    public new string ToString => Name + " (" + Simbol + ") : protones: " + Protons + ", neutrones: "
          + Neutrons + ", electrones: " + Electrons + ".";

}
