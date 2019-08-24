using UnityEngine;
using UnityEditor;

public struct OrbitalAndPeriodStruct
{
    private int period;
    private Orbital orbital;

    public int Period { get => period; }
    public Orbital Orbital { get => orbital; }

    public OrbitalAndPeriodStruct(int period, Orbital orbital)
    {
        this.period = period;
        this.orbital = orbital;
    }
}