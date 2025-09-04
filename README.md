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


##  Diario de desarrollo / Estado actual 

### Semana 1  – Preparación del entorno y base del proyecto

- [x] **19/08/2025** – Creación del repositorio en GitHub  
- [x] **05/09/2025** – Instalación y configuración de Visual Studio 2022 con .NET MAUI  
- [x] **05/09/2025** – Creación del proyecto base PlanHora en MAUI  
- [x] **05/09/2025** – Definición de estructura de carpetas y namespaces   
- [x] **05/09/2025** – Primer commit y push exitoso al repositorio remoto (resolviendo conflictos en `README.md` y `PlanHora.csproj`)
- [ ] - Implementación de pantallas iniciales (Login / Selección de local).

### Semana 2 – Modelado de datos

- [x] **05/09/2025** – Definir modelo de datos: Local, Empleado, Horario, Festivo.
- [x] **05/09/2025** – Crear clases en C# para cada entidad.
- [x] **05/09/2025** – Integrar SQLite en MAUI.
- [x] **05/09/2025** – Implementar métodos CRUD básicos (añadir, editar, eliminar empleados y locales).
- [ ] - Probar persistencia de datos.


## Problemas encontrados y soluciones

- Dificultad para localizar la carga de trabajo correcta en Visual Studio Installer  
  - **Solución:** activar la opción “Desarrollo de la interfaz de usuario de aplicaciones multiplataforma (.NET MAUI)”  
- Error al hacer push inicial por conflictos en `README.md` y `PlanHora.csproj`  
  - **Solución:** hacer Pull, resolver conflictos aceptando versión local, commit de merge y push exitoso  

### Pruebas realizadas

- Verificación de instalación de .NET con `dotnet --info`  
- Creación de proyecto de prueba “.NET MAUI App” y ejecución en Windows/Android  

### Otros aprendizajes y recursos

- Curso seguido: Documentación oficial de .NET MAUI en Microsoft Learn  
- Guías de SQLite en MAUI: [sqlite-net-pcl GitHub](https://github.com/praeclarum/sqlite-net) 
- Uso de IAs como herramienta de ayuda (ChatGPT)  


##  Consideraciones y aprendizajes

### Semana 1 y 2: 

En un principio, se pensó en utilizar la arquitectura MVC, que fue la aprendida en clase. Sin embargo, después de pesquisar sobre el tema, se decidió utilizar la arquitectura MVVM, ya que, aunque es posible usar MVC para un proyecto como *PlanHora*, no es lo ideal debido al funcionamiento de .NET MAUI y sus ventajas. Para pantallas simples y con pocas interacciones, MVC podría funcionar, pero se perderían muchas de las ventajas de MAUI y el código podría volverse más difícil de mantener.

##### Por qué MVVM es preferible en MAUI
- Data Binding automático: los controles de la UI se enlazan directamente a las propiedades del ViewModel, actualizándose automáticamente.  
- Separación limpia entre UI y lógica: la View no conoce la lógica de negocio; el código se mantiene más organizado.  
- Escalabilidad y mantenimiento: facilita agregar nuevas pantallas o funcionalidades sin afectar el resto de la app.  
- Compatibilidad con herramientas de MAUI: como `ObservableCollection`, `INotifyPropertyChanged` y comandos para botones, que funcionan de forma nativa con MVVM.  
 

---

### Autora

Proyecto desarrollado por **Natalia Leonardi Caffeu**
