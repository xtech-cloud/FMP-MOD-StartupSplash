
using System.Xml.Serialization;

namespace XTC.FMP.MOD.StartupSplash.LIB.Unity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class MyConfig : MyConfigBase
    {
        public class CanvasOptions
        {
            [XmlAttribute("renderMode")]
            public string renderMode { get; set; } = "Overlay";
        }

        public class Splash
        {
            [XmlAttribute("image")]
            public string image { get; set; } = "";
        }

        public class CameraTrigger
        {
            [XmlAttribute("active")]
            public bool active { get; set; } = false;
            [XmlAttribute("pitchMin")]
            public float pitchMin { get; set; } = 80;
            [XmlAttribute("pitchMax")]
            public float pitchMax { get; set; } = 90;
            [XmlAttribute("duration")]
            public float duration { get; set; } = 3;
        }

        public class KeyTrigger
        {
            [XmlAttribute("active")]
            public bool active { get; set; } = false;
            [XmlAttribute("keyCode")]
            public string keyCode { get; set; } = "Space";
        }


        public class Style
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";
            [XmlElement("CanvasOptions")]
            public CanvasOptions canvasOptions { get; set; } = new CanvasOptions();
            [XmlElement("Splash")]
            public Splash splash { get; set; } = new Splash();
            [XmlElement("CameraTrigger")]
            public CameraTrigger cameraTrigger { get; set; } = new CameraTrigger();
            [XmlElement("KeyTrigger")]
            public KeyTrigger keyTrigger { get; set; } = new KeyTrigger();
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}

