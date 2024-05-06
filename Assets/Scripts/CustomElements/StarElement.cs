using UnityEngine.UIElements;
using UnityEngine;

namespace CustomElements
{
    public class StarElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<StarElement> { }

        public StarElement()
        {
            generateVisualContent += GenerateVisualContent;
        }

        private void GenerateVisualContent(MeshGenerationContext mgc)
        {
            var top = 0;
            var left = 0f;
            var middleX = contentRect.width / 2;
            var middleY = contentRect.height / 2;
            var right = contentRect.width;
            var bottom = contentRect.height;

            var painter2D = mgc.painter2D;

            painter2D.lineWidth = 3.0f;
            painter2D.strokeColor = Color.green;
            
            painter2D.lineJoin = LineJoin.Round;
            painter2D.lineCap = LineCap.Round;

            painter2D.BeginPath();
            painter2D.MoveTo(new Vector2(middleX, top));
            painter2D.LineTo(new Vector2((float)(right - contentRect.width * 0.2), bottom));
            painter2D.LineTo(new Vector2(left, (float)(middleY - contentRect.height * 0.2)));
            painter2D.LineTo(new Vector2(right,(float)(middleY - contentRect.height * 0.2)));
            painter2D.LineTo(new Vector2((float)(left + contentRect.width * 0.2), bottom));
            painter2D.LineTo(new Vector2(middleX, top));
            
            painter2D.Stroke();
        }
    }
}