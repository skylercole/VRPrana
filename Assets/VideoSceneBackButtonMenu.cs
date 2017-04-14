using UnityEngine;
using UnityEngine.UI;

public class VideoSceneBackButtonMenu : MonoBehaviour
{
    public Image Image1;
    public Image Image2;
    public Image Image3;
    public Image Image4;
    public GameObject Reticle;
    public GameObject Menu;
    public AudioSource Audio;

    public static bool? IsPausing;

    public void Update()
    {
        if (IsPausing == true && !GlobalVariables.IsVideoScenePaused)
        {
            GlobalVariables.IsVideoScenePaused = true;

            GlobalVariables.SoundOn = 0;
            Audio.volume = 0;

            ToggleVisibility(false);

            if (!Reticle.activeSelf)
                Reticle.SetActive(true);
            if (!Menu.activeSelf)
                Menu.SetActive(true);

            LoopImages(Reticle, true);
            LoopImages(Menu, true);
            LoopTexts(Menu, true);

            Menu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.77f;
            Menu.transform.LookAt(Menu.transform.position + (Menu.transform.position - Camera.main.transform.position));

            IsPausing = null;
        }
        else if (IsPausing == false && GlobalVariables.IsVideoScenePaused)
        {
            ToggleVisibility(true);

            LoopImages(Reticle, false);
            LoopImages(Menu, false);
            LoopTexts(Menu, false);

            IsPausing = null;

            GlobalVariables.SoundOn = 1;
            Audio.volume = 0.6f;

            GlobalVariables.IsVideoScenePaused = false;
        }
    }

    private static void LoopImages(GameObject obj, bool enable)
    {
        foreach (var img in obj.GetComponentsInChildren<Image>())
        {
            img.enabled = enable;
        }
    }

    private static void LoopTexts(GameObject obj, bool enable)
    {
        foreach (var txt in obj.GetComponentsInChildren<Text>())
        {
            txt.enabled = enable;
        }
    }

    private void ToggleVisibility(bool enabled)
    {
        // Progress area:
        Image1.enabled = enabled;
        Image2.enabled = enabled;
        Image3.enabled = enabled;
        Image4.enabled = enabled;

        GameObject[] tagged = GameObject.FindGameObjectsWithTag("clickable");
        foreach (GameObject f in tagged)
        {
            f.GetComponent<Renderer>().enabled = enabled;
        }
    }
}