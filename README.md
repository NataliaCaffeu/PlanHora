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
- [x] **09/10/2025** - Pantalla para añadir empleados (nombre, jornada semanal, contrato).
- [x] **09/10/2025** - Pantalla para añadir locales.
- [x] **09/10/2025** - Validaciones básicas de formularios.

### Fase 4 – Gestión de horarios (UI básica)
- [x] **12/10/2025** – Crear pantalla de planificación semanal: tabla de días (Lunes a Domingo).
- [x] **12/10/2025** – Posibilidad de asignar hora entrada/salida a un empleado.
- [x] **12/10/2025** – Guardar horarios en la base de datos.
- [x] **12/10/2025** – Visualizar horas asignadas en la interfaz.

## Problemas encontrados y soluciones

##### Fase 1 y 2:
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
##### Fase 3:
Errores de compilación y ejecución relacionados con emulador Android y Fast Deployment
- **Solución:** desactivar temporalmente OneDrive para la carpeta del proyecto, limpiar manualmente bin y obj, desactivar “Implementación rápida” en las propiedades del proyecto → Android → Debug y volver a ejecutar.
Advertencias XAML de bindings (Binding could be compiled to improve runtime performance)
##### Fase 4:
- Campos de sábado y domingo solapados en la UI
  - **Solución:** Ajuste de la definición de filas del Grid en HorariosPage.xaml para que cada día tenga su propia fila y no se superpongan.
- No se cargaban los horarios existentes al seleccionar un empleado
  - **Solución:** Se implementó el método LoadHorarioEmpleado en HorariosPage.xaml.cs y se enlazó al evento SelectedIndexChanged del Picker para precargar los horarios desde la base de datos.
- Creación de duplicados al guardar un horario ya existente
  - **Solución:** Se agregó lógica en OnSaveClicked para verificar si existe un registro previo para ese empleado y día. Si existe, se actualiza con UpdateAsync en lugar de crear uno nuevo con InsertAsync.
- Actualización de horarios no reflejada en la UI ni en la base de datos
  - **Solución:** Se reemplazó la lista simple por un diccionario Dictionary<string, Horario> para gestionar los horarios por día y empleado, garantizando que cada día se actualice correctamente y que los cambios se persistan.
- Errores de nulabilidad en los parámetros del evento OnSaveClicked y SelectedIndexChanged
  - **Solución:** Se ajustaron los parámetros a object? sender, EventArgs? e para coincidir con la firma del delegado y evitar advertencias de compilación.
- Duplicidad de claves al usar días como identificador
  - **Solución:** Se manejó un diccionario por día, evitando que se intenten agregar claves duplicadas (Key: Lunes) y garantizando que se actualicen los horarios existentes.

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
##### Fase 4:
- Documentación oficial de .NET MAUI en Microsoft Learn
- Guías de SQLite en MAUI
- Almacenar datos locales en .NET MAUI
- Apuntes asignatura de Servicios en el segundo ciclo de DAM.
- Herramientas de asistencia con IA (ChatGPT, GitHub Copilot)

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

### Fase 4
Afirmando los conocimientos adquiridos mientras se cursaba la disciplina de servicios en el segundo ciclo de DAM, empregando el uso de delegados y el Dictionary.
##### Por qué se utilizaron los delegados
Se utilizaron para manejar eventos de la interfaz, como la selección de un empleado en el Picker. Gracias a los delegados, podemos ejecutar automáticamente la lógica de carga de horarios cuando el usuario cambia la selección, manteniendo la UI reactiva y desacoplada de la lógica de negocio.
##### Por qué se utilizo el Dictionary
Se implementó para organizar los horarios por día de la semana de manera eficiente. Esto permite actualizar o acceder a un horario específico sin riesgo de duplicados, simplificando la gestión y sincronización de los datos entre la base de datos y la interfaz.
  
---

### Autora

Proyecto desarrollado por **Natalia Leonardi Caffeu**
