# 🏢 Sistema de Gestión de Unidad Residencial

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![Blazor](https://img.shields.io/badge/Blazor-WebApp-512BD4?style=flat-square&logo=blazor)
![C#](https://img.shields.io/badge/C%23-Latest-239120?style=flat-square&logo=csharp)
![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?style=flat-square&logo=microsoftsqlserver)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-Community_2026-5C2D91?style=flat-square&logo=visualstudio)
![Estado](https://img.shields.io/badge/Estado-En_Desarrollo-yellow?style=flat-square)

Sistema web desarrollado con **Blazor Web App (.NET 8)** para la administración integral de una unidad residencial. Permite gestionar el control de acceso, parqueadero, mensajería, reservas de zonas comunes, mantenimientos y residentes desde una sola plataforma corporativa.

> 📚 Proyecto académico — Materia: **Profundización en Programación**


---

## 📌 Descripción General

**ResidenciaPro** es una aplicación web diseñada para centralizar y digitalizar la administración de un conjunto residencial. El sistema contempla múltiples roles de usuario (Administrador, Vigilante, Residente) y cubre los procesos operativos y administrativos del día a día de una unidad residencial moderna.

### Funcionalidades principales

- Dashboard con métricas en tiempo real del conjunto
- Control de ingreso y salida de residentes, visitantes y proveedores
- Gestión del parqueadero con visualización de cupos en tiempo real
- Registro y seguimiento de mensajería y paquetes
- Reservas de zonas comunes (salón social, BBQ, piscina, gimnasio)
- Programación y seguimiento de mantenimientos
- Gestión de residentes y apartamentos por torre

---

## 🛠️ Tecnologías Utilizadas

| Tecnología | Versión | Uso |
|---|---|---|
| .NET | 8.0 | Framework principal |
| Blazor Web App | .NET 8 | Frontend y lógica de UI |
| C# | Latest | Lenguaje de programación |
| SQL Server | 2022 | Base de datos relacional |
| SQL Server Management Studio | 19+ | Administración de BD |
| Visual Studio Community | 2022 | IDE de desarrollo |
| CSS Isolation | Blazor built-in | Estilos por componente |
| DM Sans / DM Mono | Google Fonts | Tipografía del sistema |

---

##A tener en cuenta!
La aplicacion tiene una conexion por medio de una api al backend el cual su repositorio se encuentra en: https://github.com/caes2004/API-.NET.git

## 📦 Módulos del Sistema

### ✅ Dashboard
Vista general del conjunto residencial con KPI cards en tiempo real, feed de actividad reciente, mapa visual del parqueadero y paneles de reservas, mantenimientos y mensajería.

**Componentes:**
- `Sidebar.razor` — Navegación lateral con acceso a todos los módulos
- `Dashboard.razor` — Página principal con métricas y resúmenes

---

### ✅ Control de Acceso
Registro y seguimiento de todos los ingresos y salidas del conjunto. Diferencia entre residentes, visitantes y proveedores.

**Componentes:**
- `AccesoFiltros.razor` — Barra de filtros por tipo de ingreso y búsqueda
- `AccesoTabla.razor` — Tabla de registros con acción de registrar salida
- `AccesoFormulario.razor` — Modal para registrar nuevos ingresos

**Entidades BD relacionadas:** `Ingreso`, `Usuario`, `Apartamentos`

---

### ✅ Parqueadero
Mapa visual de cupos en tiempo real, registro de visitantes con placa e historial de uso del parqueadero.

**Entidades BD relacionadas:** `Parqueadero`, `ParqueaderoVisitante`, `Ingreso`

---

### ✅  Mantenimiento
Programación y seguimiento de mantenimientos por tipo y zona común. Registro de proveedor, costo y estado.

**Entidades BD relacionadas:** `Mantenimiento`, `TipoMantenimiento`, `ZonaComun`

---

### ✅ Mensajería
Control de paquetes y correspondencia recibida. Registro de empresa, guía, fecha de recepción y entrega.

**Entidades BD relacionadas:** `Mensajeria`, `Apartamentos`, `Usuario`

---

### ✅ Reservas de Zonas Comunes
Gestión de reservas de espacios como salón social, BBQ, piscina y gimnasio. Control de disponibilidad por fecha y hora.

**Entidades BD relacionadas:** `Reserva`, `ZonaComun`, `Usuario`

---

### ✅ Residentes & Apartamentos
Administración de residentes por torre y apartamento. Registro de propietarios e inquilinos.

**Entidades BD relacionadas:** `ResidenteUnidad`, `Apartamentos`, `Torre`, `Conjunto`, `Usuario`, `Rol`

---

## 🗄️ Base de Datos

El sistema utiliza **SQL Server**

### Tablas principales

| Tabla | Descripción |
|---|---|
| `Conjunto` | Información general del conjunto residencial |
| `Torre` | Torres que componen el conjunto |
| `Apartamentos` | Unidades residenciales por torre |
| `Usuario` | Usuarios del sistema con rol asignado |
| `Rol` | Roles disponibles (Administrador, Vigilante, Residente) |
| `ResidenteUnidad` | Relación residente–apartamento (propietario o inquilino) |
| `Ingreso` | Registro de entradas y salidas del conjunto |
| `Parqueadero` | Cupos de parqueadero asignados a unidades |
| `ParqueaderoVisitante` | Registro de vehículos visitantes |
| `ZonaComun` | Zonas comunes disponibles para reserva |
| `Reserva` | Reservas de zonas comunes por residente |
| `Mantenimiento` | Mantenimientos programados o realizados |
| `TipoMantenimiento` | Catálogo de tipos de mantenimiento |
| `Mensajeria` | Registro de paquetes y correspondencia |
| `BitacoraVigilancia` | Bitácora de eventos de seguridad |

---

## ⚙️ Instalación y Configuración

### Prerrequisitos

- Visual Studio Community 2022 con carga de trabajo **ASP.NET y desarrollo web**
- .NET 8.0 SDK
- SQL Server 2022 (o SQL Server Express)
- SQL Server Management Studio (SSMS)

### Pasos

**1. Clonar el repositorio**
```bash
git clone https://github.com/JuanchoDev06/SolucionUnidadResidencial.git
```

**2. Abrir en Visual Studio**

Abre Visual Studio → `Archivo` → `Abrir` → `Proyecto o solución` → selecciona el archivo `.sln`

**3. Configurar la base de datos**

Abre SSMS, conecta a tu instancia de SQL Server y ejecuta el script de creación de la base de datos ubicado en:
```
/Database/ResidenciaPro_Script.sql
```

**4. Configurar la cadena de conexión**

En `appsettings.json` actualiza la cadena de conexión:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=ResidenciaPro;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

**5. Ejecutar el proyecto**

Presiona `F5` en Visual Studio o haz clic en el botón **Ejecutar**. El proyecto se abrirá en el navegador automáticamente.

---

## 📊 Estado del Desarrollo

| Módulo | Interfaz | BD Conectada |
|---|---|---|
| Dashboard | ✅ Completo | ✅ Completo |
| Control de Acceso | ✅ Completo | ✅ Conectado |
| Parqueadero | ✅ Completo | ✅ Conectado |
| Mantenimiento | ✅ Completo | ✅ En progreso |
| Mensajería | ✅ Completo | ✅ Conectado |
| Reservas | ✅ Completo | ✅ Conectado |
| Residentes & Apartamentos | ✅ Completo | ✅ Conectado |
| Login / Autenticación JWT | ✅ Completo | ✅ Conectado |
| Módulo de Admin | ✅ Completo | ✅ Completo |
| Autorización por Roles | ✅ Completo | ✅ Completo |
| Módulo de Reportes | ✅ Completo | ✅ Completo |

---

## 👥 Equipo

| Nombre | Rol |
|---|---|
| **Juan Andres Correa** | Desarrollador Full Stack |
| **Johnder Naranjo** | Desarrollador Full Stack |
| **Esteban Cano** | Desarrollador Full Stack |

---

## 📄 Licencia

Proyecto académico — Todos los derechos reservados © 2026
