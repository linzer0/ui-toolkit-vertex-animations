using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomElements
{
    [UxmlElement]
    public partial class RombElement : VisualElement
    {
        private Time _startTime;
        private float timeLeft;

        private float AlphaValue = 255;
        private bool ReversedAnimation = false;
        private const float AnimationTime = 1f;
        
        public RombElement()
        {
            generateVisualContent += GenerateVisualContent;

            timeLeft = AnimationTime;
            schedule.Execute(ChangeColorAnimation).Every(16);
        }

        private void ChangeColorAnimation()
        {
            if (firstColor.a > 250 && !ReversedAnimation)
            {
                ReversedAnimation = true;
                AlphaValue = 0;
                timeLeft = AnimationTime;
            }
            else if (firstColor.a < 5 && ReversedAnimation)
            {
                AlphaValue = 255;
                timeLeft = AnimationTime;
                ReversedAnimation = false;
            }
            
            timeLeft -= Time.deltaTime;
            
            firstColor.a = (byte)Mathf.Lerp(firstColor.a, AlphaValue, Time.fixedDeltaTime / timeLeft);
            secondColor.a = (byte)Mathf.Lerp(secondColor.a, AlphaValue, Time.deltaTime / timeLeft);
            thirdColor.a = (byte)Mathf.Lerp(thirdColor.a, AlphaValue, Time.fixedDeltaTime / timeLeft);
            fourColor.a = (byte)Mathf.Lerp(fourColor.a, AlphaValue, Time.deltaTime / timeLeft);

            MarkDirtyRepaint();
        }

        Vertex[] vertices = new Vertex[4];
        ushort[] indices = { 0, 1, 2, 2, 3, 0 };

        private Color32 firstColor  = new (255, 0, 0, 0);
        private Color32 secondColor  = new (0, 255, 0, 0);
        private Color32 thirdColor  = new (0, 0, 255, 0);
        private Color32 fourColor  = new (17, 55, 55, 0);

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