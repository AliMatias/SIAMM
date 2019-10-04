
public class MovementLimit
{
    private float superior;
    private float inferior;
    private float left;
    private float right;
    private float front;
    private float back;

    //constructor
    public MovementLimit()
    {
    }

    public MovementLimit(float superior, float inferior, float left, float right, float front, float back)
    {
        this.superior = superior;
        this.inferior = inferior;
        this.left = left;
        this.right = right;
        this.front = front;
        this.back = back;
    }

    public float Superior { get => superior; set => superior = value; }
    public float Inferior { get => inferior; set => inferior = value; }
    public float Left { get => left; set => left = value; }
    public float Right { get => right; set => right = value; }
    public float Front { get => front; set => front = value; }
    public float Back { get => back; set => back = value; }
}
