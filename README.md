# Сведения о приложении
## Запуск двух решений одновременно
Для одновременного запуска в режиме отладки и приложения и Web-сервиса необходимо нажать правой кнопкой мыши на решение, выбрать Назначить запускаемые проекты (видимо, в инглиш вершн будет что-то про Startup Project) и настроить.
## Решение проблемы с node-sass
После попытки запуска приложения в консоли в папке проекта Auctionator нужно исполнить команду 'npm rebuild node-sass'.
## Тестирование без представления
В решении WebService в папке Services есть класс UnitTest. Для тестирования нужно делать изменения в методе Run(). Шаблон уже подготовлен.
После запуска решения WebService тестирование запустится автоматически.