using System;
using UnityEngine;
using UnityEngine.UI;

namespace Experiments
{
    public class UIGradientMeshEffect : BaseMeshEffect
    {
        [Serializable]
        public enum Mode
        {
            Horizontal,
            Vertical
        }

        [SerializeField]
        private Gradient _gradient = new Gradient();
        [SerializeField]
        private Mode _mode = Mode.Horizontal;

        public override void ModifyMesh(VertexHelper vh)
        {
            UIVertex v0 = new UIVertex();
            UIVertex v1 = new UIVertex();
            UIVertex v2 = new UIVertex();
            UIVertex v3 = new UIVertex();

            vh.PopulateUIVertex(ref v0, 0);
            vh.PopulateUIVertex(ref v1, 1);
            vh.PopulateUIVertex(ref v2, 2);
            vh.PopulateUIVertex(ref v3, 3);

            float minPos = _mode == Mode.Horizontal ? v0.position.x : v0.position.y;
            float maxPos = _mode == Mode.Horizontal ? v2.position.x : v2.position.y;
            float rangePos = maxPos - minPos;

            float minU = _mode == Mode.Horizontal ? v0.uv0.x : v0.uv0.y;
            float maxU = _mode == Mode.Horizontal ? v2.uv0.x : v2.uv0.y;
            float rangeU = maxU - minU;

            vh.Clear();
            for (int i = 0; i < _gradient.colorKeys.Length - 1; ++i)
            {
                UIVertex ver0 = new UIVertex();
                UIVertex ver1 = new UIVertex();
                UIVertex ver2 = new UIVertex();
                UIVertex ver3 = new UIVertex();

                //bottom left
                ver0.position = v0.position;
                ver0.uv0 = v0.uv0;
                if (_mode == Mode.Horizontal)
                {
                    ver0.position.x = minPos + rangePos * _gradient.colorKeys[i].time;
                    ver0.uv0.x = minU + rangeU * _gradient.colorKeys[i].time;
                }
                else
                {
                    ver0.position.y = minPos + rangePos * _gradient.colorKeys[i].time;
                    ver0.uv0.y = minU + rangeU * _gradient.colorKeys[i].time;
                }

                ver0.color = _gradient.colorKeys[i].color;

                //top left
                ver1.position = v1.position;
                ver1.uv0 = v1.uv0;
                if (_mode == Mode.Horizontal)
                {
                    ver1.position.x = minPos + rangePos * _gradient.colorKeys[i].time;
                    ver1.uv0.x = minU + rangeU * _gradient.colorKeys[i].time;
                }
                else
                {
                    ver1.position.y = minPos + rangePos * _gradient.colorKeys[i + 1].time;
                    ver1.uv0.y = minU + rangeU * _gradient.colorKeys[i + 1].time;
                }

                ver1.color = _mode == Mode.Horizontal ? _gradient.colorKeys[i].color : _gradient.colorKeys[i + 1].color;

                // top right
                ver2.position = v2.position;
                ver2.uv0 = v2.uv0;
                if (_mode == Mode.Horizontal)
                {
                    ver2.position.x = minPos + rangePos * _gradient.colorKeys[i + 1].time;
                    ver2.uv0.x = minU + rangeU * _gradient.colorKeys[i + 1].time;
                }
                else
                {
                    ver2.position.y = minPos + rangePos * _gradient.colorKeys[i + 1].time;
                    ver2.uv0.y = minU + rangeU * _gradient.colorKeys[i + 1].time;
                }

                ver2.color = _gradient.colorKeys[i + 1].color;

                //bottom right
                ver3.position = v3.position;
                ver3.uv0 = v3.uv0;
                if (_mode == Mode.Horizontal)
                {
                    ver3.position.x = minPos + rangePos * _gradient.colorKeys[i + 1].time;
                    ver3.uv0.x = minU + rangeU * _gradient.colorKeys[i + 1].time;
                }
                else
                {
                    ver3.position.y = minPos + rangePos * _gradient.colorKeys[i].time;
                    ver3.uv0.y = minU + rangeU * _gradient.colorKeys[i].time;
                }

                ver3.color = _mode == Mode.Horizontal ? _gradient.colorKeys[i + 1].color : _gradient.colorKeys[i].color;

                vh.AddUIVertexQuad(new[] { ver0, ver1, ver2, ver3 });
            }
        }
    }
}