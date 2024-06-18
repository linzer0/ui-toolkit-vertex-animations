using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

namespace CustomElements
{
    [UxmlElement]
    public partial class RombElement : VisualElement
    {
        private float _colorAnimationTimeLeft;
        private float _positionAnimationTimeLeft;

        private const float AlphaValue = 255;

        [UxmlAttribute] public float AnimationTime = 3f;

        private Vertex[] vertices = new Vertex[4];
        private ushort[] indices = { 0, 1, 2, 2, 3, 0 };

        private Color32 firstColor = new(255, 0, 0, 0);
        private Color32 secondColor = new(0, 255, 0, 0);
        private Color32 thirdColor = new(0, 0, 255, 0);
        private Color32 fourColor = new(17, 55, 55, 0);

        [UxmlAttribute]
        public Vector3 firstVertexPosition = new Vector3(0, 50, Vertex.nearZ);
        private Vector3 secondVertexPosition;
        private Vector3 thirdVertexPosition;
        private Vector3 fourVertexPosition;

        private float Top = 0;
        private static float Left = 0f;
        private float Right => contentRect.width;
        private float Bottom => contentRect.height;

        private float MiddleX => contentRect.width / 2;
        private float MiddleY() => contentRect.height / 2;

        private bool _vertexsInited;

        public RombElement()
        {
            generateVisualContent += GenerateVisualContent;

            _colorAnimationTimeLeft = AnimationTime;
            _positionAnimationTimeLeft = AnimationTime;

        

            schedule.Execute(ChangeColorAnimation).Every(16);
            // schedule.Execute(ChangePositionAnimation).Every(16);
        }

        private void InitVertexs()
        {
            if (_vertexsInited == false)
            {
                
                _vertexsInited = true;
            }
        }

        private void ChangePositionAnimation()
        {
            _positionAnimationTimeLeft -= Time.deltaTime;
            if(!_vertexsInited)
                InitVertexs();
            
            firstVertexPosition = Vector3.Lerp(firstVertexPosition, new Vector3(MiddleX, Top, Vertex.nearZ), Time.fixedDeltaTime / _positionAnimationTimeLeft);
            // secondVertexPosition = Vector3.Lerp(secondVertexPosition, new Vector3(Right, MiddleY, Vertex.nearZ), Time.fixedDeltaTime / _positionAnimationTimeLeft);
            thirdVertexPosition = Vector3.Lerp(thirdVertexPosition, new Vector3(MiddleX, Bottom, Vertex.nearZ), Time.fixedDeltaTime / _positionAnimationTimeLeft);
            // fourVertexPosition = Vector3.Lerp(fourVertexPosition, new Vector3(Left, MiddleY, Vertex.nearZ), Time.fixedDeltaTime / _positionAnimationTimeLeft);
        }

        private void ChangeColorAnimation()
        {
            _colorAnimationTimeLeft -= Time.deltaTime;

            firstColor.a = (byte)Mathf.Lerp(firstColor.a, AlphaValue, Time.fixedDeltaTime / _colorAnimationTimeLeft);
            secondColor.a = (byte)Mathf.Lerp(secondColor.a, AlphaValue, Time.deltaTime / _colorAnimationTimeLeft);
            thirdColor.a = (byte)Mathf.Lerp(thirdColor.a, AlphaValue, Time.fixedDeltaTime / _colorAnimationTimeLeft);
            fourColor.a = (byte)Mathf.Lerp(fourColor.a, AlphaValue, Time.deltaTime / _colorAnimationTimeLeft);

            MarkDirtyRepaint();
        }

        void GenerateVisualContent(MeshGenerationContext mgc)
        {
            vertices[0].tint = firstColor;
            vertices[1].tint = secondColor;
            vertices[2].tint = thirdColor;
            vertices[3].tint = fourColor;
            
            secondVertexPosition = new Vector3(MiddleX, Top, Vertex.nearZ);
            thirdVertexPosition = new Vector3(Right, MiddleY(), Vertex.nearZ);
            fourVertexPosition = new Vector3(MiddleX, Bottom, Vertex.nearZ);
            
            vertices[0].position = firstVertexPosition;
            vertices[1].position = secondVertexPosition;
            vertices[2].position = thirdVertexPosition;
            vertices[3].position = fourVertexPosition;

            MeshWriteData mwd = mgc.Allocate(vertices.Length, indices.Length);
            mwd.SetAllVertices(vertices);
            mwd.SetAllIndices(indices);
        }
    }
}