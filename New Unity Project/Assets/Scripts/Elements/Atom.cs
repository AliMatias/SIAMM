using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System;

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
    private QryElementos qryElement;
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
    private bool fromTable = false;
    //indicador de índice de átomo (posición en la lista de átomos del manager)
    private int atomIndex;

    private const int PROTON_PREFAB_INDEX = 0;
    private const int NEUTRON_PREFAB_INDEX = 1;
    private const int ELECTRON_PREFAB_INDEX = 2;

    //control para aplicar spawn
    private bool allowElectronSpawn = true;
    private bool allowNeutronSpawn = true;
    private bool allowProtonSpawn = true;

    private Vector3 firstOrbitPosition = new Vector3(0.5f, 0f, 0f);

    // lo que aumenta el radio (x) segun cambie de orbita
    private Vector3 orbitOffset = new Vector3(0.2f, 0f, 0f);

    private List<Orbit> orbits = new List<Orbit>();

    private int elementNumber;

    //allcocate la clase popup para mostrar mensajes
    private UIPopup popup;
    //panel de info
    private MainInfoPanel mainInfoPanel;

    private TypeAtomEnum typeAtom;

    #endregion

    public int AtomIndex { get => atomIndex; set => atomIndex = value; }
    public int ElementNumber { get => elementNumber; set => elementNumber = value; }
    public int ProtonCounter { get => protonCounter; }
    public int NeutronCounter { get => neutronCounter; }
    public int ElectronCounter { get => electronCounter; }
    public TypeAtomEnum TypeAtom  { get => typeAtom; }

    //Seteo el dbmanager en el método awake, que se llama cuando se instancia el objeto
    private void Awake()
    {
        GameObject go = new GameObject();
        go.AddComponent<QryElementos>();
        qryElement = go.GetComponent<QryElementos>();

        popup = FindObjectOfType<UIPopup>();
        atomManager = FindObjectOfType<AtomManager>();
        mainInfoPanel = FindObjectOfType<MainInfoPanel>();
    }

    #region spawn
    //crea un nucleon, true -> crea proton, false -> crea neutron
    public void SpawnNucleon(bool proton, bool fromTable)
    {
        //encolar y aumentar contadores según partícula creada
        if (proton)
        {
            SpawnProton();
        }
        else
        {
            SpawnNeutron();
        }

        // indica si fue creado con el boton o desde la tabla
        this.fromTable = fromTable;

        //actualizar el label que indica el elemento.
        //Si es por tabla entonces NO! actulializo el label en runtime
        if (!fromTable)
            UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    //crea un nuevo neutron SI es que no se llego al limite
    private void SpawnNeutron()
    {
        if (allowNeutronSpawn)
        {
            //selecciono el prefab y lo instancio
            GameObject prefab = particlePrefabs[NEUTRON_PREFAB_INDEX];
            GameObject spawn = Instantiate<GameObject>(prefab, parent);

            //posicion random para que no queden todos en fila, aún no quedan bien
            float randomNumber = UnityEngine.Random.Range(0f, 0.4f);
            Vector3 randomPosition = new Vector3(randomNumber, randomNumber, randomNumber);
            spawn.transform.localPosition = randomPosition;

            neutronQueue.Enqueue(spawn);
            neutronCounter++;

            //controla si se llega al limite y NO DEJA AGREGAR MAS
            if (neutronCounter == 176)
            {
                allowNeutronSpawn = false;
                popup.MostrarPopUp("Atención!", "Se ha alcanzado la máxima cantidad válida de neutrones que puede tener un átomo. (176)");
            }
        }
    }

    //crea un nuevo proton SI es que no se llego al limite
    private void SpawnProton()
    {
        if (allowProtonSpawn)
        {
            //selecciono el prefab y lo instancio
            GameObject prefab = particlePrefabs[PROTON_PREFAB_INDEX];
            GameObject spawn = Instantiate<GameObject>(prefab, parent);

            //posicion random para que no queden todos en fila, aún no quedan bien
            float randomNumber = UnityEngine.Random.Range(0f, 0.4f);
            Vector3 randomPosition = new Vector3(randomNumber, randomNumber, randomNumber);
            spawn.transform.localPosition = randomPosition;

            protonQueue.Enqueue(spawn);
            protonCounter++;
            //controla si se llega al limite y NO DEJA AGREGAR MAS
            if (protonCounter == 118)
            {
                allowProtonSpawn = false;
                popup.MostrarPopUp("Atención!", "Se ha alcanzado la máxima cantidad válida de protones que puede tener un átomo. (118)");
            }
        }
    }

    //crear un electron
    public void SpawnElectron(bool fromTable)
    {
        if (allowElectronSpawn)
        {
            //selecciono el prefab y lo instancio
            GameObject prefab = particlePrefabs[ELECTRON_PREFAB_INDEX];
            GameObject spawn = Instantiate<GameObject>(prefab, parent);
            Orbit orbit = OrbitBuilder.BuildOrbit(electronCounter + 1, this, spawn);
            if (orbit == null)
            {
                Destroy(spawn);
                return;
            }
            spawn.transform.localPosition = orbit.Position;
            electronCounter++;
            if (electronCounter == 118)
            {
                popup.MostrarPopUp("Atención!", "Se alcanzó el límite máximo de electrones para elementos conocidos. (118)");
            }
            else if (electronCounter == 280)
            {
                allowElectronSpawn = false;
                popup.MostrarPopUp("Atención!", "Se alcanzó el límite máximo de electrones permitido. (280)");
            }
        }
        // indica si fue creado con el boton o desde la tabla
        this.fromTable = fromTable;

        //actualizo label
        //Si es por tabla entonces NO! actulializo el label en runtime
        if (!fromTable)
            UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    /// <summary>
    /// Crea una orbita correspondiente al numero recibido por parametro.
    /// </summary>
    /// <param name="number">Numero de orbita</param>
    /// <param name="position">Posicion de orbita</param>
    /// <returns>Nueva orbita | null</returns>
    public Orbit SpawnOrbit(int number, Vector3 position)
    {
        OrbitData orbitData = new OrbitData();
        try
        {
            orbitData = qryElement.GetOrbitDataByNumber(number);
        }
        catch (Exception e)
        {
            Debug.LogError("Atom Class :: Ocurrio un error al buscar Orbitas: " + e.Message);
            popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Orbitas");
        }

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
            allowNeutronSpawn = true;
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
            allowProtonSpawn = true;
        }
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    //borrar electron
    public void RemoveElectron()
    {
        if (electronCounter > 0 && orbits.Count > 0)
        {
            OrbitalAndPeriodStruct orbitalAndPeriod = Orbital.GetOrbitalAndPeriod(electronCounter);
            Orbit orbit = GetOrbit(orbitalAndPeriod.Period);
            ElectronSubshell electronSubshell = orbit.GetElectronSubshell(orbitalAndPeriod.Orbital.Name);

            GameObject toDelete = electronSubshell.RemoveLastElectron();
            Destroy(toDelete);

            if (orbitalAndPeriod.Orbital.Name.Equals(Orbital.GetOrbitalS.Name) && electronSubshell.ElectronList.Count == 0)
            {
                // si la orbita se queda sin electrones la elimino
                Destroy(orbit.OrbitCircle);
                orbits.Remove(orbit);
                Debug.Log("Cantidad de Orbitas:" + orbits.Count);
            }
            electronCounter--;
            allowElectronSpawn = true;
        }
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }
    #endregion

    #region MetodosVarios
    /*Metodo que escribe en el label del elemento de acuerdo al tipo*/
    private void UpdateElement(int protons, int neutrons, int electrons)
    {
        ElementData element = new ElementData();    
        string elementText = string.Empty;

        //resetea valor a by default
        if (protons == 0 && neutrons == 0 && electrons == 0)
            elementText = "Vacío";
        else
        {
            //obtiene datos del elemento según cantidad de protones
            try
            {
                element = qryElement.GetElementFromProton(protons);
            }
            catch (Exception e)
            {
                Debug.LogError("Atom Class :: Ocurrio un error al buscar Elementos-Protones: " + e.Message);
                popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Elementos-Protones");
                return;
            }

            elementText = checkElementType(element,protons, neutrons, electrons);
        }

        elementLabel.GetComponent<TextMesh>().text = elementText;

        mainInfoPanel.SetInfo(this);
    }

    /*Metodo Valida si es un elemento de tabla periodica, si es isotopo, y cation-anion
     y luego lo escribe en el label del elemento*/
    private string checkElementType (ElementData element, int protons, int neutrons, int electrons)
    {
        string elementText = string.Empty;
        IsotopoData elementIsotopo = new IsotopoData();
        typeAtom = TypeAtomEnum.atom; //por defecto casi siempre es un atomo estable!
        
        //si es null o no lo encontró
        if (IsNullOrEmpty(element))
        {
            elementText = "Elemento no encontrado.";
            if (!fromTable)
            {
                ElementNumber = 0;
            }
            typeAtom = TypeAtomEnum.noEncontrado;
        }
        else
        {
            //seteo nombre, símbolo y numero
            elementText = element.Name + " (" + element.Simbol + ")";
            if (!fromTable)
            {
                ElementNumber = element.Numero;
            }

            //si no coinciden los neutrones es un isótopo de ese material
            if (element.Neutrons != neutrons)
            {
                //valido que isotopo es sino existe se informa NO ENCONTRADO
                try
                {
                    elementIsotopo = qryElement.GetIsotopo(neutrons, element.Numero);
                    typeAtom = TypeAtomEnum.isotopo;
                }
                catch (Exception e)
                {
                    Debug.LogError("Atom class :: Ocurrio un error al buscar Isotopo: " + e.Message);
                    popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Isotopo");           
                }

                if (IsNullOrEmpty(elementIsotopo))
                {
                    elementText = "no encontrado.";
                    if (!fromTable)
                    {
                        ElementNumber = 0;
                    }
                    typeAtom = TypeAtomEnum.noEncontrado;
                }
                else
                {
                    //valida si el isotopo es estable o no y cambiara el label
                    if (elementIsotopo.Estable == 1)
                        elementText = "Isótopo (" + elementIsotopo.Name + ") de " + elementText + "\nMasa: " + elementIsotopo.Masa;
                    else
                        elementText = "Isótopo (" + elementIsotopo.Name + ") de " + elementText + "\nInestable" + "\nMasa: " + elementIsotopo.Masa;
                }
            }


            //si mi modelo tiene mas electrones que el de la tabla, es anión (-)
            if (element.Electrons < electrons)
            {
                if ((element.Electrons + element.MaxElectronsGana) >= electrons)
                {
                    elementText = elementText + ", anión.";
                }
                else
                {
                    elementText = "no encontrado.";
                    if (!fromTable)
                    {
                        ElementNumber = 0;
                    }
                    typeAtom = TypeAtomEnum.noEncontrado;
                }
            }
            //sino, si el modelo tiene menos electrones que el de la tabla, es catión (+)
            else if (element.Electrons > electrons)
            {
                if ((element.Electrons - element.MaxElectronsPierde) <= electrons)
                {
                    elementText = elementText + ", catión.";
                }
                else
                {
                    elementText = "no encontrado.";
                    if (!fromTable)
                    {
                        ElementNumber = 0;
                    }
                    typeAtom = TypeAtomEnum.noEncontrado;
                }
            }
            //sino, significa que es la misma cantidad, y tiene carga neutra
        }

        return elementText;
    }

    //se lanza cuando se hace click al átomo
    public void OnMouseDown()
    {
        //no va popup
        Debug.Log("clickeaste el átomo " + AtomIndex);
        atomManager.SelectAtom(AtomIndex);
    }

    //ilumina todas las partículas
    public void Select(){
        elementLabel.GetComponent<TextMesh>().color = new Color(240,0,0);
        StartAllHighlights(protonQueue);
        StartAllHighlights(neutronQueue);
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
        if(mainInfoPanel != null){
            //si se destruye el atomo y no hay otros seleccionados cierra panel
            mainInfoPanel.GetComponent<OpenMenus>().CloseBottomPanel();
        }
        Destroy(gameObject);
    }

    public Orbit GetOrbit(int number)
    {
        foreach (Orbit orbit in orbits)
        {
            if (orbit.Number == number)
            {
                return orbit;
            }
        }

        return null;
    }
 
    #endregion

    #region crear desde tabla periodica
    /*Crea tantas partículas como tiene el elemento indicado*/
    public void SpawnFromPeriodicTable(string elementName)
    {
        //nullcheck del nombre
        if (IsNullOrEmpty(elementName))
        {
            Debug.Log("Element name null or empty");
            popup.MostrarPopUp("Spawn Atomo Tabla Periodica","Nombre del Elemento no existe");
            throw (new SpawnException("Elemento no existente en la Base de Datos"));
        }

        //obtengo la data del elemento de la DB
        ElementData element = new ElementData();
        try
        {
            element = qryElement.GetElementFromName(elementName);
        }
        catch (Exception e)
        {
            Debug.LogError("Atom Class :: Ocurrio un error al buscar Elemento desde Nombre Simbolo: " + e.Message);
            popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Elemento desde Nombre Simbolo");
            return;
        }

        //nullcheck por si no encontró en la DB
        if (IsNullOrEmpty(element))
        {
            Debug.Log("Element not found.");
            popup.MostrarPopUp("Spawn Atomo Tabla Periodica", "Elemento no existente en la Base de Datos");
            throw (new SpawnException("Elemento no existente en la Base de Datos"));
            //return;
        }
        //crea la cantidad de partículas indicadas
        ElementNumber = element.Numero;
        fromTable = true;
        CreateAtomByParticlesAmount(element.Protons, element.Neutrons, element.Electrons);
        //ESCRIBE DIRECTO EL NOMBRE DEL ELEMENTO EN PANTALLA al ser spaw desde tabla periodica
        UpdateElement(element.Protons, element.Neutrons, element.Electrons);
    }

    public void SpawnFromSaveData(int protons, int neutrons, int electrons){
        fromTable = false;
        ElementNumber = protons;
        CreateAtomByParticlesAmount(protons, neutrons, electrons);
        UpdateElement(protons, neutrons, electrons);
    }

    //Este método lanza las 3 co rutinas que spawnean las partículas indicadas por parámetro
    private void CreateAtomByParticlesAmount(int protons, int neutrons, int electrons)
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


    //nullcheck de ElementData, averiguar si existe alguna librería que ya haga esto.
    private bool IsNullOrEmpty(IsotopoData e)
    {
        if (e == null || e.Name == null || e.Name == "")
            return true;
        return false;
    }
    #endregion
}
