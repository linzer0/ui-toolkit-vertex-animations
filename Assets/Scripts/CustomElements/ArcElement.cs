using UnityEngine;
using UnityEngine.UIElements;

namespace CustomElements
{
    public class ArcElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ArcElement>
        {
        }

        public ArcElement()
        {
            generateVisualContent += GenerateVisualContent;
        }
        

        private void GenerateVisualContent(MeshGenerationContext mgc)
        {
            var painter = mgc.painter2D;
            painter.lineWidth = 10.0f;

            painter.BeginPath();
            painter.strokeColor = Color.red;
            painter.MoveTo(new Vector2(50, 50));
            painter.LineTo(new Vector2(100, 100));
            painter.Stroke();
            painter.ClosePath();
            
            painter.BeginPath();
            painter.MoveTo(new Vector2(20, 20));
            painter.LineTo(new Vector2(60, 60));
            painter.strokeColor = Color.blue;
            painter.Stroke();
            painter.ClosePath();
            
            painter.BeginPath();
            painter.MoveTo(new Vector2(50, 150));
            painter.LineTo(new Vector2(100, 200));
            painter.LineTo(new Vector2(150, 150));
            painter.fillColor = Color.green;
            painter.strokeGradient = new Gradient()
            {
                colorKeys = new GradientColorKey[]
                {
                    new() { color = Color.red, time = 0.0f },
                    new() { color = Color.blue, time = 1.0f }
                }
            };
            painter.Fill();
            painter.Stroke();
            
            painter.ClosePath();
        }
    }
}
