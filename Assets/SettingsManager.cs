using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour
{
    public Slider MainSlider;
    public Text TimeText;
    public Image InfoImage;
    public Text BreathInfo;
    public AudioSource AudioSource;
    public Button AudioButton;
    public Text FrozenLakeTime;
    public Text FrozenForestTime;
    public Text ClearMindTime;
    public Text RelaxTime;
    public Text CalmingTime;
    public Text PowerTime;
    public Text HarmonyTime;
    public Text AntiStressTime;
    public Text AntiAppetiteTime;

    private void Awake()
    {
        GlobalVariables.MeditationPractice = (PlayerPrefs.HasKey("MeditationPractice") == true) ?
            PlayerPrefs.GetInt("MeditationPractice") : 0;
        GlobalVariables.TimeMinutes = (PlayerPrefs.HasKey("TimeMinutes") == true) ?
            PlayerPrefs.GetInt("TimeMinutes") : 7;
        GlobalVariables.SoundOn = (PlayerPrefs.HasKey("SoundOn") == true) ?
            PlayerPrefs.GetInt("SoundOn") : 1;

        MainSlider.value = GlobalVariables.TimeMinutes;
        TimeText.text = String.Format("{00:00}:00", GlobalVariables.TimeMinutes);
        SelectBreathControl.isButtonPressed[GlobalVariables.MeditationPractice] = true;
        BreathInfo.text = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].Description;
            //BreathControl.BreathControlText[GlobalVariables.MeditationPractice];
        InfoImage.overrideSprite = Resources.Load<Sprite>("menucircle" + (GlobalVariables.MeditationPractice + 1));
        InfoImage.color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);

        if (GlobalVariables.SoundOn == 0)
        {
            AudioButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("loudspeaker_pause");
            AudioSource.Stop();
        }

        GlobalVariables.FrozenLakeTime = GetTime("FrozenLakeTime");
        GlobalVariables.FrozenForestTime = GetTime("FrozenForestTime");
        GlobalVariables.ClearMindTime = GetTime("ClearMindTime");
        GlobalVariables.RelaxTime = GetTime("RelaxTime");
        GlobalVariables.CalmingTime = GetTime("CalmingTime");
        GlobalVariables.PowerTime = GetTime("PowerTime");
        GlobalVariables.HarmonyTime = GetTime("HarmonyTime");
        GlobalVariables.AntiStressTime = GetTime("AntiStressTime");
        GlobalVariables.AntiAppetiteTime = GetTime("AntiAppetiteTime");

        if (GlobalVariables.FrozenLakeTime != DateTime.MinValue)
            FrozenLakeTime.text = String.Format("Last played: {0:ddd, MMM d}", GlobalVariables.FrozenLakeTime);

        if (GlobalVariables.FrozenForestTime != DateTime.MinValue)
            FrozenForestTime.text = String.Format("Last played: {0:ddd, MMM d}", GlobalVariables.FrozenForestTime);

        if (GlobalVariables.ClearMindTime != DateTime.MinValue)
            ClearMindTime.text = String.Format("Last used: {0:ddd, MMM d}", GlobalVariables.ClearMindTime);

        if (GlobalVariables.RelaxTime != DateTime.MinValue)
            RelaxTime.text = String.Format("Last used: {0:ddd, MMM d}", GlobalVariables.RelaxTime);

        if (GlobalVariables.CalmingTime != DateTime.MinValue)
            CalmingTime.text = String.Format("Last used: {0:ddd, MMM d}", GlobalVariables.CalmingTime);

        if (GlobalVariables.PowerTime != DateTime.MinValue)
            PowerTime.text = String.Format("Last used: {0:ddd, MMM d}", GlobalVariables.PowerTime);

        if (GlobalVariables.HarmonyTime != DateTime.MinValue)
            HarmonyTime.text = String.Format("Last used: {0:ddd, MMM d}", GlobalVariables.HarmonyTime);

        if (GlobalVariables.AntiStressTime != DateTime.MinValue)
            AntiStressTime.text = String.Format("Last used: {0:ddd, MMM d}", GlobalVariables.AntiStressTime);

        if (GlobalVariables.AntiAppetiteTime != DateTime.MinValue)
            AntiAppetiteTime.text = String.Format("Last used: {0:ddd, MMM d}", GlobalVariables.AntiAppetiteTime);
    }

    public static void PersistData(int sceneToPlay)
    {
        PlayerPrefs.SetInt("MeditationPractice", GlobalVariables.MeditationPractice);
        PlayerPrefs.SetInt("TimeMinutes", GlobalVariables.TimeMinutes);
        PlayerPrefs.SetInt("SoundOn", GlobalVariables.SoundOn);

        switch (GlobalVariables.MeditationPractice)
        {
            case 0:
            {
                PlayerPrefs.SetString("ClearMindTime", DateTime.Now.ToString());
                break;
            }
            case 1:
            {
                PlayerPrefs.SetString("AntiStressTime", DateTime.Now.ToString());
                break;
            }
            case 2:
            {
                PlayerPrefs.SetString("AntiAppetiteTime", DateTime.Now.ToString());
                break;
            }
            case 3:
            {
                PlayerPrefs.SetString("RelaxTime", DateTime.Now.ToString());
                break;
            }
            case 4:
            {
                PlayerPrefs.SetString("PowerTime", DateTime.Now.ToString());
                break;
            }
            case 5:
            {
                PlayerPrefs.SetString("CalmingTime", DateTime.Now.ToString());
                break;
            }            
            case 6:
            {
                PlayerPrefs.SetString("HarmonyTime", DateTime.Now.ToString());
                break;
            }
        }

        switch (sceneToPlay)
        {
            case 0:
                PlayerPrefs.SetString("FrozenForestTime", DateTime.Now.ToString());
                break;
            case 1:
                PlayerPrefs.SetString("FrozenLakeTime", DateTime.Now.ToString());
                break;
        }
    }

    private DateTime GetTime(string timeString)
    {
        var time = (PlayerPrefs.HasKey(timeString) == true) ?
            PlayerPrefs.GetString(timeString) : "";
        if (time == "")
        {
            return DateTime.MinValue; ;
        }
        else
        {
            return Convert.ToDateTime(time);
        }
    }
}