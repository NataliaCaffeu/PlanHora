# PlanHora

**PlanHora** es una aplicación móvil desarrollada en **.NET MAUI**, orientada a la **gestión de turnos y horarios de empleados**, pensada inicialmente para negocios de hostelería, pero adaptable a cualquier sector que requiera planificación horaria semanal del personal.  

---

## Funcionalidades previstas

- Gestión de locales.  
- Gestión de empleados.  
- Asignación de horarios semanales.  
- Cálculo automático de horas trabajadas.  
- Alertas por exceso de jornada laboral.  
- Almacenamiento local en SQLite.  
- Exportación de horarios en PDF / Email.  
- Consulta de festivos.  

---

## Tecnologías utilizadas

- **.NET MAUI** (multi-plataforma: Android, iOS, Windows).  
- **SQLite** para persistencia de datos.  
- **MVVM** como patrón de arquitectura.  
- **Visual Studio 2022**.  
- **GitHub** para control de versiones.  

---

## Estructura inicial del proyecto

- `Views/` → Pantallas (UI).  
- `ViewModels/` → Lógica de presentación (MVVM).  
- `Models/` → Entidades (Empleado, Local, Horario, Festivo).  
- `Services/` → Servicios (acceso a datos, SQLite, etc).  

---

## Diario de desarrollo

### 🔹 Semana 1 – Preparación del entorno y base del proyecto
- [x] Creación del repositorio en GitHub.  
- [x] Instalación y configuración de Visual Studio 2022 con .NET MAUI.  
- [x] Creación del proyecto base **PlanHora** en MAUI.  
- [ ] Definición de estructura de carpetas y namespaces.  
- [ ] Implementación de pantallas iniciales (Login / Selección de local).  

**Problemas encontrados:**  
- Dificultad para localizar la carga de trabajo correcta en Visual Studio Installer.  
- Solución: activar la opción **“Desarrollo de la interfaz de usuario de aplicaciones multiplataforma (.NET MAUI)”**.  

**Pruebas realizadas:**  
- Verificación de instalación de .NET con `dotnet --info`.  
- Creación de un proyecto de prueba “.NET MAUI App” y ejecución en Windows/Android.  

---

## Otros aprendizajes y recursos

- Curso seguido: [Documentación oficial de .NET MAUI en Microsoft Learn](https://learn.microsoft.com/es-es/dotnet/maui/).  
- Guías de SQLite en MAUI: [sqlite-net-pcl GitHub](https://github.com/praeclarum/sqlite-net). 
- Uso de IAs como herramienta de ayuda (ChatGPT).
---

## Estado del proyecto

- [x] Creación del repositorio GitHub.  
- [x] Configuración de entorno MAUI.  
- [ ] Proyecto base.  
- [ ] Modelado de datos.  
- [ ] Funcionalidades CRUD.  
- [ ] Generación de horarios y PDF.  

---

## Autora

Proyecto desarrollado por **Natalia Leonardi Caffeu**.  
