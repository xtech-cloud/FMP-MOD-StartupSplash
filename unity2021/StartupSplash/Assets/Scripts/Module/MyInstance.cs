

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.StartupSplash.LIB.Proto;
using XTC.FMP.MOD.StartupSplash.LIB.MVCS;
using System.Collections;

namespace XTC.FMP.MOD.StartupSplash.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        public class UiReference
        {
            public Canvas splashCanvas;
            public RawImage imgSplash;
        }

        private UiReference uiReference_ = new UiReference();
        private GameObject triggerTick_ { get; set; }

        public MyInstance(string _uid, string _style, MyConfig _config, MyCatalog _catalog, LibMVCS.Logger _logger, Dictionary<string, LibMVCS.Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
            : base(_uid, _style, _config, _catalog, _logger, _settings, _entry, _mono, _rootAttachments)
        {
        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        /// <remarks>
        /// 可用于加载主题目录的数据
        /// </remarks>
        public void HandleCreated()
        {
            uiReference_.splashCanvas = new GameObject(string.Format("{0}#{1}#SplashCanvas", MyEntryBase.ModuleName, uid)).AddComponent<Canvas>();
            var canvasScaler = uiReference_.splashCanvas.gameObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.matchWidthOrHeight = 1.0f;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);

            uiReference_.imgSplash = new GameObject("ImageSplash").AddComponent<RawImage>();
            uiReference_.imgSplash.transform.SetParent(uiReference_.splashCanvas.transform);
            uiReference_.imgSplash.transform.localPosition = Vector3.zero;
            uiReference_.imgSplash.transform.localRotation = Quaternion.identity;
            uiReference_.imgSplash.transform.localScale = Vector3.one;
            var rtImageSplash = uiReference_.imgSplash.GetComponent<RectTransform>();
            rtImageSplash.anchorMin = Vector3.zero;
            rtImageSplash.anchorMax = Vector3.one;
            rtImageSplash.sizeDelta = Vector3.zero;

            if (style_.canvasOptions.renderMode == "Overlay")
            {
                uiReference_.splashCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            loadTextureFromTheme(style_.splash.image, (_texture) =>
            {
                uiReference_.imgSplash.texture = _texture;
            }, () =>
            {

            });

            uiReference_.splashCanvas.gameObject.SetActive(false);

            triggerTick_ = new GameObject(string.Format("{0}#{1}#TriggerTick", MyEntryBase.ModuleName, uid));
            var triggerTick = triggerTick_.AddComponent<TriggerTick>();
            triggerTick.uid = uid;
            triggerTick.model = entry_.getDummyModel();
            triggerTick.style = style_;
        }

        /// <summary>
        /// 当被删除时
        /// </summary>
        public void HandleDeleted()
        {
            GameObject.Destroy(triggerTick_);
        }

        /// <summary>
        /// 当被打开时
        /// </summary>
        /// <remarks>
        /// 可用于加载内容目录的数据
        /// </remarks>
        public void HandleOpened(string _source, string _uri)
        {
            rootUI.gameObject.SetActive(true);
            rootWorld.gameObject.SetActive(true);
            uiReference_.splashCanvas.gameObject.SetActive(true);
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);
            rootWorld.gameObject.SetActive(false);
            uiReference_.splashCanvas.gameObject.SetActive(false);
        }

    }
}
