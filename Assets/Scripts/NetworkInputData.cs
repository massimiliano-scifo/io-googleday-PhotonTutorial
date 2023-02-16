using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    //Kinetics public const byte MOUSEBUTTON1 = 0x01;
    //Physics public const byte MOUSEBUTTON2 = 0x02;

    public byte buttons;
    public Vector3 direction; // there are better ways of doing that (like a bitfield with one bit per direction), but vectors are easier to understand
}