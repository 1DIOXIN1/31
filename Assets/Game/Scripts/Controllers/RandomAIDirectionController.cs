using UnityEngine;

public class RandomAIDirectionController : Controller
{
    private float _time = 0;
    private float _timeToChangeDirection;
    private IDirectionMovable _movable;
    private IDirectionRotatable _rotatable;
    private Vector3 _currentDirection;

    public RandomAIDirectionController(IDirectionMovable movable, IDirectionRotatable rotatable, float timeToChangeDirection)
    {
        _movable = movable;
        _rotatable = rotatable;
        _timeToChangeDirection = timeToChangeDirection;
        _time = timeToChangeDirection;
    }

    public override void UpdateLogic(float deltaTime)
    {
        _time += deltaTime;

        if(_time >= _timeToChangeDirection)
        {
            _time = 0;
            _currentDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        }

        _movable.SetMoveDirection(_currentDirection);
        _rotatable.SetRotationDirection(_currentDirection);
    }
}
