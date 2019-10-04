//clase normal que sirve de una especie de DTO
public class OrbitData
{
    public int Number { get; set; }
    public string Name { get; set; }
    public int MaxElectrons { get; set; }

    //constructor
    public OrbitData()
    {
        Number = 1;
        Name = "L";
        MaxElectrons = 2;
    }

    public OrbitData(int Number, string Name, int MaxElectrons)
    {
        this.Number = Number;
        this.Name = Name;
        this.MaxElectrons = MaxElectrons;
    }

    public new string ToString => "Orbita " + Name + " (" + Number + ") -> Max. Electrones: " + MaxElectrons + ".";

}
