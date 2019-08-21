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
    private int lineType;

    public AtomInMolPositionData()
    {
    }

    public AtomInMolPositionData(int id, int moleculeId, int elementId, 
        float xPos, float yPos, float zPos, float scale, int connectedTo,
        int connectionType, int lineType)
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
        this.lineType = lineType;
    }

    public AtomInMolPositionData(AtomInMolPositionData atom)
    {
        this.id = atom.id;
        this.moleculeId = atom.moleculeId;
        this.elementId = atom.elementId;
        this.xPos = atom.xPos;
        this.yPos = atom.yPos;
        this.zPos = atom.zPos;
        this.scale = atom.scale;
        this.connectedTo = atom.connectedTo;
        this.connectionType = atom.connectionType;
        this.lineType = atom.lineType;
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
    public int LineType { get => lineType; set => lineType = value; }
}
