using UnityEngine;

public class LoadEditorBanks : MonoBehaviour
{
#if UNITY_EDITOR // Used for testing in Editor since banks are loaded in a different scene.
    private void Awake()
    {
        FMODUnity.RuntimeManager.LoadBank("Master");
        FMODUnity.RuntimeManager.LoadBank("Master.strings");
    }
#endif

}
