public class PurpleCarSpawnPoint : VehicleSpawnPoint
{
    void Awake()
    {
        Actions.Add(new SpawnVehicleAction(this, -48));
        Actions.Add(new WaitAction(2));
        Actions.Add(new SpawnVehicleAction(this, -48));
        Actions.Add(new WaitAction(3));
    }
}
