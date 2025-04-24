using System;
using UnityEngine;

public class FarmSatateManager : MonoBehaviour
{
    public enum FarmState
    {
        Empty,
        Planted,
        Growing,
        Finished,
        Rotten,
        Harvested
    }

    public FarmState farmState = FarmState.Empty;

    private void UpdateFarmState()
    {
        // switch the main timer to equal the appropriate timer
        switch (farmState)
        {
            case FarmState.Empty:
                //change to empty sprite
                break;
            case FarmState.Planted:
                //Change to planted sprite
                //start planted timer
                break;
            case FarmState.Growing:
                //Change to growing sprite
                //Start growing timer
                break;
            case FarmState.Finished:
                //Change to finished sprite
                //Start finished timer
                break;
            case FarmState.Rotten:
                //change to rotten sprite
                break;
            case FarmState.Harvested:
                //Change give the player plants
                //Change the Farm State to Empty
                //Run this script again
            break;
        }
    }
    
    // create a script that will switch the scriptable object to different plants bassed off an enum
    
    
    // create a script that will change the timers bassed off of the selected plant
    // in the update script run the main timer
    // when the timer hits 0, 
}
