using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System;
using System.IO;
using UnityEngine.Events;
using Treegen;


[CustomEditor(typeof(TreegenTreeGenerator))]
public class TreegenTreeGeneratorEditor : Editor
{
    #region SerializedProperties

    SerializedProperty RandomGenerating;
    SerializedProperty OneCommonMaterial;
    SerializedProperty TreeMaterial;
    SerializedProperty TrunkMaterial;
    SerializedProperty LeavesMaterial;
    SerializedProperty Seed;
    SerializedProperty TreeNoiseForce;
    SerializedProperty TreeNoiseSeed;
    SerializedProperty MeshSmoothShading;
    SerializedProperty MeshSmoothShadingAngle;
    SerializedProperty TrunkIterations;
    SerializedProperty Parts;
    SerializedProperty Segments;
    SerializedProperty SkipBranch;
    SerializedProperty HideOnLeaves;
    SerializedProperty TrunkSegmentLength;
    SerializedProperty TrunkLevelLength;
    SerializedProperty TrunkThickness;
    SerializedProperty MaxBranch;
    SerializedProperty MinBranch;
    SerializedProperty Twirl;
    SerializedProperty BranchSegments;
    SerializedProperty SkipSubBranch;
    SerializedProperty BranchThickness;
    SerializedProperty MaxSubBranch;
    SerializedProperty MinSubBranch;
    SerializedProperty InnerTwirl;
    SerializedProperty RootIterations;
    SerializedProperty RootSegments;
    SerializedProperty SkipRootBranch;
    SerializedProperty RootBranchCount;
    SerializedProperty RootThickness;
    SerializedProperty RootSegmentLength;
    SerializedProperty RootLevelLength;
    SerializedProperty RootTwirl;
    SerializedProperty RootNoiseForce;
    SerializedProperty RootNoiseSeed;
    SerializedProperty Leaves;
    SerializedProperty LeavesType;
    SerializedProperty Mesh;
    SerializedProperty StartIteration;
    SerializedProperty EndIteration;
    SerializedProperty StartSegment;
    SerializedProperty CountSegment;
    SerializedProperty LeavesScale;
    SerializedProperty LeavesScaleCurve;
    SerializedProperty LeavesScaleSeg;
    SerializedProperty LeavesOffsetCurve;
    SerializedProperty LeavesTurnaroundStrength;
    SerializedProperty LeavesRandomTurnaround;
    SerializedProperty LeavesOffset;
    SerializedProperty LeavesDetail;
    SerializedProperty LeavesNoiseForce;
    SerializedProperty LeavesNoiseSeed;

    private const string globalGroupKey = "GlobalGroup";
    private const string trunkGroupKey = "TrunkGroup";
    private const string branchGroupKey = "BranchGroup";
    private const string rootGroupKey = "RootGroup";
    private const string leavesGroupKey = "LeavesGroup";
    private const string saveGroupKey = "SaveGroup";

    bool GlobalGroup = false;
    bool TrunkGroup = false;
    bool BranchGroup = false;
    bool RootGroup = false;
    bool LeavesGroup = false;
    bool SaveGroup = false;
    private bool showPreviewButton = true; // Initially show the preview button
    private float _lastAngle = 0;
    
    #endregion

    private void OnEnable()
    {
        RandomGenerating = serializedObject.FindProperty("RandomGenerating");
        OneCommonMaterial = serializedObject.FindProperty("OneCommonMaterial");
        TreeMaterial = serializedObject.FindProperty("TreeMaterial");
        TrunkMaterial = serializedObject.FindProperty("TrunkMaterial");
        LeavesMaterial = serializedObject.FindProperty("LeavesMaterial");
        Seed = serializedObject.FindProperty("Seed");
        TreeNoiseForce = serializedObject.FindProperty("TreeNoiseForce");
        TreeNoiseSeed = serializedObject.FindProperty("TreeNoiseSeed");
        MeshSmoothShading = serializedObject.FindProperty("MeshSmoothShading");
        MeshSmoothShadingAngle = serializedObject.FindProperty("MeshSmoothShadingAngle");
        TrunkIterations = serializedObject.FindProperty("TrunkIterations");
        Parts = serializedObject.FindProperty("Parts");
        Segments = serializedObject.FindProperty("Segments");
        SkipBranch = serializedObject.FindProperty("SkipBranch");
        TrunkSegmentLength = serializedObject.FindProperty("TrunkSegmentLength");
        TrunkLevelLength = serializedObject.FindProperty("TrunkLevelLength");
        TrunkThickness = serializedObject.FindProperty("TrunkThickness");
        MaxBranch = serializedObject.FindProperty("MaxBranch");
        MinBranch = serializedObject.FindProperty("MinBranch");
        Twirl = serializedObject.FindProperty("Twirl");
        BranchSegments = serializedObject.FindProperty("BranchSegments");
        SkipSubBranch = serializedObject.FindProperty("SkipSubBranch");
        BranchThickness = serializedObject.FindProperty("BranchThickness");
        MaxSubBranch = serializedObject.FindProperty("MaxSubBranch");
        MinSubBranch = serializedObject.FindProperty("MinSubBranch");
        InnerTwirl = serializedObject.FindProperty("InnerTwirl");
        RootIterations = serializedObject.FindProperty("RootIterations");
        RootSegments = serializedObject.FindProperty("RootSegments");
        SkipRootBranch = serializedObject.FindProperty("SkipRootBranch");
        RootBranchCount = serializedObject.FindProperty("RootBranchCount");
        RootThickness = serializedObject.FindProperty("RootThickness");
        RootSegmentLength = serializedObject.FindProperty("RootSegmentLength");
        RootLevelLength = serializedObject.FindProperty("RootLevelLength");
        RootTwirl = serializedObject.FindProperty("RootTwirl");
        RootNoiseForce = serializedObject.FindProperty("RootNoiseForce");
        RootNoiseSeed = serializedObject.FindProperty("RootNoiseSeed");
        Leaves = serializedObject.FindProperty("Leaves");
        LeavesType = serializedObject.FindProperty("LeavesType");
        Mesh = serializedObject.FindProperty("Mesh");
        StartIteration = serializedObject.FindProperty("StartIteration");
        EndIteration = serializedObject.FindProperty("EndIteration");
        StartSegment = serializedObject.FindProperty("StartSegment");
        CountSegment = serializedObject.FindProperty("CountSegment");
        LeavesScale = serializedObject.FindProperty("LeavesScale");
        LeavesScaleCurve = serializedObject.FindProperty("LeavesScaleCurve");
        LeavesScaleSeg = serializedObject.FindProperty("LeavesScaleSeg");
        LeavesOffsetCurve = serializedObject.FindProperty("LeavesOffsetCurve");
        LeavesRandomTurnaround = serializedObject.FindProperty("LeavesRandomTurnaround");
        LeavesTurnaroundStrength = serializedObject.FindProperty("LeavesTurnaroundStrength");
        LeavesOffset = serializedObject.FindProperty("LeavesOffset");
        LeavesDetail = serializedObject.FindProperty("LeavesDetail");
        LeavesNoiseForce = serializedObject.FindProperty("LeavesNoiseForce");
        LeavesNoiseSeed = serializedObject.FindProperty("LeavesNoiseSeed");
    }

    public override void OnInspectorGUI()
    {
        TreegenTreeGenerator _treegenTreeGenerator = (TreegenTreeGenerator)target;

        serializedObject.Update();

        EditorGUILayout.Space(5);
        if (GUILayout.Button(new GUIContent("Random Generating")))
        {
            _treegenTreeGenerator.RandomTreeGenerating();
        }
        EditorGUILayout.Space(5);

        GlobalGroup = EditorPrefs.GetBool(globalGroupKey);
        GlobalGroup = EditorGUILayout.BeginFoldoutHeaderGroup(GlobalGroup, "Global");
        if (GlobalGroup)
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.PropertyField(OneCommonMaterial);
            if (_treegenTreeGenerator.OneCommonMaterial)
            {
                EditorGUILayout.PropertyField(TreeMaterial);
            }
            else
            {
                EditorGUILayout.PropertyField(TrunkMaterial);
                EditorGUILayout.PropertyField(LeavesMaterial);
            }
            EditorGUILayout.PropertyField(Seed); 
            EditorGUILayout.PropertyField(TreeNoiseForce);
            EditorGUILayout.PropertyField(TreeNoiseSeed);
            EditorGUILayout.PropertyField(MeshSmoothShading);
            if (_treegenTreeGenerator.MeshSmoothShading)
            {
                EditorGUILayout.PropertyField(MeshSmoothShadingAngle);
                if (_lastAngle != _treegenTreeGenerator.MeshSmoothShadingAngle)
                {
                    showPreviewButton = true;
                    _lastAngle = _treegenTreeGenerator.MeshSmoothShadingAngle;
                    //showPreviewButton = false;
                    //_treegenTreeGenerator.SmoothPreviewShow();
                }

                if (_treegenTreeGenerator._newGenWaiting)
                {
                    showPreviewButton = true;
                }
                
                if (showPreviewButton)
                {
                    if (GUILayout.Button(new GUIContent("Smoothing preview", "Click to show the smoothing preview [Only to save the Mesh]")))
                    {
                        _treegenTreeGenerator.SmoothPreviewShow();
                        showPreviewButton = false;
                    }
                }
                else
                {
                    if (GUILayout.Button(new GUIContent("Close Preview", "Click to close the preview [Only to save the Mesh]")))
                    {
                        _treegenTreeGenerator.SmoothPreviewClose();
                        showPreviewButton = true;
                    }
                }
            }
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.Space(5);
        }
        EditorPrefs.SetBool(globalGroupKey, GlobalGroup);
        EditorGUILayout.EndFoldoutHeaderGroup();

        TrunkGroup = EditorPrefs.GetBool(trunkGroupKey);
        TrunkGroup = EditorGUILayout.BeginFoldoutHeaderGroup(TrunkGroup, "Trunk");
        if (TrunkGroup)
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.PropertyField(TrunkIterations);
            EditorGUILayout.PropertyField(Parts);
            EditorGUILayout.PropertyField(Segments);
            EditorGUILayout.PropertyField(SkipBranch);
            EditorGUILayout.PropertyField(TrunkSegmentLength);
            EditorGUILayout.PropertyField(TrunkLevelLength);
            EditorGUILayout.PropertyField(TrunkThickness);
            EditorGUILayout.PropertyField(MaxBranch);
            EditorGUILayout.PropertyField(MinBranch);
            EditorGUILayout.PropertyField(Twirl);
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.Space(5);
        }
        EditorPrefs.SetBool(trunkGroupKey, TrunkGroup);
        EditorGUILayout.EndFoldoutHeaderGroup();

        BranchGroup = EditorPrefs.GetBool(branchGroupKey);
        BranchGroup = EditorGUILayout.BeginFoldoutHeaderGroup(BranchGroup, "Branch");
        if (BranchGroup)
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.PropertyField(BranchSegments);
            EditorGUILayout.PropertyField(SkipSubBranch);
            EditorGUILayout.PropertyField(BranchThickness);
            EditorGUILayout.PropertyField(MaxSubBranch);
            EditorGUILayout.PropertyField(MinSubBranch);
            EditorGUILayout.PropertyField(InnerTwirl);
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.Space(5);
        }
        EditorPrefs.SetBool(branchGroupKey, BranchGroup);
        EditorGUILayout.EndFoldoutHeaderGroup();

        RootGroup = EditorPrefs.GetBool(rootGroupKey);
        RootGroup = EditorGUILayout.BeginFoldoutHeaderGroup(RootGroup, "Root");
        if (RootGroup)
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.PropertyField(RootIterations);
            EditorGUILayout.PropertyField(RootSegments);
            EditorGUILayout.PropertyField(SkipRootBranch);
            EditorGUILayout.PropertyField(RootBranchCount);
            EditorGUILayout.PropertyField(RootThickness);
            EditorGUILayout.PropertyField(RootSegmentLength);
            EditorGUILayout.PropertyField(RootLevelLength);
            EditorGUILayout.PropertyField(RootTwirl);
            EditorGUILayout.PropertyField(RootNoiseForce);
            EditorGUILayout.PropertyField(RootNoiseSeed);
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.Space(5);
        }
        EditorPrefs.SetBool(rootGroupKey, RootGroup);
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        LeavesGroup = EditorPrefs.GetBool(leavesGroupKey);
        LeavesGroup = EditorGUILayout.BeginFoldoutHeaderGroup(LeavesGroup, "Leaves");
        if (LeavesGroup)
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.PropertyField(Leaves);
            if(_treegenTreeGenerator.Leaves)
            {
                EditorGUILayout.PropertyField(LeavesType);
                EditorGUILayout.PropertyField(Mesh);
                EditorGUILayout.PropertyField(StartIteration);
                EditorGUILayout.PropertyField(EndIteration);
                EditorGUILayout.PropertyField(StartSegment);
                EditorGUILayout.PropertyField(CountSegment);
                EditorGUILayout.PropertyField(LeavesScale);
                EditorGUILayout.PropertyField(LeavesScaleCurve);
                EditorGUILayout.PropertyField(LeavesScaleSeg);
                EditorGUILayout.PropertyField(LeavesOffsetCurve);
                EditorGUILayout.PropertyField(LeavesRandomTurnaround);
                if (_treegenTreeGenerator.LeavesRandomTurnaround)
                { 
                EditorGUILayout.PropertyField(LeavesTurnaroundStrength);
                }
                EditorGUILayout.PropertyField(LeavesOffset);
                EditorGUILayout.PropertyField(LeavesDetail);
                EditorGUILayout.PropertyField(LeavesNoiseForce);
                EditorGUILayout.PropertyField(LeavesNoiseSeed);
            }
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.Space(5);
        }
        EditorPrefs.SetBool(leavesGroupKey, LeavesGroup);
        EditorGUILayout.EndFoldoutHeaderGroup();
        

        serializedObject.ApplyModifiedProperties();

        SaveGroup = EditorPrefs.GetBool(saveGroupKey);
        SaveGroup = EditorGUILayout.BeginFoldoutHeaderGroup(SaveGroup, "Save");

        if (SaveGroup)
        {
            
            if (GUILayout.Button(new GUIContent("Save Mesh", "Save Mesh to asset")))
            {
                string path = EditorUtility.SaveFilePanelInProject(
                "Save Mesh to asset",
                _treegenTreeGenerator.name + ".asset",
                "asset", "Please enter a file name to save the mesh to");
                if (path.Length != 0)
                {
                path = path.Replace(Application.dataPath, "Assets");
                Mesh m = _treegenTreeGenerator.NewGen();
                if (_treegenTreeGenerator.MeshSmoothShading)
                {
                    m = _treegenTreeGenerator.Smooth(m);
                }
                AssetDatabase.CreateAsset(m, path);
                m = (Mesh)AssetDatabase.LoadAssetAtPath(path, typeof(Mesh));
                _treegenTreeGenerator.GetComponent<MeshFilter>().sharedMesh = m;
                }
            }

            if (GUILayout.Button(new GUIContent("Save Trunc Mesh", "Save Trunc Mesh to asset")))
            {
                string path = EditorUtility.SaveFilePanelInProject(
                "Save Trunc Mesh to asset",
                _treegenTreeGenerator.name + " (Trunc)" + ".asset",
                "asset", "Please enter a file name to save the mesh to");
                if (path.Length != 0)
                {
                path = path.Replace(Application.dataPath, "Assets");
                Mesh m = _treegenTreeGenerator.NewGenTrunc();
                if (_treegenTreeGenerator.MeshSmoothShading)
                {
                    m = _treegenTreeGenerator.Smooth(m);
                }
                AssetDatabase.CreateAsset(m, path);
                m = (Mesh)AssetDatabase.LoadAssetAtPath(path, typeof(Mesh));
                _treegenTreeGenerator.GetComponent<MeshFilter>().sharedMesh = m;
                }
            }
        
            if (_treegenTreeGenerator.CountSegment > 0)
            {
                if (GUILayout.Button(new GUIContent("Save Leaves Mesh", "Save Leaves Mesh to asset")))
                {
                string path = EditorUtility.SaveFilePanelInProject(
                "Save Leaves Mesh to asset",
                _treegenTreeGenerator.name + " (Leaves)" + ".asset",
                "asset", "Please enter a file name to save the mesh to");
                if (path.Length != 0)
                {
                    path = path.Replace(Application.dataPath, "Assets");
                    Mesh m = _treegenTreeGenerator.NewGenLeaves();
                    if (_treegenTreeGenerator.MeshSmoothShading)
                    {
                        m = _treegenTreeGenerator.Smooth(m);
                    }
                    AssetDatabase.CreateAsset(m, path);
                    m = (Mesh)AssetDatabase.LoadAssetAtPath(path, typeof(Mesh));
                    _treegenTreeGenerator.GetComponent<MeshFilter>().sharedMesh = m;
                }
                }
            }
        }
        EditorPrefs.SetBool(saveGroupKey, SaveGroup);
        EditorGUILayout.EndFoldoutHeaderGroup();
        serializedObject.ApplyModifiedProperties();
        
    }

}
