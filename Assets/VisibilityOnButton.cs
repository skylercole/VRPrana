using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class VisibilityOnButton : MonoBehaviour {

    public VRInput input;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;

    private bool visible = true;

    // Use this for initialization
    void Start ()
    {
        input.OnClick += () =>
        {
            // Disable images
            image1.enabled = !visible;
            image2.enabled = !visible;
            image3.enabled = !visible;
            image4.enabled = !visible;

            // Disable texts
            GameObject[] tagged = GameObject.FindGameObjectsWithTag("clickable");
            foreach (GameObject f in tagged)
            {
                f.GetComponent<Renderer>().enabled = !visible;
            }

            visible = !visible;
        };

        input.OnCancel += () =>
        {
            SceneManager.LoadSceneAsync("Scenes/05 VRMenuWithVRRaycaster");
        };
    }
}