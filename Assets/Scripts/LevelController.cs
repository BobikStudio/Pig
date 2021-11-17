using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public static LevelController Singeltone;

    public UnityEvent OnLevelRestarted;

    private void Awake()
    {
        if (Singeltone == null)
        {
            Singeltone = this;
        }
        else if (Singeltone != this)
        {
            Destroy(this);
        }
    }

    public void RestartLevel()
    {
        OnLevelRestarted?.Invoke();
    }
}
