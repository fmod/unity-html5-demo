using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadBankAndScene : MonoBehaviour
{
    [SerializeField]
    public string sceneName;

    [FMODUnity.BankRef]
    public List<string> banks;

    public Button ClickToLoadButton, ChangeSceneButton;
    public Text LoadingBanksText;

    private void Awake()
    {
        LoadingBanksText.gameObject.SetActive(false);
        ChangeSceneButton.interactable = false;
    }

    public void LoadBanks()
    {
        foreach (string b in banks)
        {
            FMODUnity.RuntimeManager.LoadBank(b, true);
            Debug.Log("Loaded bank " + b);
        }
        /*
            For Chrome / Safari browsers / WebGL.  Reset audio on response to user interaction (LoadBanks is called from a button press), to allow audio to be heard.
        */
        FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
        FMODUnity.RuntimeManager.CoreSystem.mixerResume();
        
        LoadingBanksText.gameObject.SetActive(true);
        StartCoroutine(CheckBanksLoaded());
    }

    IEnumerator CheckBanksLoaded()
    {
        while (!FMODUnity.RuntimeManager.HasBanksLoaded)
        {
            yield return null;
        }

        LoadingBanksText.text = "Banks Loaded";
        LoadingBanksText.GetComponent<Text>().color = Color.green;
        ChangeSceneButton.interactable = true;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
