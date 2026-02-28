using System.Collections.Generic;

public class ControllersUpdateService
{
    private List<Controller> _controllers = new();

    public void Update(float deltaTime)
    {
        foreach (var controller in _controllers)
        {
            controller.Update(deltaTime);
        }
    }
    //TODO сделать удаление и причину
    public void Add(Controller controller)
    {
        _controllers.Add(controller);
    }
}
