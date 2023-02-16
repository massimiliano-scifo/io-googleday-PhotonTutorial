using Fusion;

public class Ball : NetworkBehaviour
{
    [Networked] private TickTimer Life { get; set; }
    public void Init()
    {
        //Instead of storing the current remaining time, TickTimer stores the end-time in ticks.
        //This means the timer does not need to be sync'ed on every tick but just once, when it is created.

        Life = TickTimer.CreateFromSeconds(Runner, 5.0f); 
    }
    public override void FixedUpdateNetwork()
    {
        if (Life.Expired(Runner))
            Runner.Despawn(Object);
        else
            transform.position += 5 * Runner.DeltaTime * transform.forward;
    }
}