# MVC BD
### Описание 
Веб приложение для взаимодействия с базой данных

Поддерживаемые функиции - добавление, изменение, удаление записей и миграция в случае изменения структуры записи (добавление новых полей)
### Запуск 
Для запуска необходимо добавить базу файлы из папки "BD" в MS SQL Server, и изменить в файле Web.config строку, указав ваш логин и пароль
```<add name="DatabaseModel" connectionString="data source=localhost;initial catalog=Tishkin;User=student;Password=student;MultipleActiveResultSets=True;App=TISHKIN" providerName="System.Data.SqlClient"/>```

Когда приложение запустится в меню открыть ссылку "Список" 
