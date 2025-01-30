using UnityEngine;
using AK.Wwise;

public class RoomReverbLPFController : MonoBehaviour
{
    public AK.Wwise.RTPC RoomReverb_LPF; // Assign your RTPC here
    private bool isMuffled = false;

    void Start()
    {
        if (RoomReverb_LPF == null)
        {
            Debug.LogError("RoomReverb_LPF RTPC is not assigned!");
        }
    }

    public void ToggleMuffle(bool state)
    {
        isMuffled = state;
        // Apply LPF to the Room's Aux Bus globally
        RoomReverb_LPF.SetValue(gameObject, isMuffled ? 80f : 0f);
    }
}
