using UnityEngine;

public class TableTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (AssemblyTimerManager.Instance.isDone)
        {
            AssemblyTimerManager.Instance.StopTimer();
            AssemblyTimerManager.Instance.SetRecord();
        }
    }
}