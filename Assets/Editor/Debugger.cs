using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class Debugger : EditorWindow
{
    [MenuItem("Debug/Debugger")]
    public static void ShowExample()
    {
        Debugger wnd = GetWindow<Debugger>();
        wnd.titleContent = new GUIContent("Debugger");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        VisualElement label = new Label("CPU Status");
        root.Add(label);

        PropertyField a = new PropertyField();
        a.bindingPath = "emulator.cpu.a";
        a.label = "A";
        root.Add(a);

        PropertyField x = new PropertyField();
        x.bindingPath = "emulator.cpu.x";
        x.label = "X";
        root.Add(x);

        PropertyField y = new PropertyField();
        y.bindingPath = "emulator.cpu.y";
        y.label = "Y";
        root.Add(y);

        PropertyField pc = new PropertyField();
        pc.bindingPath = "emulator.cpu.pc";
        pc.label = "PC";
        root.Add(pc);

        PropertyField s = new PropertyField();
        s.bindingPath = "emulator.cpu.s";
        s.label = "S";
        root.Add(s);

        PropertyField p = new PropertyField();
        p.bindingPath = "emulator.cpu.p";
        s.label = "S";
        root.Add(p);

        var manager = FindFirstObjectByType<NES.NESManager>();
        var so = new SerializedObject(manager);
        root.Bind(so);
    }
}
