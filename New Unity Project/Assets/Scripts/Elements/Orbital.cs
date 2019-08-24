using System.Collections;
using System.Collections.Generic;
using System;

public class Orbital
{
    private string name;
    private int maxElectrons;
    private ArrayList periods = new ArrayList();

    public string Name { get => name; }
    public int MaxElectrons { get => maxElectrons; }
    public ArrayList Periods { get => periods; }

    private Orbital(string name, int maxElectrons, ArrayList periodos)
    {
        this.name = name;
        this.maxElectrons = maxElectrons;
        this.periods = periodos;
    }

    public static Orbital GetOrbitalS => new Orbital("s", 2, new ArrayList(new int[] { 1, 2, 3, 4, 5, 6, 7 }));
    public static Orbital GetOrbitalP => new Orbital("p", 6, new ArrayList(new int[] { 2, 3, 4, 5, 6, 7 }));
    public static Orbital GetOrbitalD => new Orbital("d", 10, new ArrayList(new int[] { 3, 4, 5, 6, 7 }));
    public static Orbital GetOrbitalF => new Orbital("f", 14, new ArrayList(new int[] { 4, 5, 6, 7 }));
    public static Orbital GetOrbitalG => new Orbital("g", 18, new ArrayList(new int[] { 5, 6, 7 }));
    public static Orbital GetOrbitalH => new Orbital("h", 22, new ArrayList(new int[] { 6, 7 }));
    public static Orbital GetOrbitalI => new Orbital("i", 26, new ArrayList(new int[] { 7 }));

    public static List<Orbital> GetAllOrbitals => 
        new List<Orbital>(
            new Orbital[] 
            {
                GetOrbitalS,
                GetOrbitalP,
                GetOrbitalD,
                GetOrbitalF,
                GetOrbitalG,
                GetOrbitalH,
                GetOrbitalI
            });

    public static OrbitalAndPeriodStruct GetOrbitalAndPeriod(int electronNumber)
    {
        for (int i = 1; i <= 13; i++)
        {
            int nOrbital = Math.Min(i, 7);
            int period = Math.Max(i - 6, 1);

            while (nOrbital > 0)
            {
                Orbital orbital = Orbital.GetAllOrbitals[nOrbital - 1];
                if (orbital.Periods.Contains(period))
                {
                    if (electronNumber <= orbital.MaxElectrons)
                    {
                        return new OrbitalAndPeriodStruct(period, orbital);
                    }

                    electronNumber -= orbital.MaxElectrons;
                }

                nOrbital--;
                period++;
            }
        }
        return new OrbitalAndPeriodStruct(0, null); ;
    }
}
