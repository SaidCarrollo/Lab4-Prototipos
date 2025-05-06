using System;
using UnityEngine;

public class UIEventManager : SingletonPersistent<UIEventManager>
{
    // Delegates
    public Action<Color> OnColorChange;
    public Action OnPause;
    public Action OnResume;
    public Action OnRestart;
    public Action OnGoToMenu;


    public Action<float> OnTimeUpdated;
}

