# PlanHora

**PlanHora** es una aplicación móvil desarrollada en .NET MAUI, orientada a la gestión de turnos y horarios de empleados, pensada inicialmente para negocios de hostelería, pero adaptable a cualquier sector que requiera planificación horaria semanal del personal.


## Funcionalidades 

- Gestión de locales.  
- Gestión de empleados.  
- Asignación de horarios semanales.  
- Cálculo automático de horas trabajadas.  
- Alertas por exceso de jornada laboral.  
- Almacenamiento local en SQLite.  
- Exportación de horarios en PDF / Email.  


##  Tecnologías utilizadas

- **.NET MAUI** (multi-plataforma: Android, iOS, Windows)  
- **SQLite** para persistencia de datos  
- **MVVM** como patrón de arquitectura  
- **Visual Studio 2022** entorno de desarrollo
- **.NET MAUI Community Toolkit** para mejora de UX
- **SkiaSharp** para general los PDFs
- **GitHub** para control de versiones  


##  Estructura inicial del proyecto

- `Views/` → Pantallas (UI).  
- `ViewModels/` → Lógica de presentación (MVVM).  
- `Models/` → Entidades (Empleado, Local, Horario, Festivo).  
- `Services/` → Servicios (acceso a datos, SQLite, etc).
- `Converters/` → Dar formato a informacion de horario


### Pantallas funcionales

- RegisterPage: pagina de registro de usuarios
- LoginPage: pagina de acceso de usuarios y navegación a SelectLocalPage.
- SelectLocalPage: agregar y borrar locales y acceder a empleados. Acceso a LocalFormPage y EmpleadosPage.
- LocalFormaPage: pagina para agregar locales a la lista.
- EmpleadosPage: lista de empleados con botón de prueba para agregar empleados y borrar empleados.
- EmpleadosFormPage: formulario para llenar los datos de los empleados para añadilos a la lista de los locales.
- EmpleadosDeletePage: maneja la lista de empleados para borrar un empleado.
- HorariosPage: pagina donde de se selecionará el empleado para darle un horario. Hay un botón para exportar el horario del empleado seleccionado como PDF y asi se puede enviarlo por whatsapp. Un botón para acceder a HorarioFormPage.
- HorarioFormPage: pagina donde se escribirá el horario del empleado, hace comprovaciones y exibe alertas. 

## Videos demo del proyecto:

- https://drive.google.com/drive/folders/1RWR1WoEPr_eGGwVQ6Q9b65xEcW_O6nn2?usp=drive_link

##  Diario de desarrollo 

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

### Fase 5 – Cálculos automáticos, validaciones y mejora de la UI
- [x] **16/10/2025** – Mejora de la UI
- [x] **20/10/2025 - finalizado 23/10/2025** – Cálculo de horas por día y por semana.
- [x] **20/10/2025 - finalizado 23/10/2025** – Validaciones: alertar si supera 9h/día o contrato semanal.
- [x] **20/10/2025** – Mensajes de alerta (pop-ups o notificaciones en pantalla).
- [x] **20/10/2025** – Testear

### Fase 6 – Exportación y compartir
- [x] **Iniciado 27/10/2025 - Finalizado 01/11/2025** – Generar PDF con el cuadrante semanal.
- [x] **Iniciado 27/10/2025 - Finalizado 01/11/2025** – Integrar función de compartir (correo, WhatsApp, etc.).
- [x] **01/11/2025** - Verificar formato y legibilidad del PDF.

### Fase 7 – Extras y pulido
 - [x] **08/11/2025** - Revisión de UX (diseño, usabilidad).
 - [x] **08/11/2025** - Corregir y habilitar pantalla de registro y login.
 - [x] **08/11/2025** - Revisar errores y corregir bugs.

### Fase 8 – Testing y entrega
 - [x] **09/11/2025** - Modificación de icono con el logo de la aplicación.
 - [x] **09/11/2025** - Testeo completo en emulador Android y en un dispositivo real.
 - [x] **09/11/2025** - Revisar errores y corregir bugs.
 - [x] **09/11/2025** - Grabacion de pantalla enseñando funcionamento de la aplicacion.
 - [x] **09/11/2025** - Preparar el PDF final del proyecto.

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
##### Fase 5:
- Navegación de SelectLocalPage a HorariosPage. Al seleccionar un local, la navegación no funcionaba y aparecía el error: Relative routing to shell elements is currently not supported.
  - **Solución:** Se cambió la URI a ruta absoluta con ///HorariosPage?localId=ID y se eliminó la dependencia del LocalPicker en HorariosPage. Ahora la página recibe LocalId por query property.
- EmpleadoFormPage con constructor de int. Se pasaba _localId como argumento, pero el constructor esperaba un objeto Empleado?.
  - **Solución:** Unificar los constructores a uno que reciba un Empleado? y asignar _empleado.LocalId cuando se cree uno nuevo.
- Frame estaba marcado como obsoleto en .NET 9.
  - **Solución:** Se reemplazó Frame por Border con VerticalStackLayout dentro.
- Validación de horarios en HorarioFormPage. Los horarios se guardaban como string y la comparación con TimeSpan fallaba (string was not recognized as a valid TimeSpan).
  - **Solución:** Convertir los horarios de apertura, cierre y entradas/salidas usando TimeSpan.TryParseExact o TimeSpan.TryParse.
- No se podían marcar días libres.
  - **Solución:** Se añadió CheckBox por día y el evento OnDiaLibreCheckedChanged que limpia las entradas de ese día y lo excluye del cálculo de horas.
- Validación de horas de entrada y salida: Al guardar horarios, la aplicación daba errores de string was not recognized as a valid TimeSpan o consideraba incorrectas horas válidas.
  - **Solución:** Implementamos TryParseHoraSimple que permite formatos flexibles como 9, 9:00 o 9:30. Esto evita errores de conversión y permite media hora (:30) en la jornada.
- Validación de 9 horas diarias: Aunque el usuario ingresaba menos de 9 horas, la aplicación alertaba que excedía 9 horas.
  - **Solución:** Ajustamos el cálculo de horasDia usando TimeSpan.TotalHours y aseguramos que se compare correctamente con el límite de 9 horas, considerando también los días libres.
- Actualización de horarios existentes: Al guardar un horario para un empleado que ya tenía uno, la aplicación creaba un duplicado y no se mostraba correctamente.
  - **Solución:** Antes de guardar, se busco si ya existe un horario para ese empleado y local. Si existe, sobrescribimos el registro usando el mismo Id para que SaveHorarioAsync haga un Update en lugar de Insert.
- Cuadrado blanco en HorariosPage: Después de guardar un horario, aparecía un cuadrado blanco en la CollectionView. Se estaba mostrando un HorarioDia vacío por no filtrar correctamente los días libres.
  - **Solución:** Creamos la clase HorarioDia con una propiedad EsLibre y eliminamos el filtrado que ocultaba días sin horario. Ahora los días libres se muestran correctamente.
- Mostrar días libres: Los días marcados como libres no se mostraban en la CollectionView.
  - **Solución:** Añadimos la propiedad EsLibre a HorarioDia. Creamos un BoolToLibreConverter que convierte true en "Libre". Asignamos todos los días al ItemsSource y usamos el converter en el XAML para mostrar "Libre" cuando corresponde.
- Formato de horas en CollectionView: Los horarios aparecían como 12 / 16 sin contexto.
  - **Solución:** Creamos HorarioFormatterConverter para mostrar los datos com una descrpcion de que significaban.
##### Fase 6:
- Error QuestPDF no pudo inicializarse en tiempo de ejecución. Causa investigada: QuestPDF depende de librerías de Windows Desktop (GDI, System.Drawing) que no existen en Android ni iOS. Por eso, aunque compile, al ejecutar en móvil falla.
- **Solución:** No usar QuestPDF en Android/iOS, porque no es compatible. Usar una librería que funcione en MAUI Mobile, la que se esta utilizando es SkiaSharp y SkiaSharp.Views.Maui.Controls.Compatibility. 
##### Fase 7:
- Antes, al agregar o borrar empleados y locales, todos los datos de todos los usuarios aparecían en la app. No había separación por usuario.
  - **Solución:** Se añadio la propiedad UsuarioId en Local y Empleado. Se actualizo los métodos de la DatabaseService para filtrar locales y empleados por el usuario actual: _local.UsuarioId = SessionService.UsuarioActual.Id; _empleado.UsuarioId = SessionService.UsuarioActual.Id;. Se actualizo los métodos GetLocalesPorUsuarioAsync y GetEmpleadosPorUsuarioAsync. Se modifico los SelectLocalPage y EmpleadoFormPage para que solo carguen los datos del usuario logueado.
##### Fase 8:
- Al intentar ejecutar la app en modo Release en un dispositivo Android real, aparecieron errores de AOT (Ahead-of-Time) relacionados con SkiaSharp.Views.Maui.Controls.Compatibility.dll y SkiaSharp.Views.Android.dll.
  - **Solución:** Se confirmo que en modo Debug funciona correctamente. Esto indica que el problema estaba relacionado con el AOT en Release. Se decidio seguir probando en modo Debug, que permite ejecutar en el dispositivo sin precompilación AOT estricta.
- Al cambiar el icono de la app por nuestro PNG (logoplanhorafinal.png) aparecieron errores como:resource mipmap/appicon not found
  - **Solución:** Se sustituio el <MauiIcon> en el .csproj por el png con el logo de la aplicacion, se verifico el AndroidManifest.xml para asegurarnos de que no hubiera referencias antiguas a appicon, se reconstruio la solución y ya no aparecieron errores.
    
### Pruebas realizadas

- Verificación de instalación de .NET con `dotnet --info`  
- Creación de proyecto de prueba “.NET MAUI App” y ejecución en Windows/Android
- Verificación de persistencia de locales y empleados: al añadir registros de prueba, estos permanecen al cerrar y reabrir la app.
- Navegación básica funcionando: LoginPage → SelectLocalPage → EmpleadosPage.
- Ejecución correcta en el emulador Android y en Windows.
- Ejecución correcta en un dispositivo real.

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
##### Fase 5:
- Documentación de Microsoft MAUI – Data Binding y Converters [https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding]
- TimeSpan.Parse / TryParse – Microsoft Docs: TimeSpan.Parse: [https://learn.microsoft.com/en-us/dotnet/api/system.timespan.parse?view=net-9.0]
- SQLite-net ORM – GitHub: sqlite-net: [https://github.com/praeclarum/sqlite-net?]
- Herramientas de asistencia con IA (ChatGPT, GitHub Copilot, Claude)
##### Fase 6:
- Sitio oficial SkiaSharp Docs: [https://learn.microsoft.com/en-us/previous-versions/xamarin/xamarin-forms/user-interface/graphics/skiasharp/]
- GitHub SkiaSharp: [https://github.com/mono/SkiaSharp]
- Herramientas de asistencia con IA (ChatGPT, GitHub Copilot, Tabnine)
##### Fase 7:
- Documentación oficial de .NET MAUI en Microsoft Learn
- Herramientas de asistencia con IA (ChatGPT, GitHub Copilot)
##### Fase 8:
- Documentación oficial de .NET MAUI en Microsoft Learn
- GitHub Issues de SkiaSharp
- Microsoft Q&A y StackOverflow
- Herramientas de asistencia con IA (ChatGPT, GitHub Copilot, Tabnine)
  
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

### Fase 5
Para mejorar la legibilidad del CollectionView en la página de horarios, se decidió dar formato a las etiquetas de entrada y salida y también mostrar explícitamente los días libres.Para eso se utilizo el IValueConverter.
##### Por qué se utilizo IValueConverter
Son una tecnología de MAUI/XAML que permite transformar los datos del modelo antes de mostrarlos en la interfaz, sin alterar los datos originales. Estos conversores permiten presentar la información de manera clara y consistente, mejorando la experiencia del usuario sin modificar la lógica de negocio.
##### Ejemplos en el proyecto:
BoolToLibreConverter: Convierte un booleano indicando si un día es libre en el texto "Libre" para mostrarlo en la UI.
HorarioFormatterConverter: Da formato a las horas de entrada y salida para que se vean como "Entrada - 09:00 / Salida - 17:00".

### Fase 6
Se intentó usar QuestPDF para generar los cuadrantes semanales, pero se descubrió que no es compatible con .NET MAUI y solo funciona en entornos de escritorio. Al ejecutar la app, aparecían errores de inicialización de fuentes y el PDF no se generaba correctamente.
##### Por qué se utilizo SkiaSharp
Buscando una solución compatible con MAUI, se optó por SkiaSharp, lo que permitió generar PDFs de manera confiable y compartirlos desde dispositivos móviles usando Share.Default.
También se aprendió a manejar correctamente la ubicación de los archivos usando FileSystem.CacheDirectory, asegurando que los PDFs se puedan compartir sin necesidad de permisos de almacenamiento adicionales.
##### Por qué se utilizo SkiaSharp.Views.Maui.Controls.Compatibility
SkiaSharp contiene solo la lógica de dibujo básica (clases SKCanvas, SKPaint, etc.), SkiaSharp.Views.Maui.Controls.Compatibility agrega adaptadores y bindings para que estas clases funcionen dentro de MAUI XAML y controles nativos, asegurando compatibilidad multiplataforma. Asi que Se añade SkiaSharp.Views.Maui.Controls.Compatibility para que se pueda dibujar el PDF correctamente en MAUI, evitando errores de inicialización de fuentes y gráficos en Android/iOS/Windows.

### Fase 7
Se añadio desde el inicio del desarrollo del proyecto, para facilitar la realizacion de pruebas, un boton el la pantalla de login para acceder la aplicacion sin hacer login. Con eso las funciones creadas para luego manejar los locales y empleados, como añadir y borrar locales, añadir y borrar empleados no llevaban en cuenta que pudiera haber mas de un usuario para utilizar la aplicacion. Para arreglar ese problema se añadio un sessionService y usuarioId.
##### Por que añadir SessionService
Introducir un SessionService para gestionar el usuario logueado simplifica el filtrado de locales, empleados y horarios.
##### Por que añadir UsuarioId
Al asociar cada registro con un UsuarioId, se evita mostrar datos de otros usuarios, mejorando la seguridad y coherencia de la app.
Ademas se tomo como lecion que la lógica de sesión y filtrado de datos por usuario es fundamental para apps multiusuario, asi que planificarla desde el inicio evita problemas de mezcla de datos en el futuro.

### Fase 8
Al intentar ejecutar la aplicacion en un dispositivo real aparecian problemas por haber elegido el modo de release ya que SkiaSharp.Views.Maui.Controls.Compatibility presentó problemas de precompilación. Al pesquisar sobre el tema se descubrio que no todos los paquetes NuGet funcionan automáticamente en AOT/Release para Android. La solucion encontrada para poder ejecutarlo en un dispositivo real fue probar la aplicación en modo Debug primero ya que el modo Release puede fallar por AOT. La leccion que se saca de eso es que se debe siempre conocer cómo forzar versiones compatibles de paquetes y revisar dependencias es crítico para evitar errores de compilación en Release.

---

### Autora

Proyecto desarrollado por **Natalia Leonardi Caffeu**
