using System;
using UnityEngine;

public class OrbitBuilder
{
    private static Vector3 firstOrbitPosition = new Vector3(0.5f, 0f, 0f);

    // lo que aumenta el radio (x) segun cambie de orbita
    private static Vector3 orbitOffset = new Vector3(0.2f, 0f, 0f);

    public static Orbit BuildOrbit(int electronNumber, Atom atom, GameObject electron)
    {
        OrbitalAndPeriodStruct orbitalAndPeriod = Orbital.GetOrbitalAndPeriod(electronNumber);
        if(orbitalAndPeriod.Orbital != null)
        {

            Orbital orbital = orbitalAndPeriod.Orbital;
            int period = orbitalAndPeriod.Period;

            Orbit orbit = atom.GetOrbit(period);
            if (orbit == null)
            {
                orbit = CreateOrbit(atom, period);
                if (orbit == null)
                {
                    return null;
                }
            }

            ElectronSubshell electronSubshell = orbit.GetElectronSubshell(orbital.Name);
            if (electronSubshell == null)
            {
                electronSubshell = CreateElectronSubshell(orbital);
            }

            electronSubshell.AddElectron(electron);
            orbit.ElectronSubshells.Add(electronSubshell);

            return orbit;
        }
        return null;
    }

    private static Orbit CreateOrbit(Atom atom, int orbitNumber)
    {
        Vector3 orbitPosition = firstOrbitPosition + (orbitOffset * (orbitNumber - 1));
        return atom.SpawnOrbit(orbitNumber, orbitPosition);
    }

    private static ElectronSubshell CreateElectronSubshell(Orbital orbital)
    {
        return new ElectronSubshell(orbital.Name, orbital.MaxElectrons);
    }
}