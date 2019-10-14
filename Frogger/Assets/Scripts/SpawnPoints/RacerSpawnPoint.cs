public class RacerSpawnPoint : VehicleSpawnPoint
{
    void Awake()
    {
        Actions.Add(new SpawnVehicleAction(this, 96));
        Actions.Add(new WaitAction(3));
    }
}
