using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

public class FakeTSSMessageSender : MonoBehaviour
{
    public UIAMsg fakeUIA = new UIAMsg();
    public SpecMsg fakeSpecMsg = new SpecMsg();
    public GPSMsg fakeGPS = new GPSMsg();

    private void Start()
    {
        Fake_SetUIA();
    }
    [ContextMenu("SetUIA")]
    public void Fake_SetUIA()
    {
        Simulation.User.UIA = fakeUIA;
        EventBus.Publish<UIAMsgEvent>(new UIAMsgEvent());
    }
    [ContextMenu("SetGPS")]
    public void Fake_SetGPS()
    {
        Simulation.User.GPS = fakeGPS;
        EventBus.Publish<UpdatedGPSEvent>(new UpdatedGPSEvent());
    }
    [ContextMenu("SetSpectrometer")]
    public void Fake_SetSpectrometer() {
        Simulation.User.GEO = fakeSpecMsg;
        EventBus.Publish<GeoSpecRecievedEvent>(new GeoSpecRecievedEvent());
    }

}
