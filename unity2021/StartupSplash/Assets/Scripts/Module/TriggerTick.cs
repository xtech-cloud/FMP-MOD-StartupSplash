using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace XTC.FMP.MOD.StartupSplash.LIB.Unity
{
    public class TriggerTick : MonoBehaviour
    {
        public string uid { get; set; }
        public MyConfig.Style style { get; set; }
        public DummyModel model { get; set; }

        private enum Status
        {
            OPEN,
            CLOSE
        }

        private Status status = Status.OPEN;
        private float cameraTimer = 0;

        private void Update()
        {
            if (style.cameraTrigger.active)
            {
                float pitch = Camera.main.transform.rotation.eulerAngles.x;
                if (pitch > style.cameraTrigger.pitchMin && pitch < style.cameraTrigger.pitchMax)
                {
                    cameraTimer += Time.deltaTime;
                    if (cameraTimer > style.cameraTrigger.duration)
                    {
                        changeState();
                        cameraTimer = 0;
                    }
                }
                else
                {
                    cameraTimer = 0;
                }
            }

            if (style.keyTrigger.active)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    changeState();
                }
            }
        }

        private void changeState()
        {
            if (status == Status.OPEN)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["uid"] = uid;
                data["delay"] = 0f;
                model.Publish(MySubjectBase.Close, data);
                status = Status.CLOSE;
            }
            else
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["uid"] = uid;
                data["source"] = "";
                data["uri"] = "";
                data["delay"] = 0f;
                model.Publish(MySubjectBase.Open, data);
                status = Status.OPEN;
            }
        }
    }
}
