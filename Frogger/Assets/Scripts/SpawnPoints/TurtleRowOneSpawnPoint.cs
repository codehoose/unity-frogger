public class TurtleRowOneSpawnPoint : VehicleSpawnPoint
{
    public float speed = 32;

    public float wait = 2.5f;

    void Awake()
    {
        Actions.Add(new SpawnVehicleAction(this, speed));
        Actions.Add(new WaitAction(wait));
    }
}
