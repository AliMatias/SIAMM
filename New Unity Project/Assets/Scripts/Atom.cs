using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

//script que se encarga de spawnear las partículas, manejarlas y saber que estoy formando.
public class Atom: MonoBehaviour
{
    #region atributos
    //Lista de prefabs de partículas, posición 0->proton, 1->neutron, 2->electron
    public GameObject[] particlePrefabs;
    //objeto padre, que va a representar el átomo en sí
    [SerializeField]
    private Transform parent;
    //objeto con el que interactúo para acceder a la DB
    private DBManager DBManager;
    private AtomManager atomManager;
    //label que indica elemento en construcción.
    public GameObject elementLabel;
    //listas para controlar las partículas agregadas
    private Queue<GameObject> protonQueue = new Queue<GameObject>();
    private Queue<GameObject> neutronQueue = new Queue<GameObject>();
    //contadores de partículas
    private int protonCounter = 0;
    private int neutronCounter = 0;
    private int electronCounter = 0;

    // indica si fue creado desde la tabla periodica o desde un boton
    private bool fromTabla = false;
    //indicador de índice de átomo (posición en la lista de átomos del manager)
    private int atomIndex;

    private bool allowElectronSpawn = true;

    private Vector3 firstOrbitPosition = new Vector3(0.5f, 0f, 0f);

    // lo que aumenta el radio (x) segun cambie de orbita
    private Vector3 orbitOffset = new Vector3(0.2f, 0f, 0f);

    private List<Orbit> orbits = new List<Orbit>();
    Orbit lastOrbit;
    #endregion

    public int AtomIndex { get => atomIndex; set => atomIndex = value; }
    public int ElementNumber { get; set; }

    //Seteo el dbmanager en el método awake, que se llama cuando se instancia el objeto
    private void Awake()
    {
        DBManager = FindObjectOfType<DBManager>();
        atomManager = FindObjectOfType<AtomManager>();
    }

    #region spawn
    //crea un nucleon, true -> crea proton, false -> crea neutron
    public void SpawnNucleon(bool proton, bool fromTabla)
    {
        int index = 1;
        if (proton)
        {
            index = 0;
        }
        //selecciono el prefab y lo instancio
        GameObject prefab = particlePrefabs[index];
        GameObject spawn = Instantiate<GameObject>(prefab, parent);
        
        //posicion random para que no queden todos en fila, aún no quedan bien
        float randomNumber = Random.Range(0f, 0.4f);
        Vector3 randomPosition = new Vector3(randomNumber, randomNumber, randomNumber);
        spawn.transform.localPosition = randomPosition;
        
        //encolar y aumentar contadores según partícula creada
        if (proton)
        {
            protonQueue.Enqueue(spawn);
            protonCounter++;
        }
        else
        {
            neutronQueue.Enqueue(spawn);
            neutronCounter++;
        }

        // indica si fue creado con el boton o desde la tabla
        this.fromTabla = fromTabla;

        //actualizar el label que indica el elemento.
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    //crear un electron
    public void SpawnElectron(bool fromTabla)
    {
        // tomo la ultima orbita
        lastOrbit = orbits.LastOrDefault();

        // si no hay o la ultima esta completa creo otra
        AddNewOrbitIfLastIsCompleted();

        if (allowElectronSpawn)
        {
            //selecciono el prefab y lo instancio
            GameObject prefab = particlePrefabs[2];
            GameObject spawn = Instantiate<GameObject>(prefab, parent);
            spawn.transform.localPosition = lastOrbit.Position;
            // agrego a la lista de electrones y aumento contador
            lastOrbit.AddElectron(spawn);
            electronCounter++;
        }
        // indica si fue creado con el boton o desde la tabla
        this.fromTabla = fromTabla;

        //actualizo label
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    /// <summary>
    /// Agrega una nueva orbita a la lista si es la primera o si la ultima ya esta completa
    /// </summary>
    private void AddNewOrbitIfLastIsCompleted()
    {
        // crear nueva orbita si la ultima esta completa o si es la primera
        if (orbits.Count == 0)
        {
            // crear primer orbita
            SpawnOrbit(1, firstOrbitPosition);
        }
        else
        {
            // verificar si se llego al maximo de electrones en la ultima orbita
            if (lastOrbit.isCompleted())
            {
                int newOrbitNumber = lastOrbit.Number + 1;
                Vector3 newOrbitPosition = firstOrbitPosition + (orbitOffset * (newOrbitNumber - 1));
                Orbit orbit = SpawnOrbit(newOrbitNumber, newOrbitPosition);
                if (orbit == null)
                {
                    // se completaron todas las orbitas
                    allowElectronSpawn = false;
                }
            }
        }
    }

    /// <summary>
    /// Crea una orbita correspondiente al numero recibido por parametro.
    /// </summary>
    /// <param name="number">Numero de orbita</param>
    /// <param name="position">Posicion de orbita</param>
    /// <returns>Nueva orbita | null</returns>
    public Orbit SpawnOrbit(int number, Vector3 position)
    {
        OrbitData orbitData = DBManager.GetOrbitDataByNumber(number);

        if (orbitData == null)
        {
            return null;
        }

        // spawn orbita
        GameObject circlePrefab = particlePrefabs[3];
        GameObject circleSpawn = Instantiate<GameObject>(circlePrefab, parent);
        circleSpawn.SendMessage("Create", position.x);

        // crea orbita y la agrega al atomo
        Orbit orbit = new Orbit(orbitData.Number, orbitData.Name, orbitData.MaxElectrons, position, circleSpawn);
        orbits.Add(orbit);
        lastOrbit = orbit;
        return orbit;
    }
    #endregion

    #region borradores

    //borrar neutron
    public void RemoveNeutron()
    {
        if (neutronCounter != 0)
        {
            GameObject toDelete = neutronQueue.Dequeue();
            Destroy(toDelete);
            neutronCounter--;
        }
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    //borrar proton
    public void RemoveProton()
    {
        if (protonCounter != 0)
        {
            GameObject toDelete = protonQueue.Dequeue();
            Destroy(toDelete);
            protonCounter--;
        }
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    //borrar electron
    public void RemoveElectron()
    {
        if (electronCounter > 0 && orbits.Count > 0)
        {
            GameObject toDelete = lastOrbit.ElectronList.LastOrDefault();
            if (toDelete != null)
            {
                Destroy(toDelete);
                lastOrbit.RemoveLastElectron();
                if(lastOrbit.ElectronList.Count == 0)
                {
                    // si la orbita se queda sin electrones la elimino y tomo la anterior como ultima
                    Destroy(lastOrbit.OrbitCircle);
                    orbits.Remove(lastOrbit);
                    lastOrbit = orbits.LastOrDefault();
                }
                electronCounter--;
                allowElectronSpawn = true;
            }
        }
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }
    #endregion

    /*Metodo Valida si es un elemento de tabla periodica, si es isotopo, y cation-anion
     y luego lo escribe en el label del elemento*/
    private void UpdateElement(int protons, int neutrons, int electrons)
    {
        ElementData element = new ElementData();
        ElementData elementIsotopo = new ElementData();
        string elementText = string.Empty;

        Debug.Log("protones: " + protons + " neutrones:" + neutrons + " electrones:" + electrons);

        //resetea valor a by default
        if (protons == 0 && neutrons == 0 && electrons == 0)
            elementText = "Vacío";
        else
        {
            //obtiene datos del elemento según cantidad de protones
            element = DBManager.GetElementFromProton(protons);
            //si es null o no lo encontró
            if (IsNullOrEmpty(element))
            {
                elementText = "Elemento no encontrado.";
                if (!fromTabla)
                {
                    ElementNumber = 0;
                }
            }
            else
            {
                //seteo nombre, símbolo y numero
                elementText = element.Name + " (" + element.Simbol + ")";
                if (!fromTabla)
                {
                    ElementNumber = element.Numero;
                }

                //si no coinciden los neutrones es un isótopo de ese material (falta límites inf y sup)
                if (element.Neutrons != neutrons)
                {
                    //valido que isotopo es sino existe se informa NO ENCONTRADO
                    elementIsotopo = DBManager.GetIsotopo(neutrons, element.Numero);

                    if (IsNullOrEmpty(elementIsotopo))
                    {
                        elementText = "no encontrado.";
                        if (!fromTabla)
                        {
                            ElementNumber = 0;
                        }
                    }
                    else
                    {
                        elementText = "isótopo (" + elementIsotopo.Name + ") de " + elementText;
                    }
                }
                //si mi modelo tiene mas electrones que el de la tabla, es anión (-)
                if (element.Electrons < electrons)
                {
                    elementText = elementText + ", anión.";
                }
                //sino, si el modelo tiene menos electrones que el de la tabla, es catión (+)
                else if (element.Electrons > electrons)
                {
                    elementText = elementText + ", catión.";
                }
                //sino, significa que es la misma cantidad, y tiene carga neutra
            }
        }

        Debug.Log(elementText);
        elementLabel.GetComponent<TextMesh>().text = elementText;
    }

    //se lanza cuando se hace click al átomo
    public void OnMouseDown()
    {
        Debug.Log("clickeaste el átomo " + AtomIndex);
        atomManager.SelectAtom(AtomIndex);
    }

    //ilumina todas las partículas
    public void Select(){
        elementLabel.GetComponent<TextMesh>().color = new Color(240,0,0);
        StartAllHighlights(protonQueue);
        StartAllHighlights(neutronQueue);
        //StartAllHighlights(electronQueue);
    }

    //ilumina las partículas de esta cola
    private void StartAllHighlights(Queue<GameObject> queue){
        foreach(GameObject obj in queue)
        {
            obj.GetComponent<HighlightObject>().StartHighlight();
        }
    }

    //quita la iluminación a todas las partículas
    public void Deselect(){
        elementLabel.GetComponent<TextMesh>().color = new Color(255,255,255);
        StopAllHighlights(neutronQueue);
        StopAllHighlights(protonQueue);
        //StopAllHighlights(electronQueue);
    }

    //quita la iluminación a todas las particulas de esta cola
    private void StopAllHighlights(Queue<GameObject> queue){
        foreach (GameObject obj in queue)
        {
            obj.GetComponent<HighlightObject>().StopHighlight();
        }
    }

    /*  cuando se destruye la instancia de este script, tengo que destruir
    *   manualmente el gameObject al cual está asignado este script
    */
    void OnDestroy()
    {
        Destroy(gameObject);
    }

    #region crear desde tabla periodica
    /*Crea tantas partículas como tiene el elemento indicado*/
    public void SpawnFromPeriodicTable(string elementName)
    {
        //nullcheck del nombre
        if (IsNullOrEmpty(elementName))
        {
            Debug.Log("Element name null or empty");
            return;
        }

        //obtengo la data del elemento de la DB
        ElementData element = DBManager.GetElementFromName(elementName);
        //nullcheck por si no encontró en la DB
        if (IsNullOrEmpty(element))
        {
            Debug.Log("Element not found.");
            return;
        }
        //crea la cantidad de partículas indicadas
        ElementNumber = element.Numero;
        fromTabla = true;
        IterateCounterAndCreateParticles(element.Protons, element.Neutrons, element.Electrons);
    }

    //Este método lanza las 3 co rutinas que spawnean las partículas indicadas por parámetro
    private void IterateCounterAndCreateParticles(int protons, int neutrons, int electrons)
    {
        //Empiezo las 3 co rutinas que se van a ejecutar en paralelo
        StartCoroutine(SpawnGivenNumberOfNucleons(protons, true));
        StartCoroutine(SpawnGivenNumberOfNucleons(neutrons, false));
        StartCoroutine(SpawnGivenNumberOfElectrons(electrons));
    }

    //Co rutina que spawnea N nucleones (true = proton, false = neutron), cada X segundos
    IEnumerator SpawnGivenNumberOfNucleons(int counter, bool proton)
    {
        while (counter > 0)
        {
            SpawnNucleon(proton, true);
            counter--;
            //esta línea espera x segundos antes de seguir ejecutando
            yield return new WaitForSeconds(0.25f);
        }
    }

    //Co rutina que spawnea N electrones cada X segundos
    IEnumerator SpawnGivenNumberOfElectrons(int counter)
    {
        while (counter > 0)
        {
            SpawnElectron(true);
            counter--;
            //esta línea espera x segundos antes de seguir ejecutando
            yield return new WaitForSeconds(0.5f);
        }
    }
    #endregion


    #region nullchecks
    //nullcheck de string, averiguar si existe alguna librería que ya haga esto.
    private bool IsNullOrEmpty(string s)
    {
        if (s == null || s == "")
            return true;
        return false;
    }

    //nullcheck de ElementData, averiguar si existe alguna librería que ya haga esto.
    private bool IsNullOrEmpty(ElementData e)
    {
        if (e == null || e.Name == null || e.Name == "")
            return true;
        return false;
    }
    #endregion
}
