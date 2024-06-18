using UnityEngine.UIElements;

namespace CustomElements
{
    [UxmlElement]
    public partial class ImageAnimation : VisualElement
    {
        [UxmlAttribute] private float AngleLimit = 15;
        [UxmlAttribute] private bool PlayAnimation = true;
        
        private bool _rotationDirectionIsMinus = true;
        
        public ImageAnimation()
        {
            schedule.Execute(Animation).Every(16);
        }

        void Animation()
        {
            if (!PlayAnimation)
                return;
            
            var newRotate = style.rotate;
            Angle rotateAngle = newRotate.value.angle;

            if (_rotationDirectionIsMinus)
            {
                rotateAngle.value--;
                if (rotateAngle.value < -AngleLimit)
                {
                    _rotationDirectionIsMinus = false;
                }
            }
            else if (_rotationDirectionIsMinus == false)
            {
                rotateAngle.value++;
                 if (rotateAngle.value > AngleLimit)
                 {
                     _rotationDirectionIsMinus = true;
                 }
            }
            
            newRotate.value = new Rotate(rotateAngle);
            style.rotate = newRotate;
            
            MarkDirtyRepaint();
        }
    }
}