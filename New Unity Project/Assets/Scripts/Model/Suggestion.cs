//Unión N a N entre elementos para sugerencias
public class Suggestion{
    private int id;
    private int idMolecula;
    private int idSugerido;
    
    //constructor
    public Suggestion()
    {
    }

    public Suggestion(int id, int idMolecula, int idSugerido)
    {
        this.id = id;
        this.idMolecula = idMolecula;
        this.idSugerido = idSugerido;
    }

    public int Id { get => id; set => id = value; } 
    public int IdMolecula { get => idMolecula; set => idMolecula = value; } 
    public int IdSugerido { get => idSugerido; set => idSugerido = value; } 
}