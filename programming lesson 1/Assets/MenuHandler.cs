using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//interacting with scene change
using UnityEngine.UI;//interacting with GUI elements
using UnityEngine.EventSystems;//control the event

public class MenuHandler : MonoBehaviour
{
    [Header("References")]
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public AudioSource audioSource;
    public Slider volumeSlider;
    public Toggle volumeToggle;
    public Slider brightnessSlider;
    public Slider ambientSlider;

    public bool isFullScreen;
    public Toggle fullscreenToggle;
    public Dropdown resDropdown;

    [Header("Keys")]
    public KeyCode holdingKey;
    public KeyCode forward, backward, left, right, jump, crouch, sprint, interact;

    [Header("Keybind References")]
    public Text forwardText;
    public Text backwardText, leftText, rightText, jumpText, crouchText, sprintText, interactText;

    Resolution[] resolutions;
    public int resolutionIndex;

    public Light dirLight;

    [HideInInspector]
    public bool showOptions;

    [Range(1, 3)]
    public int levelSelect;

    public void LoadGame()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //Will only exit if in Unity Editor
#endif
        Application.Quit();
        //Exit built application
    }

    public void ToggleOptions()
    {
        OptionToggle();

    }
    bool OptionToggle()
    {
        if (showOptions)//showOptions == true or showOptions is true
        {
            showOptions = false;
            //Set to not display Options Menu as it is not actived
            mainMenu.SetActive(true);
            //Show Main Menu as Options is not being viewed
            optionsMenu.SetActive(false);
            return true;
        }
        else
        {
            showOptions = true;
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);

            volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
            volumeToggle = GameObject.Find("VolumeToggle").GetComponent<Toggle>();
            brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
            audioSource = GameObject.Find("MainMusic").GetComponent<AudioSource>();
            ambientSlider = GameObject.Find("AmbientSlider").GetComponent<Slider>();
            ambientSlider.value = RenderSettings.ambientIntensity;

            resDropdown = GameObject.Find("ResolutionDropdown").GetComponent<Dropdown>();
            fullscreenToggle = GameObject.Find("FullscreenToggle").GetComponent<Toggle>();
            //resDropdown.ClearOptions();
            for (int i = 0; i < resolutions.Length; i++)
            {
                resDropdown.options[i].text = ResolutionToString(resolutions[i]);
                //resDropdown.value = i;
                resDropdown.options.Add(new Dropdown.OptionData(resDropdown.options[i].text));
            }

            volumeSlider.value = audioSource.volume;

            return false;
        }
    }

    public void Volume()
    {
        audioSource.volume = volumeSlider.value;
    }

    public void Mute()
    {
        audioSource.mute = !volumeToggle.isOn;
    }

    public void Brightness()
    {
        dirLight.intensity = brightnessSlider.value;
    }

    public void Ambience()
    {
        RenderSettings.ambientIntensity = ambientSlider.value;
    }

    public void Resolution()
    {
        resolutionIndex = resDropdown.value;
        Screen.SetResolution((int)resolutions[resolutionIndex].width, (int)resolutions[resolutionIndex].height, isFullScreen);
    }

    // Use this for initialization
    void Start()
    {
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward","W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));
    }

    string ResolutionToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }

    public void Fullscreen()
    {
        isFullScreen = !fullscreenToggle.isOn;
        Resolution();
    }

    public void Save()
    {
        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Crouch", crouch.ToString());
        PlayerPrefs.SetString("Sprint", sprint.ToString());
        PlayerPrefs.SetString("Interact", interact.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
