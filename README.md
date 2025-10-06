# PlanHora

**PlanHora** es una aplicación móvil desarrollada en .NET MAUI, orientada a la gestión de turnos y horarios de empleados, pensada inicialmente para negocios de hostelería, pero adaptable a cualquier sector que requiera planificación horaria semanal del personal.


## Funcionalidades previstas

- Gestión de locales.  
- Gestión de empleados.  
- Asignación de horarios semanales.  
- Cálculo automático de horas trabajadas.  
- Alertas por exceso de jornada laboral.  
- Almacenamiento local en SQLite.  
- Exportación de horarios en PDF / Email.  
- Consulta de festivos.  


##  Tecnologías utilizadas

- **.NET MAUI** (multi-plataforma: Android, iOS, Windows)  
- **SQLite** para persistencia de datos  
- **MVVM** como patrón de arquitectura  
- **Visual Studio 2022**  
- **GitHub** para control de versiones  


##  Estructura inicial del proyecto

- `Views/` → Pantallas (UI).  
- `ViewModels/` → Lógica de presentación (MVVM).  
- `Models/` → Entidades (Empleado, Local, Horario, Festivo).  
- `Services/` → Servicios (acceso a datos, SQLite, etc).  


### Funcionalidades funcionando actualmente

- LoginPage: navegación a SelectLocalPage.
- SelectLocalPage: agregar locales de prueba, visualizar la lista y persistencia correcta en SQLite.
- EmpleadosPage: lista de empleados con botón de prueba para agregar empleados; persistencia funcionando.


##  Diario de desarrollo / Estado actual 

### Fase 1  – Preparación del entorno y base del proyecto

- [x] **19/08/2025** – Creación del repositorio en GitHub  
- [x] **05/09/2025** – Instalación y configuración de Visual Studio 2022 con .NET MAUI  
- [x] **05/09/2025** – Creación del proyecto base PlanHora en MAUI  
- [x] **05/09/2025** – Definición de estructura de carpetas y namespaces   
- [x] **05/09/2025** – Primer commit y push exitoso al repositorio remoto (resolviendo conflictos en `README.md` y `PlanHora.csproj`)
- [x] **29/09/2025** - Implementación de pantallas iniciales (Login / Selección de local).

### Fase 2 – Modelado de datos

- [x] **05/09/2025** – Definir modelo de datos: Local, Empleado, Horario, Festivo.
- [x] **05/09/2025** – Crear clases en C# para cada entidad.
- [x] **05/09/2025** – Integrar SQLite en MAUI.
- [x] **05/09/2025** – Implementar métodos CRUD básicos (añadir, editar, eliminar empleados y locales).
- [x] **02/10/2025** - Probar persistencia de datos.

### Fase 3 – Gestión de empleados y locales

- [x] **05/10/2025** – Pantalla para listar empleados.
- [x] - Pantalla para añadir/editar empleados (nombre, jornada semanal, contrato).
- [x] - Pantalla para añadir/editar locales.
- [x] - Validaciones básicas de formularios.

## Problemas encontrados y soluciones

- Dificultad para localizar la carga de trabajo correcta en Visual Studio Installer  
  - **Solución:** activar la opción “Desarrollo de la interfaz de usuario de aplicaciones multiplataforma (.NET MAUI)”  
- Error al hacer push inicial por conflictos en `README.md` y `PlanHora.csproj`  
  - **Solución:** hacer Pull, resolver conflictos aceptando versión local, commit de merge y push exitoso
- Errores de InitializeComponent() y nombres de elementos XAML
Ocurrían porque el x:Class del XAML y la clase .cs no coincidían o el proyecto no estaba compilando correctamente los archivos generados.
  - **Solución:** se verificó que x:Class coincida con el namespace y nombre de la clase, y se recompiló el proyecto.
- Incoherencia de accesibilidad en métodos y clases
Los métodos de acceso a la base de datos usaban tipos internos o miembros required que bloqueaban la compilación.
  - **Solución:** se cambió la visibilidad de clases y métodos a public y se ajustaron las propiedades necesarias para trabajar con SQLite.
- Uso de métodos sincrónicos en SQLite
Inicialmente se usaban métodos sincrónicos (SQLiteConnection), lo que podía bloquear la interfaz de usuario.
  - **Solución:** se migró a métodos asíncronos (SQLiteAsyncConnection) para mejorar el rendimiento y evitar bloqueos en la UI.
- Propiedades required y errores de compilación
  - **Solución:** Se usaron propiedades necesarias (required) en modelos para asegurar que los campos no queden nulos.
  - **Solución:** Ajustes en el código y inicialización de entidades permitieron que SQLite pueda trabajar correctamente con estas propiedades.
- Obsolescencia de Frame en MAUI
Frame aparece obsoleto en .NET 9.
  - **Solución:** se reemplazó por Border con VerticalStackLayout para mantener la apariencia. 

### Pruebas realizadas

- Verificación de instalación de .NET con `dotnet --info`  
- Creación de proyecto de prueba “.NET MAUI App” y ejecución en Windows/Android
- Verificación de persistencia de locales y empleados: al añadir registros de prueba, estos permanecen al cerrar y reabrir la app.
- Navegación básica funcionando: LoginPage → SelectLocalPage → EmpleadosPage.
- Ejecución correcta en el emulador Android y en Windows.

### Otros aprendizajes y recursos

##### Fase 1 y 2:
- Curso seguido: Documentación oficial de .NET MAUI en Microsoft Learn  
- Guías de SQLite en MAUI: [sqlite-net-pcl GitHub](https://github.com/praeclarum/sqlite-net) 
- Uso de IAs como herramienta de ayuda (ChatGPT)
##### Fase 3:
- .NET MAUI – SQLite: [https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-9.0](https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-9.0)  
- Almacenar datos locales en .NET MAUI – Microsoft Learn: [https://learn.microsoft.com/en-us/training/modules/store-local-data/](https://learn.microsoft.com/en-us/training/modules/store-local-data/)  
- Exploring SQLite Integration in .NET MAUI – Medium: [https://medium.com/@erdalkama/exploring-sqlite-integration-in-net-maui-59371a8ec1d3](https://medium.com/@erdalkama/exploring-sqlite-integration-in-net-maui-59371a8ec1d3)  
- Beginners Guide to Integrating SQLite with .NET MAUI – MoldStud: [https://moldstud.com/articles/p-beginners-guide-to-integrating-sqlite-with-net-maui](https://moldstud.com/articles/p-beginners-guide-to-integrating-sqlite-with-net-maui)  
- Stack Overflow – SQLite-net PCL CreateTableAsync issue: [https://stackoverflow.com/questions/74213821/sqlite-net-pcl-stuck-on-createtableasynctype](https://stackoverflow.com/questions/74213821/sqlite-net-pcl-stuck-on-createtableasynctype)  
- Stack Overflow – Why must the new() constraint require a public constructor?: [https://stackoverflow.com/questions/5984152/why-must-the-new-constraint-require-a-public-constructor](https://stackoverflow.com/questions/5984152/why-must-the-new-constraint-require-a-public-constructor)  
- C# Required Members – Microsoft Learn: [https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-11.0/required-members](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-11.0/required-members)  
- C# new constraint – Microsoft Learn: [https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-constraint](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-constraint)
- Herramientas de asistencia con IA (ChatGPT, GitHub Copilot, Tabnine, Perplexity AI)


##  Consideraciones y aprendizajes

### Fase 1 y 2: 

En un principio, se pensó en utilizar la arquitectura MVC, que fue la aprendida en clase. Sin embargo, después de pesquisar sobre el tema, se decidió utilizar la arquitectura MVVM, ya que, aunque es posible usar MVC para un proyecto como *PlanHora*, no es lo ideal debido al funcionamiento de .NET MAUI y sus ventajas. Para pantallas simples y con pocas interacciones, MVC podría funcionar, pero se perderían muchas de las ventajas de MAUI y el código podría volverse más difícil de mantener.
##### Por qué MVVM es preferible en MAUI
- Data Binding automático: los controles de la UI se enlazan directamente a las propiedades del ViewModel, actualizándose automáticamente.  
- Separación limpia entre UI y lógica: la View no conoce la lógica de negocio; el código se mantiene más organizado.  
- Escalabilidad y mantenimiento: facilita agregar nuevas pantallas o funcionalidades sin afectar el resto de la app.  
- Compatibilidad con herramientas de MAUI: como `ObservableCollection`, `INotifyPropertyChanged` y comandos para botones, que funcionan de forma nativa con MVVM.  

 ### Fase 3

 En un primer momento, se intento utilizar métodos sincrónicos para acceder a SQLite, ya que son más sencillos de implementar y permiten realizar consultas y operaciones de manera directa sin necesidad de async o await. Para pruebas rápidas y pantallas simples, esta opción parecía suficiente. Sin embargo, después de probar la app en el emulador, se observó que al realizar consultas o guardar registros la interfaz se bloqueaba temporalmente, afectando la experiencia del usuario. Por esta razón, se decidió cambiar a métodos asincrónicos (SQLiteAsyncConnection) que permiten ejecutar las operaciones de base de datos en segundo plano.
##### Por qué los métodos asincrónicos son preferibles en MAUI
- Evitan bloquear la interfaz de usuario, garantizando que la app siga respondiendo mientras se cargan o guardan datos.
- Permiten un flujo más fluido cuando hay muchas consultas o registros.
- Facilitan el mantenimiento, ya que se pueden combinar con async/await en cualquier parte de la app sin comprometer la UI.
- Son la práctica recomendada en aplicaciones multiplataforma modernas, especialmente cuando se trabaja con bases de datos locales y operaciones que pueden tardar.
  
---

### Autora

Proyecto desarrollado por **Natalia Leonardi Caffeu**
