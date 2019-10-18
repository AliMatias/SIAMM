using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEditor;

//Esta clase se va a encargar de desactivar y activar los diferentes elementos de la UI cuando corresponda
public class CombinationManager : MonoBehaviour
{

    #region Atributos
    //lista de botones que van a ser apagados cuando se entre en el modo combinación
    private List<Button> buttonsToToggle = new List<Button>();
    private bool combineMode = false;
    private AtomManager atomManager;
    private QryMoleculas qryMolecule;
    private QryMaterials qryMaterial;
    private QryElementos qryElementos;
    private MoleculeManager moleculeManager;
    private SelectionManager selectionManager;
    private MaterialManager materialManager;
    private TipsManager tipsManager;
    public Button combineButton;
    public Text combineModeButton;
    private UIPopup popup;

    //prefab de animacion para combinacion y parent donde se asignara el GObj
    public GameObject animationPrefab;
    //contenedor para instanciar animacion
    private GameObject animationCombination;

    public bool CombineMode { get => combineMode; }

    //panel de info
    private MainInfoPanel mainInfoPanel;
    private GameObject buttonInfoPanel;

    #endregion

    void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        materialManager = FindObjectOfType<MaterialManager>();
        popup = FindObjectOfType<UIPopup>();
        tipsManager = FindObjectOfType<TipsManager>();

        //instancio en el momento la clase que contiene las querys, seria lo mismo que hacer class algo = new class();
        GameObject go = new GameObject();
        go.AddComponent<QryMoleculas>();
        go.AddComponent<QryMaterials>();
        go.AddComponent<QryElementos>();
        qryMolecule = go.GetComponent<QryMoleculas>();
        qryMaterial = go.GetComponent<QryMaterials>();
        qryElementos = go.GetComponent<QryElementos>();

        moleculeManager = FindObjectOfType<MoleculeManager>();
        selectionManager = FindObjectOfType<SelectionManager>();
        //encuentro y asigno a mi lista los botones a apagar
        GameObject[] btns = GameObject.FindGameObjectsWithTag("toToggle");
        foreach (GameObject btn in btns)
        {
            buttonsToToggle.Add(btn.GetComponent<Button>());
        }
        combineButton.interactable = false;

        mainInfoPanel = FindObjectOfType<MainInfoPanel>();
        buttonInfoPanel = GameObject.Find("InteractivePanelInfoBtn");
    }

    public void SwitchCombineMode()
    {
        combineMode = !combineMode;
        //apago los botones
        foreach (Button btn in buttonsToToggle)
        {
            btn.interactable = !combineMode;
        }
        combineButton.interactable = !combineButton.interactable;
        // le aviso al selection manager que cambié de modo
        selectionManager.SwitchCombineMode(combineMode);

        //obtengo el texto del boton y lo cambio
        if (!combineMode)
        {
            combineModeButton.text = "Creación";

            //si se ingresa a modo combinacion POR AHORA habilita boton
            buttonInfoPanel.GetComponent<Button>().interactable = true;          
        }
        else
        {
            combineModeButton.text = "Combinación";

            //si se ingresa a modo combinacion POR AHORA cierra panel
            mainInfoPanel.GetComponent<OpenMenus>().CloseBottomPanel();

            //si se ingresa a modo combinacion POR AHORA desactiva tambien el boton info
            buttonInfoPanel.GetComponent<Button>().interactable = false;

            /*CREA UN TIP! CON LA TEMATICA PASADA POR ID*/
            tipsManager.LaunchTips(1);
        }
    }

    public void Combine(){
        List<int> selectedAtoms = atomManager.GetSelectedAtoms();
        List<int> selectedMolecules = moleculeManager.GetSelectedMolecules();
        int atomCount = selectedAtoms.Count;
        int molCount = selectedMolecules.Count;
        if(atomCount > 0 && molCount > 0){
            Debug.LogError("CombinationManager :: Se intentó combinar átomos y moléculas.");
            popup.MostrarPopUp("Manager Combinación", 
                "Solo se pueden combinar átomos con átomos, y moléculas con moléculas.");
        }else{
            if(atomCount>1){
                    Debug.Log("Combinando átomos.");
                    CombineAtoms(selectedAtoms);

                    /*CREA UN TIP! CON LA TEMATICA PASADA POR ID*/
                    tipsManager.LaunchTips(2);
            }
            else{
                if(molCount>1){
                    Debug.Log("Combinando moléculas.");
                    CombineMolecules(selectedMolecules);
                }else{
                    Debug.LogError("CombinationManager :: Intento de combinar sin seleccionar nada");
                    popup.MostrarPopUp("Manager Combinación",
                        "Debe seleccionar 2 o más átomos o moléculas para poder combinar.");
                }
            }
        }
    }

    //Acá tiene que ir a la bd a buscar la combinación
    private void CombineAtoms(List<int> selectedAtoms)
    {
        List<int> elementNumbers = new List<int>();
        bool combined = false;
        foreach (int index in selectedAtoms)
        {
            int numeroElemento = atomManager.FindAtomInList(index).ElementNumber;
            if(numeroElemento != 0)
            {
                elementNumbers.Add(numeroElemento);
            }
        }

        if (elementNumbers != null && elementNumbers.Count > 0)
        {
            List<List<int>> possibleCombinations = new List<List<int>>();
            IEnumerable<(int ElementId, int Count)> combinedElements = from element in elementNumbers
                                   group element by element into elementOccurrences
                                   select (
                                      ElementId: elementOccurrences.Key, Count: elementOccurrences.Count()
                                   );

            foreach ((int elementId, int count) in combinedElements)
            {
                List<int> molecules = new List<int>();
                try
                {
                    molecules = qryMolecule.GetMoleculesByAtomNumberAndQuantity(elementId, count);
                    possibleCombinations.Add(molecules);
                }
                catch (Exception e)
                {
                    Debug.LogError("CombinationManager :: Ocurrio un error al buscar moleculas por elemento y cantidad: " + e.Message);
                    popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo moleculas por elemento y cantidad");
                    return;
                }
            }

            if (possibleCombinations != null && possibleCombinations.Count > 0)
            {
                List<int> intersection = Intersect(possibleCombinations);
                if (intersection.Count > 0)
                {
                    bool found = false;
                    foreach(int moleculaId in intersection)
                    {
                        int elementCount;
                        try
                        {
                            elementCount = qryMolecule.GetUniqueElementCountInMoleculeById(moleculaId);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("CombinationManager :: Ocurrio un error al buscar Elementos Componentes de una Molecula: " + e.Message);
                            popup.MostrarPopUp("Elementos Qry DB", "Error obteniendo Elementos Componentes de una Molecula");
                            return;
                        }


                        if (elementCount == combinedElements.ToList().Count)
                        {
                            MoleculeData moleculeData = null;
                            try
                            {
                                moleculeData = qryMolecule.GetMoleculeById(moleculaId);
                            }
                            catch (Exception e)
                            {
                                Debug.LogError("CombinationManager :: Ocurrio un error al buscar Identificador de Molecula: " + e.Message);
                                popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Identificador de Molecula");
                                return;
                            }

                            List<AtomInMolPositionData> atomsPosition;
                            try
                            {
                                atomsPosition = qryMolecule.GetElementPositions(moleculaId);
                            }
                            catch (Exception e)
                            {
                                Debug.LogError("CombinationManager :: Ocurrio un error al buscar posiciones de los Elementos Quimico: " + e.Message);
                                popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo posiciones de los Elementos Quimicos");
                                return;
                            }

                            //flag para popup
                            found = true;

                            //lanza el proceso de spawn localmente..
                            SpawnMolecule(atomsPosition, moleculeData.ToStringToList, selectedAtoms, moleculeData.ToString);

                            break;

                        }
                    }
                    if (!found)
                    {
                        combined = CombineAtomsMaterial(elementNumbers);
                    }
                } 
                else
                {
                    combined = CombineAtomsMaterial(elementNumbers);
                }
            }
            else
            {
                combined = CombineAtomsMaterial(elementNumbers);
            }

            if (combined)
            {
                DeleteCombinedAtoms(selectedAtoms);
            }
        }
        else
        {
            popup.MostrarPopUp("Manager Combinación", "No hay átomos válidos seleccionados");
        }
    }

    private bool CombineAtomsMaterial(List<int> atomIds)
    {
        int previous = atomIds[0];
        //valido que todos los atomos sean iguales
        foreach (int atomId in atomIds)
        {
            if (previous != atomId)
            {
                Debug.Log("CombinationManager :: No se pueden formar materiales con atomos distintos.");
                popup.MostrarPopUp("Combinación", "No se encontraron moléculas o materiales que contengan esos átomos");
                return false;
            }
        }

        try
        {
            List<MaterialMappingData> possibleMappings = qryMaterial.GetMaterialByAtomId(previous);

            if (possibleMappings == null || possibleMappings.Count <= 0)
            {
                Debug.Log("CombinationManager :: No se encontraron moléculas o materiales que contengan esos átomos.");
                popup.MostrarPopUp("Combinación", "No se encontraron moléculas o materiales que contengan esos átomos");
                return false;
            }
            // TODO - Deberia aparecerle un pop up al usuario para que elija que combinacion realizar
            //        Por ahora hardcodeo la primera que encuentre.

            MaterialMappingData mapping = possibleMappings[0];
            MaterialData material = qryMaterial.GetMaterialById(mapping.IdMaterial);
            ElementInfoBasic elementInfo = qryElementos.GetElementInfoBasica(previous);

            if (material == null)
            {
                popup.MostrarPopUp("Combinación", "No se encontró ninguna combinación posible");
                return false;
            }

            if (atomIds.Count >= mapping.Amount)
            {
                popup.MostrarPopUp("Combinación", "Creaste " + material.Name);
                materialManager.SpawnMaterial(material);
            }
            else
            {
                popup.MostrarPopUp("Cantidad insuficiente", "Se necesitan al menos " + mapping.Amount
                    + " átomos de " + elementInfo.Name +
                    " para formar " + material.Name);
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Hubo un error tratando de obtener el material de la base de datos" + e.StackTrace);
            popup.MostrarPopUp("Manager Combinación",
                "Hubo un error tratando de obtener el material de la base de datos");
            return false;
        }

        return true;
    }

    private void CombineMolecules(List<int> selectedMolecules){
        List<int> moleculeIds = new List<int>();
        foreach (int index in selectedMolecules)
        {
            int molId = moleculeManager.FindMoleculeInList(index).MoleculeId;
            if(molId != 0)
            {
                moleculeIds.Add(molId);
            }
        }
        int previous = moleculeIds[0];
        //valido que todas las moléculas sean iguales
        foreach (int mol in moleculeIds){
            if(previous != mol){
                Debug.Log("No se pueden combinar moléculas distintas.");
                popup.MostrarPopUp("Manager Combinación", 
                    "SIAMM no soporta combinar distintas moléculas aún.");
                return;
            }
        }
        try{
            //acá agarro el [0] porque son todos iguales los índices (lo chequee arriba)
            MaterialMappingData mapping = qryMaterial.GetMaterialByMoleculeId(moleculeIds[0]);
            MoleculeData molecule = qryMolecule.GetMoleculeById(mapping.IdMolecule);
            MaterialData material = qryMaterial.GetMaterialById(mapping.IdMaterial);

            if (material == null)
            {
                popup.MostrarPopUp("Combinación", "No se encontró ninguna combinación posible");
                return;
            }

            if(moleculeIds.Count >= mapping.Amount)
            {
                popup.MostrarPopUp("Combinación","Creaste " + material.Name);
                materialManager.SpawnMaterial(material);
                DeleteCombinedMolecules(selectedMolecules);
            }
            else
            {
                Debug.Log("Cantidad insuficiente");
                popup.MostrarPopUp("Cantidad insuficiente","Se necesitan al menos " + mapping.Amount 
                    + " moléculas de " + molecule.TraditionalNomenclature + 
                    " para formar " + material.Name);
            }
            
        }catch(Exception e){
            Debug.LogError("Hubo un error tratando de obtener el material de la base de datos" + e.StackTrace);
            popup.MostrarPopUp("Manager Combinación", 
                "Hubo un error tratando de obtener el material de la base de datos");
        }
    }


    static List<int> Intersect(List<List<int>> lists)
    {
        return lists.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
    }

    //Borrar del espacio de trabajo, los átomos seleccionados.
    private void DeleteCombinedAtoms(List<int> selectedAtoms)
    {
        foreach(int atom in selectedAtoms)
        {
            atomManager.DeleteAtom(atom);
        }
    }

    // Borrar del espacio de trabajo, las moleculas seleccionadas.
    private void DeleteCombinedMolecules(List<int> selectedMolecules)
    {
        foreach (int molecule in selectedMolecules)
        {
            moleculeManager.DeleteMolecule(molecule);
        }
    }

    #region Metodos Para traslacion de objetos atomos
    //metodo para lanzar traslacion, animacion y luego recien spawnear la molecula (no se usan atributos /var globales)
    private void SpawnMolecule(List<AtomInMolPositionData> atomsPosition, string name, List<int> selectedAtoms, string toStringPopup)
    {
        //solicito a Manager de molecula los datos de la futura nueva molecula a crear
        Molecule newMoleculeAndPos = moleculeManager.GetMoleculePos(true);

        //ACA VA LA MAGIA, y va trasladando los parametros para que sea serializado
        InteractivePosCombinedAtoms(atomsPosition, name, selectedAtoms, newMoleculeAndPos.transform.localPosition, newMoleculeAndPos, toStringPopup);

        //mientras genera la molecula resultante, y la corutina hace el efecto del tralation
        moleculeManager.SpawnMolecule(atomsPosition, name, newMoleculeAndPos);
    }


    //Obtener los gameobjets atomos seleccionados para tener su posicion inicial y lanzar la corutina principal/
    public void InteractivePosCombinedAtoms(List<AtomInMolPositionData> atomsPosition, string name, List<int> selectedAtoms, Vector3 posMoleculefinal, Molecule newMoleculeAndPos, string toStringPopup)
    {
        List<Atom> posAtomInicial = new List<Atom>();

        //obtengo los gameObject ATOM para interactuar con la posicion final de la molecula nueva
        foreach (int atom in selectedAtoms)
        {
            posAtomInicial.Add(atomManager.FindAtomInList(atom));
        }

        //ACA LANZA LA CORUTINA y sigue para terminar con el proceso main llamador!    
        StartCoroutine(IniciaTask(atomsPosition, name, selectedAtoms, posMoleculefinal, posAtomInicial, newMoleculeAndPos, toStringPopup));
    }


    //metodo que ejecuta un "hilo" para traslacion de gameobjects atoms de posicion inicial a una final relativa
    public IEnumerator translation(Vector3 finalPos, List<Atom> posAtomInicial)
    {
        foreach (Atom atom in posAtomInicial)
        {
            float lerpTime = 0.99f;//tiempo de retardo en los frames.. para generar el efecto fade
            float _timeStartedLerping = Time.time;
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float porcentajeComplete = timeSinceStarted / lerpTime;

            while (true)
            {

                timeSinceStarted = Time.time - _timeStartedLerping;
                porcentajeComplete = timeSinceStarted / lerpTime;

                //hago que desaparezca el label! 
                atom.elementLabel.GetComponent<TextMesh>().text = "";

                //aplico el traslado de los ATOMOS a la posicion final donde se creara la molecula combinada
                atom.transform.position = Vector3.Lerp(atom.transform.position, finalPos, porcentajeComplete);

                if (porcentajeComplete >= 1) break;

                yield return new WaitForEndOfFrame();
            }
        }
    }

    

    //Metodo que espera o sincroniza la corutina de traslacion para que espere que termine la tralsacion y recien ahi seguir con las tareas posteriores
    IEnumerator IniciaTask(List<AtomInMolPositionData> atomsPosition, string name, List<int> selectedAtoms, Vector3 posMoleculefinal, List<Atom> posAtomInicial, Molecule newMoleculeAndPos, string toStringPopup)
    {
        //lanza el traslation
        yield return StartCoroutine(translation(posMoleculefinal, posAtomInicial));

        //muestra animacion!
        StartAnimationCombination(posMoleculefinal);

        //cuando termina recien ahi! borra los atomos seleccionados
        DeleteCombinedAtoms(selectedAtoms);


        //AHORA SI! QUE SE VEA la molecula que ya se creo.. y quedo en NO VISIBLE esperando que se termine la courutina!
        newMoleculeAndPos.gameObject.SetActive(true);

        yield return StartCoroutine(StopAnimationCombination());

        //exito! muestro por pantalla 
        popup.MostrarPopUp("Manager Combinación", "Molécula Formada: " + toStringPopup);
    }


    //corutina para desaparecer la animacion de combinacion
    private IEnumerator StopAnimationCombination()
    {
        float lifeTime = 2.0f;//tiempo de espera que retarda la animacion
        yield return new WaitForSeconds(lifeTime);
        Destroy(animationCombination);
    }


    //instancio animacion en el momento que se necesita para no consumir recursos constantemente
    private void StartAnimationCombination(Vector3 posMoleculefinal)
    {
        // creo una copia del prefab
        animationCombination = Instantiate<GameObject>(animationPrefab);
        // seteo posición
        animationCombination.transform.localPosition = posMoleculefinal;
    }

    #endregion

}
