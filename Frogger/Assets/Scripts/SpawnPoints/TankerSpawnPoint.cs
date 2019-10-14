public class TankerSpawnPoint : VehicleSpawnPoint
{
    void Awake()
    {
        Actions.Add(new SpawnVehicleAction(this, -16));
        Actions.Add(new WaitAction(10));
    }
}
