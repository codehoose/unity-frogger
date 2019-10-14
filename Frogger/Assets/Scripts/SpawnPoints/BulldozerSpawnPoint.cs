public class BulldozerSpawnPoint : VehicleSpawnPoint
{
    void Awake()
    {
        Actions.Add(new SpawnVehicleAction(this, 32));
        Actions.Add(new WaitAction(4));
    }
}
