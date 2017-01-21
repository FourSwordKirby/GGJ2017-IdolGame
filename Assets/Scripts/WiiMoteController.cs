using UnityEngine;
using System.Collections;
using WiimoteApi;

public class WiiMoteController : MonoBehaviour
{
    public Wiimote ourWiimote;
    public bool connected = false;
    // Update is called once per frame
    void Update()
    {
        if (!(WiimoteManager.HasWiimote()) || WiimoteManager.Wiimotes.Count != 2 || !(connected)) //How many Wiimotes do we need or want?
        {
            WiimoteManager.FindWiimotes();
            foreach (Wiimote remote in WiimoteManager.Wiimotes)
            {
                ourWiimote = remote;
                print("here");
            }
            connected = true;
        }
        if (connected)
        {
            //ourWiimote.SendStatusInfoRequest(); //For checking battery. I don't think it works though.
            ourWiimote.SendPlayerLED(true, false, false, false);
            printAccelerometerData();
        }
        else
        {
            print("Tautological error detected");
        }
        //To read off whether or not a button is pressed, do something like:
        //print(ourWiimote.Button.d_left);
        //Make sure to ask for the wiimote data beforehand
    }
    
    void printAccelerometerData()
    {
        //ourWiimote.SendStatusInfoRequest(); //For checking battery. I don't think it works though.
        ourWiimote.ReadWiimoteData();
        ourWiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL); //This line probably only needs to be called once
        //Calculates the magnitude of force acting towards the right side of the controller (so having it on the left side gives -1)
        print("Acceleration rightwards (in g's):");
        print(ourWiimote.Accel.GetCalibratedAccelData()[0]);
        //Calculates the magnitude of force acting towards the front of the controller (so having it on the port side gives -1)
        print("Acceleration forwards (in g's):");
        print(ourWiimote.Accel.GetCalibratedAccelData()[1]);
        print("Acceleration downwards (in g's):");
        //Calculates the magnitude of force acting towards the back of the controller (so having it on the A button side gives -1)
        print(ourWiimote.Accel.GetCalibratedAccelData()[2]);
    }

}
