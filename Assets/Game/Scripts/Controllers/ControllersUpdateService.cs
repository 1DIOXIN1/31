using System;
using System.Collections.Generic;

public class ControllersUpdateService
{
    private List<ControllerToRemoveReason> _controllers = new();

    public void Update(float deltaTime)
    {
        _controllers.RemoveAll(controller => controller.RemoveReason.Invoke());

        foreach (var controller in _controllers)
            controller.Controller.Update(deltaTime);
    }

    public void Add(Controller controller, Func<bool> removeReason)
    {
        _controllers.Add(new ControllerToRemoveReason(controller, removeReason));
    }

    private class ControllerToRemoveReason
    {
        public ControllerToRemoveReason(Controller controller, Func<bool> removeReason)
        {
            Controller = controller;
            RemoveReason = removeReason;
        }

        public Controller Controller { get; }
        public Func<bool> RemoveReason { get; }
    }
}
