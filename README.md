Схема архитектуры MVP

```mermaid
graph LR
    classDef model fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px;
    classDef presenter fill:#efebe9,stroke:#5d4037,stroke-width:2px;
    classDef view fill:#e1f5fe,stroke:#0288d1,stroke-width:2px;

    MODEL["MODEL<br>Business logic"]:::model
    PRESENTER["PRESENTER<br>Updates View<br>Notifies Model of changes"]:::presenter
    VIEW["VIEW<br>UI"]:::view

    VIEW --> PRESENTER
    PRESENTER --> MODEL
    MODEL --> PRESENTER
    PRESENTER --> VIEW
```

Направление зависимостей

```mermaid
graph LR
    classDef model fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px;
    classDef presenter fill:#efebe9,stroke:#5d4037,stroke-width:2px;
    classDef view fill:#e1f5fe,stroke:#0288d1,stroke-width:2px;
    classDef adapter fill:#fff3e0,stroke:#ef6c00,stroke-width:2px;
    classDef service fill:#eff3e0,stroke:#6f6c00,stroke-width:2px;


    VIEW["VIEW"]:::view
    SERVICES["SERVICES"]:::service
    ADAPTERS["ADAPTERS"]:::adapter
    PRESENTER["PRESENTER"]:::presenter
    MODEL["MODEL"]:::model

    ADAPTERS --> SERVICES
    ADAPTERS --> VIEW
    VIEW --> PRESENTER
    PRESENTER --> MODEL
```
все зависимости однонаправлены (Adapter -> View -> Presenter -> Model), кроме адаптера, который должен зависеть от сервисов.

---

Более подробная схема слоёв приложения с зависимостями

<img width="1121" height="502" alt="Диаграмма без названия" src="https://github.com/user-attachments/assets/08383d9d-ae0e-46bf-8170-9f8c7eddbbd5" />

<b>*  ИСКЛЮЧЕНИЕ ИЗ ПОРЯДКА ЗАВИСИМОСТЕЙ</b><br>
Окна зависят от AWindow базовой реализации WindowSystem. Так было удобнее инстансить и показывать/прятать окна.
<br><b>Как этого можно было избежать</b><br>
Перенести базовую реализации WindowSystem в View. Подробнее об этом в разделе OVERENGINEERING

---
<b>Итог:</b><br>
Однонаправленные зависимости, модель и презентер даже не знают, что они в Юнити. Это упрощает юнит тестирование, позволяет легко подменять реализации, переиспользовать части кода. <br>  
Отдельно вынес StringMathModule в бизнес логике. Также предполагается что все сервисы переиспользуются в других проектах (и вообще импортируются пакетом без возможности редактирования). Сервисы представляют интерфейс и возможность легко подменять реализации (например IPersistentData можно использовать PlayerPrefs под веб (т.к. на вебе не поддерживается запись в файлы), а под другие платформы могла бы быть реализация с чтением файлов / базы данных и пр.)

---
<b>OVERENGINEERING</b><br>
Хорошее упражнение в архитектуре, но очевидный оверинжиниринг для реальных продуктов. Если рассматривать это как архитектуру для проектов компаний, то можно предполагать, что определённые сервисы / фреймворки ВСЕГДА будут использоваться. Нет необходимости абстрагироваться от UniTask, VContainer, сервисов WindowsSystem, PersistentData (т.е. в данном примере можно было вообще обойтись без слоя Адаптеров, если принять за данное, что сервисы у нас будут в каждом проекте и это нормально, что бизнес-логика от них зависит). Можно писать более конкретные сервисы (например WindowsSystem.DefaultRealisation) и зависеть от них. <br>Софт, разработанный под конкретный фреймворк, всегда будет более оптимизированным, чем абстрактный. А в геймдеве оптимизация очень важна.
<br>Короче "хорошая" архитектура - это палка о двух концах. Нужно с умом выбирать где действительно нужно потратить время и выделить отдельный модуль / абстракцию, а где нет. 
