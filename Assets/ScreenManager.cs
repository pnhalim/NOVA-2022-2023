using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Screens
{
    UIA,
    Vitals,
    Geosampling,
    Messaging,
    Navigation, 
    TaskList
}
[System.Serializable]
public enum LUNAState
{
    left, right, center
}

public class ScreenManager : MonoBehaviour
{

    public GameObject UIAPanel;
    public List<GameObject> VitalsPanel;
    public GeoSampleVegaController GeosamplingPanel;
    public GameObject MessagingPanel;
    public NavScreenController NavigationPanel;

    // STATE MACHINE
    public static Screens CurrScreen = Screens.UIA;
    public static LUNAState LUNA = LUNAState.center;

    public void SwitchScreen(string ScreenToShow)
    {
        switch (ScreenToShow)
        {
            case "UIA":
                UIAPanel.SetActive(true);
                foreach (GameObject g in VitalsPanel)
                {
                    g.SetActive(false);
                }
                GeosamplingPanel.minimize();
                MessagingPanel.SetActive(false);
                NavigationPanel.CloseAll();
                CurrScreen = Screens.UIA;
                break;
            case "Vitals":
                UIAPanel.SetActive(false);
                foreach (GameObject g in VitalsPanel)
                {
                    g.SetActive(true);
                }
                GeosamplingPanel.minimize();
                MessagingPanel.SetActive(false);
                NavigationPanel.CloseAll();
                CurrScreen = Screens.Vitals;
                break;
            case "Geosampling":
                UIAPanel.SetActive(false);
                foreach (GameObject g in VitalsPanel)
                {
                    g.SetActive(false);
                }
                GeosamplingPanel.open();
                MessagingPanel.SetActive(false);
                NavigationPanel.CloseAll();
                CurrScreen = Screens.Geosampling;
                break;
            case "Messaging":
                UIAPanel.SetActive(false);
                foreach (GameObject g in VitalsPanel)
                {
                    g.SetActive(false);
                }
                GeosamplingPanel.minimize();
                MessagingPanel.SetActive(true);
                NavigationPanel.CloseAll();
                CurrScreen = Screens.Messaging;
                break;
            case "Navigation":
                UIAPanel.SetActive(false);
                foreach (GameObject g in VitalsPanel)
                {
                    g.SetActive(false);
                }
                GeosamplingPanel.open();
                MessagingPanel.SetActive(false);
                NavigationPanel.OpenNavMainMenu();
                CurrScreen = Screens.Navigation;
                break;
            default:
                break;
        }
        Debug.Log("Curr Screen = " + ScreenToShow.ToString());
    }
    
    public void CloseScreen(string ScreenToClose)
    {

    }

    public static void ScrollUp()
    {
        if (LUNA == LUNAState.center)
        {
            EventBus.Publish<ScrollEvent>(new ScrollEvent(CurrScreen, Direction.up));
        }
        else if (LUNA == LUNAState.right)
        {
            EventBus.Publish<ScrollEvent>(new ScrollEvent(Screens.TaskList, Direction.up));
        }
        
    }

    public static void ScrollDown()
    {
        if (LUNA == LUNAState.center)
        {
            EventBus.Publish<ScrollEvent>(new ScrollEvent(CurrScreen, Direction.down));
        }
        else if (LUNA == LUNAState.right)
        {
            EventBus.Publish<ScrollEvent>(new ScrollEvent(Screens.TaskList, Direction.down));
        }
    }

    // RANDOM STUFF
    public void CloseScreen(GameObject Screen)
    {
        StartCoroutine(_CloseScreen(Screen));
    }
    IEnumerator _CloseScreen(GameObject Screen)
    {
        yield return new WaitForSeconds(1f);
        Screen.SetActive(false);
    }
}
