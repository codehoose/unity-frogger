public class YellowBuggySpawnPoint : VehicleSpawnPoint
{
    void Awake()
    {
        Actions.Add(new SpawnVehicleAction(this, -32));
        Actions.Add(new WaitAction(2));
        Actions.Add(new SpawnVehicleAction(this, -32));
        Actions.Add(new WaitAction(2));
        Actions.Add(new SpawnVehicleAction(this, -32));
        Actions.Add(new WaitAction(3));
    }
}