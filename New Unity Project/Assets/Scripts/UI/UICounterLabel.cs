using TMPro;
using UnityEngine;

public class UICounterLabel : MonoBehaviour
{
    private const string EMPTY_VALUE = "-";

    private AtomManager atomManager;
    private MoleculeManager moleculeManager;

    // seteo desde inspector a que label pertenece la instancia de este script
    [SerializeField]
    public ParticleEnum particleType;

    void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = EMPTY_VALUE;
    }

    // Update is called once per frame
    void Update()
    {
        // traigo todos los atomos seleccionados
        var selectedAtoms = atomManager.GetSelectedAtoms();
        var selectedMolecules = moleculeManager.GetSelectedMolecules();

        // si hay un solo atomo seleccionado y ninguna molecula, actualizo los labels con sus contadores
        if (selectedAtoms.Count == 1 && selectedMolecules.Count == 0)
        {
            int atomIndex = selectedAtoms[0];
            Atom atom = atomManager.FindAtomInList(atomIndex);

            string counterText = EMPTY_VALUE;
            switch (particleType)
            {
                case ParticleEnum.Proton:
                    counterText = atom.ProtonCounter.ToString();
                    break;
                case ParticleEnum.Neutron:
                    counterText = atom.NeutronCounter.ToString();
                    break;
                case ParticleEnum.Electron:
                    counterText = atom.ElectronCounter.ToString();
                    break;
            }

            gameObject.GetComponent<TextMeshProUGUI>().text = counterText;
        }
        else
        {
            // si no hay ninguno o hay mas de uno seleccionado
            gameObject.GetComponent<TextMeshProUGUI>().text = EMPTY_VALUE;
        }
    }
}
