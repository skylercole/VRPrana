using UnityEngine;

public class ShowAdvice : MonoBehaviour {

    float timeRemaining = 60f;
    private int currentAdvice = 0;
    public AudioSource audioSource;
    private bool setFirstInhale = true;

    // Use this for initialization
    void Start ()
    {
        timeRemaining = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].InhaleRatio;
        GetComponent<TextMesh>().text = "Prepare";
        InvokeRepeating("decreaseTimeRemaining", 1.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (timeRemaining <= 0 && !GlobalVariables.IsVideoScenePaused)
        {
            string advice = "";
            var timeToAdd = 0;

            if (currentAdvice == 3)
            { 
                currentAdvice = 0;
            }
            else
            {
                currentAdvice++;
            }

            switch (currentAdvice)
            {
                case 0:
                    advice = "Inhale";
                    timeToAdd = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].InhaleRatio;
                    break;
                case 1:
                    advice = "Retain";
                    timeToAdd = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].RetainRatio;
                    break;
                case 2:
                    advice = "Exhale";
                    timeToAdd = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].ExhaleRatio;
                    break;
                case 3:
                    advice = "Sustain";
                    timeToAdd = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].SustainRatio;
                    break;
            }

            if (timeToAdd > 0)
            {
                GetComponent<TextMesh>().text = advice;

                if (audioSource.pitch > 1)
                {
                    audioSource.pitch = 1;
                }
                else
                {
                    audioSource.pitch = 1.1f;
                }
                
                PlayAudio();
            }

            timeRemaining = timeToAdd;
        }
    }

    void decreaseTimeRemaining()
    {
        if (!GlobalVariables.IsVideoScenePaused)
        { 
            if (ChangeCycleTime.InitalCounter > 0)
            {
                GetComponent<TextMesh>().text = "Prepare";
            }
            else
            {
                if (setFirstInhale)
                {
                    GetComponent<TextMesh>().text = "Inhale";
                    setFirstInhale = false;
                    PlayAudio();
                }
                else
                {
                    timeRemaining--;
                }
            }
        }
    }

    void PlayAudio()
    {
        if (GlobalVariables.SoundOn == 1)
        {
            audioSource.Stop();
            audioSource.Play();
        }
    }
}