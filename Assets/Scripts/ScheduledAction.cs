using UnityEngine;
using System;

// Leave for future use, currently not used in the code
public class ScheduledAction
{
    private float executeAt;
    // The action to be executed
    private Action action;


    // Scheduled execution time
    public float ExecuteAt => executeAt;
    
    // Constructor to initialize the ScheduledAction with the time to execute and the action to perform
    public ScheduledAction(float executeAt, Action action)
    {
        this.executeAt = executeAt;
        this.action = action;
    }

    public void Execute()
    {
        action?.Invoke();
    }
}
