using UnityEngine;
using UnityEngine.UI;
using VizConnectC;

public class Viz : MonoBehaviour
{
    VizEnginePool myVizEngine = new VizEnginePool();
    Text var_Text;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectToViz()
    {
        myVizEngine.Disconnect();
        myVizEngine.CleanRendererList();
        myVizEngine.AddRenderer("localhost", 6100);
        myVizEngine.Connect();
    }

    public void ActionButtonClick()
    {
        myVizEngine.Send("SCENE*/AT/Development/Tutorial*Tree$G_ALL... SET" + var_Text);
        //https://www.youtube.com/watch?v=gqQhKTn6xSQ&t=88s 
    }
}
