using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PanelCreator : MonoBehaviour
{
    [SerializeField]
    private RectTransform canvasTransform;

    [SerializeField]
    private float centerSize;

    public static PanelCreator Instance;

    public static GameObject panelC;

    private GridLayoutGroup gridLayoutGroup;

    Color panelcolor = new Color(255f, 255f, 255f, 255f);
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public GameObject panelCreator() {
        // Create a new GameObject with a RectTransform and Image component
        panelC = new GameObject("Selection Panel", typeof(RectTransform), typeof(Image));

        // Optional: Add color to the panel
        panelC.GetComponent<Image>().color = panelcolor;

        Vector2 anchorpoint = new Vector2(0.5f, 0.5f);

        //gridLayoutGroup = panelC.AddComponent<GridLayoutGroup>();

        // Set the panel's parent to be the Canvas
        RectTransform panelRect = panelC.GetComponent<RectTransform>();
        panelRect.SetParent(canvasTransform, false);


        // Center the panel and make it maintain a square aspect ratio
        panelRect.anchorMin = anchorpoint;
        panelRect.anchorMax = anchorpoint;
        panelRect.pivot = anchorpoint;
        panelRect.anchoredPosition = Vector2.zero;

        return panelC;
        //configuregridlayoutgroup();
    }

    void configuregridlayoutgroup() {

        gridLayoutGroup.padding = new RectOffset(35, 0, 80, 0);

        // Set cell size and spacing
        gridLayoutGroup.cellSize = new Vector2(900f, 900f);
        gridLayoutGroup.spacing = new Vector2(45f,0f);

        // Set alignment and layout constraints
        gridLayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
        gridLayoutGroup.childAlignment = TextAnchor.UpperLeft;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.Flexible;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
