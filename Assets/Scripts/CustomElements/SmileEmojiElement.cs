using UnityEngine;
using UnityEngine.UIElements;

namespace CustomElements
{
    public class SmileEmojiElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<SmileEmojiElement>
        {
        }

        public SmileEmojiElement()
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
            painter2D.lineWidth = 10.0f;
            painter2D.strokeColor = Color.yellow;
            painter2D.lineJoin = LineJoin.Bevel;
            painter2D.lineCap = LineCap.Round;


            painter2D.BeginPath();

            painter2D.MoveTo(new Vector2((float)(left + (contentRect.width * 0.2)), (float)(top + contentRect.height * 0.1)));
            painter2D.LineTo(new Vector2((float)(left + (contentRect.width * 0.2)), (float)(bottom * 0.7)));

            painter2D.MoveTo(new Vector2((float)(right - (contentRect.width * 0.2)), (float)(top + contentRect.height * 0.1)));
            painter2D.LineTo(new Vector2((float)(right - (contentRect.width * 0.2)), (float)(bottom * 0.7)));

            painter2D.MoveTo(new Vector2((float)(left + (contentRect.width * 0.3)), (float)(bottom * 0.8)));
            painter2D.BezierCurveTo(
                new Vector2((float)(left + (contentRect.width * 0.5)), (float)(bottom * 0.85)),
                new Vector2((float)(left + (contentRect.width * 0.45)), (float)(bottom * 0.88)),
                new Vector2((float)(right - (contentRect.width * 0.3)), (float)(bottom * 0.8)));

            painter2D.Stroke();

        }
    }
}