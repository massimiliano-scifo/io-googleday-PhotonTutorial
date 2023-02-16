using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    //Kinetics [SerializeField] private Ball _prefabBall;
    //Physics [SerializeField] private PhysxBall _prefabPhysxBall;
    [Networked] private TickTimer delay { get; set; }

    private NetworkCharacterControllerPrototype _cc;
    private Vector3 _forward;

    /* 
     * Property changes
     * When defining networked properties, Fusion will replace the provided get and set stubs with custom code to access the network state. 
     * This means that the application cannot use these methods to deal with changes in property values, 
     * and creating separate setter methods will only work locally.
    
    private Material _material;
    Material material
    {
        get
        {
            if (_material == null)
                _material = GetComponentInChildren<MeshRenderer>().material;
            return _material;
        }
    }

    [Networked(OnChanged = nameof(OnBallSpawned))]
    public NetworkBool spawned { get; set; }
    public static void OnBallSpawned(Changed<Player> changed)
    {
        changed.Behaviour.material.color = Color.red;
    }

    public override void Render()
    {
        material.color = Color.Lerp(material.color, Color.blue, Time.deltaTime);
    }
    */

    /* RPC
     * RPC are not tied to a specific tick and will execute at different times on different clients. 
     * RPC are not part of the network state, so any player that connects or re-connects after an RPC was sent, 
     * or just didn't receive it because it was sent unreliably, will never see the consequences of it.
    
    private Text _messages;
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_SendMessage(string message, RpcInfo info = default)
    {
        if (_messages == null)
            _messages = FindObjectOfType<Text>();
        if (info.IsInvokeLocal)
            message = $"You said: {message}\n";
        else
            message = $"Some other player said: {message}\n";
        _messages.text += message;
    }
    
    private void Update()
    {
        //Object.HasInputAuthority - this is because this code runs on all clients, but only the client that controls this player should call the RPC
        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.R))
        {
            RPC_SendMessage("Hey Mate!");
        }
    }
    */
    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        _forward = transform.forward;
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize(); // prevent cheating and client-side code-injection
            _cc.Move(5 * Runner.DeltaTime * data.direction);
            /*Kinetics
            if (data.direction.sqrMagnitude > 0)
                _forward = data.direction;

            if (delay.ExpiredOrNotRunning(Runner))
            {
                if ((data.buttons & NetworkInputData.MOUSEBUTTON1) != 0)
                {
                    delay = TickTimer.CreateFromSeconds(Runner, 0.5f); // limit spawn frequency
                    Runner.Spawn(_prefabBall,
                    transform.position + _forward, Quaternion.LookRotation(_forward),
                    Object.InputAuthority, (runner, o) =>
                    {
                        // Initialize the Ball before synchronizing it, so the thick time is set properly
                        o.GetComponent<Ball>().Init();
                    });
                    //Property spawned = !spawned;
                }
                *//* Physics
                else if ((data.buttons & NetworkInputData.MOUSEBUTTON2) != 0)
                {
                    delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
                    Runner.Spawn(_prefabPhysxBall,
                      transform.position + _forward,
                      Quaternion.LookRotation(_forward),
                      Object.InputAuthority,
                      (runner, o) =>
                      {
                          o.GetComponent<PhysxBall>().Init(10 * _forward); //the Player must also call spawn and set the velocity
                      });
                    //Property spawned = !spawned;
                }*//*
            }*/
        }
    }
    
}