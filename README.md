#  Biblioteca API - CRUD con Entity Framework Core

## Descripción

Este proyecto es una API REST desarrollada con **ASP.NET Core** y **Entity Framework Core**, que simula un sistema de gestión de biblioteca.

Permite administrar información sobre:
- Autores
- Libros
- Usuarios
- Préstamos

El objetivo principal es practicar operaciones CRUD (Create, Read, Update, Delete) aplicando buenas prácticas de arquitectura y acceso a datos.

---

##  Tecnologías utilizadas

-  ASP.NET Core Web API (.NET 8)
-  Entity Framework Core
-  MySQL
-  Pomelo.EntityFrameworkCore.MySql
-  Swagger (documentación de endpoints)

---

##  Modelo de datos

### Entidades principales:

- **Autor**
  - Id
  - Nombre

- **Libro**
  - Id
  - ISBN
  - Título
  - FechaPublicación
  - AutorId
  - Ejemplares
  - Disponibles

- **Usuario**
  - Id
  - Nombre
  - Usuario
  - Password
  - Rol
  - FechaRegistro

- **Prestamo**
  - Id
  - FechaPrestamo
  - FechaDevolucion
  - UsuarioId
  - LibroId
  - Devuelto

---

##  Relaciones

- Un **Autor** puede tener muchos **Libros**
- Un **Usuario** puede tener muchos **Préstamos**
- Un **Libro** puede estar en muchos **Préstamos**

---

##  Funcionalidades

###  CRUD completo para:
- Libros
- Usuarios
- Préstamos

###  Consultas implementadas:
- Obtener libros con su autor
- Obtener préstamos con usuario y libro
- Filtrar préstamos activos
- Ordenar libros por fecha de publicación
- Búsqueda de libros por nombre (LIKE)

---

##  Endpoints principales

###  Libros
- `GET /api/libros`
- `POST /api/libros`
- `PUT /api/libros/{id}`
- `DELETE /api/libros/{id}`

###  Usuarios
- `GET /api/usuarios`
- `POST /api/usuarios`

###  Préstamos
- `GET /api/prestamos`
- `POST /api/prestamos`

---

##  Configuración del proyecto

### 1. Clonar repositorio

```bash
git clone https://github.com/tu-usuario/tu-repo.git
cd tu-repo
