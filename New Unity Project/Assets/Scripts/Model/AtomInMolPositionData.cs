public class AtomInMolPositionData
{
    private int id;
    private int moleculeId;
    private int elementId;
    private float xPos;
    private float yPos;
    private float zPos;
    private float scale;
    private int connectedTo;
    private int connectionType;

    public AtomInMolPositionData()
    {
    }

    public AtomInMolPositionData(int id, int moleculeId, int elementId, 
        float xPos, float yPos, float zPos, float scale, int connectedTo,
        int connectionType)
    {
        this.id = id;
        this.moleculeId = moleculeId;
        this.elementId = elementId;
        this.xPos = xPos;
        this.yPos = yPos;
        this.zPos = zPos;
        this.scale = scale;
        this.connectedTo = connectedTo;
        this.connectionType = connectionType;
    }

    public int Id { get => id; set => id = value; }
    public int MoleculeId { get => moleculeId; set => moleculeId = value; }
    public int ElementId { get => elementId; set => elementId = value; }
    public float XPos { get => xPos; set => xPos = value; }
    public float YPos { get => yPos; set => yPos = value; }
    public float ZPos { get => zPos; set => zPos = value; }
    public float Scale { get => scale; set => scale = value; }
    public int ConnectedTo { get => connectedTo; set => connectedTo = value; }
    public int ConnectionType { get => connectionType; set => connectionType = value; }
}
