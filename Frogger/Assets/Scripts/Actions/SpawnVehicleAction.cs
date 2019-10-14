using System.Collections;

public class SpawnVehicleAction : IAction
{
    private VehicleSpawnPoint _spawn;
    private float _speed;
    private float _xMax;

    public SpawnVehicleAction(VehicleSpawnPoint spawn, float speed, float xMax = 130)
    {
        _spawn = spawn;
        _speed = speed;
        _xMax = xMax;
    }

    public IEnumerator Execute()
    {
        var poolObject = _spawn.GetNextVehicle();
        var component = poolObject.GetComponent<Vehicle>();
        component.Init(_spawn.transform.position, _speed, _xMax);
        yield return null;
    }
}

