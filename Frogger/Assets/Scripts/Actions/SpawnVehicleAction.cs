using System.Collections;

public class SpawnVehicleAction : IAction
{
    private VehicleSpawnPoint _spawn;
    private float _speed;

    public SpawnVehicleAction(VehicleSpawnPoint spawn, float speed)
    {
        _spawn = spawn;
        _speed = speed;
    }

    public IEnumerator Execute()
    {
        var poolObject = _spawn.GetNextVehicle();
        var component = poolObject.GetComponent<Vehicle>();
        component.Init(_spawn.transform.position, _speed);
        yield return null;
    }
}

