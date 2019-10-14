public class LogRowOneSpawnPoint : VehicleSpawnPoint
{
    void Awake()
    {
        Actions.Add(new SpawnVehicleAction(this, 32, 200));
        Actions.Add(new WaitAction(3));
    }
}
