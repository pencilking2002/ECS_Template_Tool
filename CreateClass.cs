//Author: APMIX
 //Put this in Assets/Editor Folder
 using UnityEditor;
 using UnityEngine;
 using System.Collections;
 using System;
 using System.IO;
 using System.Text;
 using UnityEditor.Experimental.UIElements;
 using UnityEngine.Experimental.UIElements;
 using UnityEngine.Experimental.UIElements.StyleEnums;

public class CreateClassWindow : EditorWindow
 {
     private string cdFileName;
     private string sFileName;
     private TextField cdFileNameTexField;
     private TextField csFileNameTexField;

     [MenuItem("Tools/ESC File Creator")]
     private static void Init()
     {
         CreateClassWindow createClassWindow = (CreateClassWindow)EditorWindow.GetWindow(typeof(CreateClassWindow));
         createClassWindow.Show();

     }
     private void OnEnable()
     {
        CreateClassWindow createClassWindow = (CreateClassWindow)EditorWindow.GetWindow(typeof(CreateClassWindow));

        if (createClassWindow == null)
            return;

        // Create a root and define stylesheet
        var root = this.GetRootVisualContainer();
        root.AddStyleSheetPath("Stylesheets/styles");

        var column = new VisualElement() {name = "column"};
        column.AddToClassList("column");

        // Define the first row
        var row1 = new VisualElement() {name = "row1"};
        row1.AddToClassList("container");
        row1.Add(new Label() {text = "Component Data"});

        cdFileNameTexField = new TextField() {value = ""};
        row1.Add(cdFileNameTexField);
        var btn = new Button(onClickCreateComponentData) {text = "Create"};
        row1.Add(btn);

        // Define the second row
        var row2 = new VisualElement() {name = "row2"};
        row2.AddToClassList("container");
        row2.Add(new Label() {text = "Component System"});

        csFileNameTexField = new TextField() {value = ""};
        row2.Add(csFileNameTexField);
        var btn2 = new Button(onClickCreateComponentSystem) {text = "Create"};
        row2.Add(btn2);

        column.Add(row1);
        column.Add(row2);
        root.Add(column);

     }

     private void onClickCreateComponentData()
     {
         Debug.Log("Hello");
         CreateComponentData(cdFileNameTexField.value);
     }

     private void onClickCreateComponentSystem()
     {
         Debug.Log("Hello");
         CreateComponentSystem(csFileNameTexField.value);
     }


     static void CreateComponentData(string fileName)
     {
         var name = fileName;

         if (string.IsNullOrEmpty(name) || name == "File Name" || name.Contains(" "))
         {
             Debug.LogError("You must provide a file name");
             return;
         }

         string copyPath = "Assets/"+name+".cs";
         Debug.Log("Creating Classfile: " + copyPath);
         if( File.Exists(copyPath) == false ){ // do not overwrite
             var structName  = name.Replace("Component", string.Empty);
             using (StreamWriter outfile =
                 new StreamWriter(copyPath))
                 {

                     outfile.WriteLine("using System;");
                     outfile.WriteLine("using Unity.Entities;");
                     outfile.WriteLine("");
                     outfile.WriteLine("namespace IslandHopper.Game");
                     outfile.WriteLine("{");
                     outfile.WriteLine("    [Serializable]");

                     outfile.WriteLine("    public struct "+ structName +" : IComponentData");
                     outfile.WriteLine("    {");
                     outfile.WriteLine("    }");
                     outfile.WriteLine(" ");
                     outfile.WriteLine("    public class " + name + " : ComponentDataWrapper<" + structName + "> {}");
                     outfile.WriteLine("}");

             }
         }
         AssetDatabase.Refresh();
     }

     static void CreateComponentSystem(string fileName)
     {
         var name = fileName;

         if (string.IsNullOrEmpty(name) || name == "File Name" || name.Contains(" "))
         {
             Debug.LogError("You must provide a valid file name");
             return;
         }
         string copyPath = "Assets/"+name+".cs";
         Debug.Log("Creating Classfile: " + copyPath);
         if( File.Exists(copyPath) == false ){ // do not overwrite
             using (StreamWriter outfile =
                 new StreamWriter(copyPath))
             {

                 outfile.WriteLine("using Unity.Entities;");
                 outfile.WriteLine("using UnityEngine;");
                 outfile.WriteLine("");
                 outfile.WriteLine("namespace IslandHopper.Game");
                 outfile.WriteLine("{");

                 outfile.WriteLine("    public class "+ fileName +" : ComponentSystem");
                 outfile.WriteLine("    {");
                 outfile.WriteLine("        protected override void OnCreateManager()");
                 outfile.WriteLine("        {");
                 outfile.WriteLine("        }");
                 outfile.WriteLine("");
                 outfile.WriteLine("        protected override void OnUpdate()");
                 outfile.WriteLine("        {");
                 outfile.WriteLine("        }");
                 outfile.WriteLine("    }");
                 outfile.WriteLine("}");


             }
         }
         AssetDatabase.Refresh();
     }

 }
