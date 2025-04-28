using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class G {
    static GameSystem _I;
    public static GameSystem I {
        get {
            if (_I == null) {
                _I = GameObject.FindAnyObjectByType<GameSystem>();
            }
            return _I;
        }
    }
    public static UIMainState UI {
        get {
            return G.I.ui.state;
        }
    }
}

public partial class GameSystem : MonoBehaviour
{
    public GameObject UICanvas;
    public UIMain ui;
    public Camera mainCamera;
    public ControlMode controlMode;
    public ControlManager controls;
    public Dictionary<string, GameObject> gameObjectRegistry = new Dictionary<string, GameObject>();


    void Awake() {
        ui = UICanvas.GetComponent<UIMain>();
        ui.state = UIMain.DefaultState();

        mainCamera = Camera.main; // Camera.main is expensive so use G.I.mainCamera instead
    }
    public void Start()
    {
        SetControlMode(ControlMode.UI);
        SetGamePause(false);
    }

    public void Resume() {
        G.UI.uiType = UIType.HUD;
        G.UI.MarkModified();

        SetControlMode(ControlMode.Cooking);
        SetGamePause(false);
    }

    public bool IsPaused() {
        return Time.timeScale == 0;
    }
    public void SetGamePause(bool pause) {
        Time.timeScale = pause ? 0.0f : 1.0f;
    }

    public void SetControlMode(ControlMode mode) {
        void SetActionMapEnabled(InputActionMap actionMap, bool enabled) {
            if (enabled)
                actionMap.Enable();
            else
                actionMap.Disable();
        }

        var ui = controls.input.actions.FindActionMap("UI");
        var cooking = controls.input.actions.FindActionMap("Cooking");
        SetActionMapEnabled(ui, mode == ControlMode.UI);
        SetActionMapEnabled(cooking, mode == ControlMode.Cooking);

        controlMode = mode;
    }
    public Vector2 GetAbsoluteSize(RectTransform rectTransform)
    {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        var width = Mathf.Abs(corners[2].x - corners[0].x);
        var height = Mathf.Abs(corners[2].y - corners[0].y);

        return new Vector2(width, height);
    }

    public void RegisterGameObject(string id, GameObject obj) {
        Debug.Assert(!gameObjectRegistry.ContainsKey(id));
        gameObjectRegistry.Add(id, obj);
    }
    public void UnregisterGameObject(string id, GameObject obj)
    {
        Debug.Assert(gameObjectRegistry.ContainsKey(id), id + "does not exist");
        if (gameObjectRegistry[id] == obj) gameObjectRegistry.Remove(id);
    }

    public bool TryGetGameObjectById(string id, out GameObject obj) {
        obj = null;
        return !string.IsNullOrEmpty(id) && gameObjectRegistry.TryGetValue(id, out obj);
    }
}
