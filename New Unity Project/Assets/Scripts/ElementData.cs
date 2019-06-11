//clase normal que sirve de una especie de DTO
public class ElementData
{
    private string name;
    private string simbol;
    private int protons;
    private int neutrons;
    private int electrons;

    //public string Name { get => name; set => name = value; }
    //public int Protons { get => protons; set => protons = value; }
    //public int Neutrons { get => neutrons; set => neutrons = value; }
    //public int Electrons { get => electrons; set => electrons = value; }
    //public string Simbol { get => simbol; set => simbol = value; }

    #region Set & Get

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Simbol
    {
        get
        {
            return simbol;
        }

        set
        {
            simbol = value;
        }
    }

    public int Protons
    {
        get
        {
            return protons;
        }

        set
        {
            protons = value;
        }
    }

    public int Neutrons
    {
        get
        {
            return neutrons;
        }

        set
        {
            neutrons = value;
        }
    }

    public int Electrons
    {
        get
        {
            return electrons;
        }

        set
        {
            electrons = value;
        }
    }
    #endregion

    public new string ToString => Name + " (" + Simbol + ") : protones: " + Protons + ", neutrones: "
          + Neutrons + ", electrones: " + Electrons + ".";

}
