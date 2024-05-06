using UnityEngine;
using UnityEngine.UIElements;

namespace CustomElements
{
    [UxmlElement]
    public partial class EmojiIconElement : VisualElement
    {
        public EmojiIconElement()
        {
            generateVisualContent += GenerateVisualContent;
        }
        
        private void GenerateVisualContent(MeshGenerationContext mgc)
        {
            var top = 0;
            var left = 0f;
            var right = contentRect.width;
            var bottom = contentRect.height;
            
            var painter2D = mgc.painter2D;
            painter2D.lineWidth = 10.0f;
            painter2D.strokeColor = Color.white;
            painter2D.lineJoin = LineJoin.Bevel;
            painter2D.lineCap = LineCap.Round;

            painter2D.strokeGradient = new Gradient() {
                colorKeys = new GradientColorKey[] {
                    new GradientColorKey() { color = Color.red, time = 0.0f },
                    new GradientColorKey() { color = Color.blue, time = 1.0f }
                }
            };

            painter2D.BeginPath();
            
            painter2D.MoveTo(new Vector2((float)(left + (contentRect.width * 0.2)), (float)(top + contentRect.height * 0.1)));
            painter2D.LineTo(new Vector2((float)(left + (contentRect.width * 0.2)), (float)(bottom * 0.7)));
            
            painter2D.MoveTo(new Vector2((float)(right - (contentRect.width * 0.2)), (float)(top + contentRect.height * 0.1)));
            painter2D.LineTo(new Vector2((float)(right - (contentRect.width * 0.2)), (float)(bottom * 0.7)));
            
            painter2D.MoveTo(new Vector2((float)(left + (contentRect.width * 0.3)), (float)(bottom * 0.8)));
            painter2D.LineTo(new Vector2((float)(right - (contentRect.width * 0.3)), (float)(bottom * 0.8)));
            
           painter2D.Stroke();

       }
    }
}