using UnityEngine;
using UnityEngine.UIElements;

namespace CustomElements
{
    [UxmlElement]
    public partial class RombElement : VisualElement
    {

        public RombElement()
        {
            generateVisualContent += GenerateVisualContent;

            schedule.Execute(ChangeColorAnimation).Every(1000);
        }

        private void ChangeColorAnimation()
        {
        }

        Vertex[] vertices = new Vertex[4];
        ushort[] indices = { 0, 1, 2, 2, 3, 0 };

        private Color32 firstColor  = new Color32(255, 0, 0, 255);
        private Color32 secondColor  = new Color32(0, 255, 0, 255);
        private Color32 thirdColor  = new Color32(0, 0, 255, 255);
        private Color32 fourColor  = new Color32(17, 55, 55, 255);

        void GenerateVisualContent(MeshGenerationContext mgc)
        {
            vertices[0].tint = firstColor;
            vertices[1].tint = secondColor;
            vertices[2].tint = thirdColor;
            vertices[3].tint = fourColor;

            var top = 0;
            var left = 0f;
            var middleX = contentRect.width / 2;
            var middleY = contentRect.height / 2;
            var right = contentRect.width;
            var bottom = contentRect.height;

            vertices[0].position = new Vector3(left, middleY, Vertex.nearZ);
            vertices[1].position = new Vector3(middleX, top, Vertex.nearZ);
            vertices[2].position = new Vector3(right, middleY, Vertex.nearZ);
            vertices[3].position = new Vector3(middleX, bottom, Vertex.nearZ);

            MeshWriteData mwd = mgc.Allocate(vertices.Length, indices.Length);
            mwd.SetAllVertices(vertices);
            mwd.SetAllIndices(indices);
        }
    }
}