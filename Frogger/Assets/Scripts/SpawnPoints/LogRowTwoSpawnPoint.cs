public class LogRowTwoSpawnPoint : VehicleSpawnPoint
{
    public float speed = 48;

    void Awake()
    {
        Actions.Add(new SpawnVehicleAction(this, speed, 200));
        Actions.Add(new WaitAction(3));
    }
}
