using UnityEngine.UIElements;

namespace CustomElements
{
    [UxmlElement]
    public partial class ImageAnimation : VisualElement
    {
        [UxmlAttribute] private float AngleLimit = 25;
        [UxmlAttribute] private bool AnimationIsStopped = true;
        
        private bool _rotationDirectionIsMinus = true;
        
        public ImageAnimation()
        {
            schedule.Execute(PlayAnimation).Every(16);
        }

        private void PlayAnimation()
        {
            if (AnimationIsStopped)
                return;
            
            RotateBell();
            MarkDirtyRepaint();
        }

        void RotateBell()
        {
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
        }
    }
}