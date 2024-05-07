# Between the lines : Vertex animation in UI Toolkit


## Chapter 1: Animation concept

Хочу начать с объяснение того, как будет работать анимация у наших элеметов интерфейса.

Речь идет не про Transition/Delays/и других встроенных возможностях анимации в UI Toolkit'е.

Мы продолжим изучение возможностей генерации интерфейсов на основе создания вершин и меша.

Из предыдущих статей мы научилист создавать кастомные UI элементы, теперь давайте добавим немного динамичности.

Стоит сказать, что генерация анимации развлечение не самое дешевое с точки зрения производительности, так как она будет нагружать CPU чуть ли не каждый кадр.

Если разобрать анимацию на базовые составляющие, то это изменение состояния объекта (это может быть размеры, формы, цвет, положение).

Первая задача которая стоит перед нами, научиться вызывать внутренние методы класса **VisualElement'а**, через определенные количетсво времени. 

Для этого мы будем использовать внутренюю реализацию интерфейса **IVisualElementScheduler**.

Из названия можно догадаться, что оно занимается планирование и выполнение каких-то действий, давайте разбираться как с ним работать.
```csharp
public RombElement()
{
    schedule.execute(MethodName);
}
```

В этом примере кода, при создание элемента мы обращаемся к методу **Execute** передавая в него **Action**.

Это значит, что этот метод будет вызван сразу после выполнения конструктора.

Стоит понимать, что выполнен он будет только один раз, а как нам сделать многократные вызовы?

– Я рад, что вы спросили, отвечаю:

```csharp
public RombElement()
{
    schedule.Execute(MethodName).Every(16);
}
```

Мы добавляем к нашему вызову **.Every**, что означает, наш метод будет вызываться каждые X милисекунд.

## Chapter 2: Color animation 

Под анимацией цвета мы будем обозначать изменение цвета/прозрачности и связанное с его составляющимим у **UI Element'a**

К примеру, мы хотим, чтобы наш элемент интерфейса появлялся из прозрачности, давайте сделаем это на примере нашего RombElement'а.

```csharp
public RombElement()
{
    schedule.Execute(MethodName).Every(16);
}
```

Давайте сделаем так, чтобы наш элемент проявлялся из прозрачности. 
Как и всегда, приведу весь код, а потом будет разбирать детально.

```csharp
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomElements
{
    [UxmlElement]
    public partial class RombElement : VisualElement
    {
        private float _timeLeft;
        
        private const float AlphaValue = 255;
        
        [UxmlAttribute] public float AnimationTime = 3f;

        public RombElement()
        {
            generateVisualContent += GenerateVisualContent;

            _timeLeft = AnimationTime;
            schedule.Execute(ChangeColorAnimation).Every(16);
        }

        private void ChangeColorAnimation()
        {
            _timeLeft -= Time.deltaTime;
            
            firstColor.a = (byte)Mathf.Lerp(firstColor.a, AlphaValue, Time.fixedDeltaTime / _timeLeft);
            secondColor.a = (byte)Mathf.Lerp(secondColor.a, AlphaValue, Time.deltaTime / _timeLeft);
            thirdColor.a = (byte)Mathf.Lerp(thirdColor.a, AlphaValue, Time.fixedDeltaTime / _timeLeft);
            fourColor.a = (byte)Mathf.Lerp(fourColor.a, AlphaValue, Time.deltaTime / _timeLeft);

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
```

Обратим наше внимание на конструктор класса, мы там можем заметить вызов метода **scheduler'а**.

Мы хотим вызывать метод **ChangeColorAnimation** каждые 16 милисекунд.

Дальше идет само тело метода анимации, в нем мы можем заметить **_timeLeft**, это переменная для отслеживания времени анимации, которое изначально равно **AnimationTime**. 

Кстати, его можно настроить напрямую через **UI Builder**, благодаря новому аттрибуту **[UxmlAttribute]**. 

Он появился в новой версии пакета, более подробнее о нововведениях можно прочетсь в моей [статье](https://dtf.ru/gamedev/2540623-chto-neset-nam-den-gryadushii-ili-novye-vozmozhnosti-ui-toolkita-s-kodom-i-kartinkami).

Мы немного отвлеклись, вернемся к нашим анимациям.


```chsarp
private void ChangeColorAnimation()
{
    _timeLeft -= Time.deltaTime;
    
    firstColor.a = (byte)Mathf.Lerp(firstColor.a, AlphaValue, Time.fixedDeltaTime / _timeLeft);
    secondColor.a = (byte)Mathf.Lerp(secondColor.a, AlphaValue, Time.deltaTime / _timeLeft);
    thirdColor.a = (byte)Mathf.Lerp(thirdColor.a, AlphaValue, Time.fixedDeltaTime / _timeLeft);
    fourColor.a = (byte)Mathf.Lerp(fourColor.a, AlphaValue, Time.deltaTime / _timeLeft);

    MarkDirtyRepaint();
}
```

Дальше у нас идет четыре строки, где мы описываем, что наш оттенки наших вершин должны стремится от 0, к 255.

После чего, мы вызываем метод **MarkDirtyRepaint()**, для того чтобы спровоцировать вызов метода **GenerateVisualContent(...)**

[Примерно так это выглядит](Resources/firstColorAnimation.mov)
