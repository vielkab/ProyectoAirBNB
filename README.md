# Abnb - Base de proyecto con DDD (Domain-Driven Design)

Este repositorio implementa el nucleo de dominio de una plataforma tipo Airbnb, pensado como base academica para cursos y trabajos futuros.

El objetivo no es tener una aplicacion completa, sino un modelo de dominio limpio y expresivo que sirva para aprender:

- como modelar reglas de negocio en codigo,
- como proteger invariantes del dominio,
- como separar responsabilidades con arquitectura en capas,
- y como preparar el terreno para crecer hacia Application, Infrastructure y API.

## 1. Estado actual del proyecto

La solucion contiene principalmente la capa de dominio:

- [src/Dominio](src/Dominio)

En la practica, hoy tienes:

- Entidades y agregados de negocio.
- Objetos de valor (Value Objects).
- Errores de dominio y resultado funcional.
- Eventos de dominio.
- Contratos de repositorio y unidad de trabajo.
- Un servicio de dominio para calculo de precios.

No hay aun implementaciones de infraestructura (base de datos, mensajeria, API, etc.) ni casos de uso de aplicacion.

## 2. Vision de arquitectura

### 2.1 Decisiones de arquitectura aplicadas

1. Modelo centrado en dominio (Domain-first)
La solucion parte por definir primero el negocio y sus reglas, en lugar de empezar por controladores, ORM o UI.

2. Capa de dominio aislada
El proyecto [src/Dominio/Dominio.csproj](src/Dominio/Dominio.csproj#L1) solo tiene dependencias minimas y no depende de infraestructura concreta.

3. Contratos en lugar de implementaciones
Los repositorios y unidad de trabajo se definen como interfaces para que el dominio no conozca detalles de persistencia:

- [src/Dominio/Usuarios/IRepositorioUsuarios.cs](src/Dominio/Usuarios/IRepositorioUsuarios.cs#L1)
- [src/Dominio/Departamentos/IRepositorioDepartamentos.cs](src/Dominio/Departamentos/IRepositorioDepartamentos.cs#L1)
- [src/Dominio/Reservas/IRepositorioReservas.cs](src/Dominio/Reservas/IRepositorioReservas.cs#L1)
- [src/Dominio/Abstracciones/IUnidadDeTrabajo.cs](src/Dominio/Abstracciones/IUnidadDeTrabajo.cs#L1)

4. Reglas dentro del agregado, no fuera
Las transiciones de estado de una reserva se realizan dentro de [src/Dominio/Reservas/Reserva.cs](src/Dominio/Reservas/Reserva.cs#L1), evitando logica de negocio dispersa.

5. Modelo explicito de errores y resultados
En vez de usar excepciones para todo, se usa Resultado + Error para comunicar fallos esperables del negocio:

- [src/Dominio/Abstracciones/Resultado.cs](src/Dominio/Abstracciones/Resultado.cs#L1)
- [src/Dominio/Abstracciones/Error.cs](src/Dominio/Abstracciones/Error.cs#L1)

6. Eventos de dominio para desacoplar reacciones
Las entidades registran eventos de dominio, permitiendo reaccionar a cambios de negocio sin acoplarse a infraestructura:

- [src/Dominio/Abstracciones/Entidad.cs](src/Dominio/Abstracciones/Entidad.cs#L1)
- [src/Dominio/Reservas/Events/ReservaConfirmadaEventoDominio.cs](src/Dominio/Reservas/Events/ReservaConfirmadaEventoDominio.cs#L1)

7. Tipos ricos para lenguaje ubicuo
Se prefieren tipos de dominio (Calificacion, RangoFechas, Dinero, EstadoReserva) en lugar de primitivos sueltos.

### 2.2 Como deberia crecer esta arquitectura

Para el trabajo futuro, una evolucion recomendada:

1. Dominio: reglas e invariantes (ya iniciado).
2. Application: casos de uso, comandos/queries, orquestacion transaccional.
3. Infrastructure: EF Core, repositorios concretos, outbox, integraciones externas.
4. API/Web: endpoints, autenticacion, validaciones de entrada, serializacion.
5. Tests: unitarios del dominio + integracion de persistencia + pruebas de contrato API.

## 3. Conceptos DDD usados en este proyecto

## 3.1 Entidades

Una entidad tiene identidad propia y ciclo de vida. En este proyecto heredan de Entidad y usan Id.

- Base de entidades: [src/Dominio/Abstracciones/Entidad.cs](src/Dominio/Abstracciones/Entidad.cs#L1)
- Ejemplo: [src/Dominio/Reservas/Reserva.cs](src/Dominio/Reservas/Reserva.cs#L1)

## 3.2 Agregados y raiz de agregado

Reserva, Usuario, Departamento y Resena modelan comportamientos de negocio y protegen consistencia desde metodos propios.

- Reserva como agregado con transiciones de estado: [src/Dominio/Reservas/Reserva.cs](src/Dominio/Reservas/Reserva.cs#L1)
- Usuario con creacion controlada: [src/Dominio/Usuarios/Usuario.cs](src/Dominio/Usuarios/Usuario.cs#L1)

## 3.3 Objetos de valor (Value Objects)

Representan conceptos sin identidad, comparables por valor e inmutables por diseño.

Ejemplos:

- Dinero: [src/Dominio/Compartido/Dinero.cs](src/Dominio/Compartido/Dinero.cs#L1)
- Moneda: [src/Dominio/Compartido/Moneda.cs](src/Dominio/Compartido/Moneda.cs#L1)
- Rango de fechas: [src/Dominio/Reservas/RangoFechas.cs](src/Dominio/Reservas/RangoFechas.cs#L1)
- Calificacion: [src/Dominio/Resenas/Calificacion.cs](src/Dominio/Resenas/Calificacion.cs#L1)

## 3.4 Enumeraciones ricas (Smart Enum)

En vez de usar enums primitivos, se usa una abstraccion de enumerador con comportamiento.

- Base: [src/Dominio/Abstracciones/Enumerador.cs](src/Dominio/Abstracciones/Enumerador.cs#L1)
- Estado de reserva: [src/Dominio/Reservas/EstadoReserva.cs](src/Dominio/Reservas/EstadoReserva.cs#L1)
- Comodidades con porcentaje de recargo: [src/Dominio/Departamentos/Comodidad.cs](src/Dominio/Departamentos/Comodidad.cs#L1)

## 3.5 Servicio de dominio

Cuando una regla no pertenece naturalmente a una sola entidad, se extrae a servicio de dominio.

- Calculo de precios: [src/Dominio/Reservas/ServicioPrecios.cs](src/Dominio/Reservas/ServicioPrecios.cs#L1)

## 3.6 Eventos de dominio

Cada cambio relevante del negocio puede emitir eventos para reacciones posteriores.

- Interfaz de evento: [src/Dominio/Abstracciones/IEventoDominio.cs](src/Dominio/Abstracciones/IEventoDominio.cs#L1)
- Evento de reserva creada: [src/Dominio/Reservas/Events/ReservaReservadaEventoDominio.cs](src/Dominio/Reservas/Events/ReservaReservadaEventoDominio.cs#L1)
- Evento de usuario creado: [src/Dominio/Usuarios/Events/UsuarioCreadoEventoDominio.cs](src/Dominio/Usuarios/Events/UsuarioCreadoEventoDominio.cs#L1)
- Evento de resena creada: [src/Dominio/Resenas/Events/ResenaCreadaEventoDominio.cs](src/Dominio/Resenas/Events/ResenaCreadaEventoDominio.cs#L1)

## 3.7 Repositorios y unidad de trabajo

Los repositorios son puertos del dominio para cargar/guardar agregados. La unidad de trabajo coordina persistencia transaccional.

- Repositorios: ver seccion 2.1 punto 3.
- Unidad de trabajo: [src/Dominio/Abstracciones/IUnidadDeTrabajo.cs](src/Dominio/Abstracciones/IUnidadDeTrabajo.cs#L1)

## 3.8 Errores de dominio y Result

Los errores esperables del negocio se expresan explicitamente y se retornan como Resultado.

- Resultado: [src/Dominio/Abstracciones/Resultado.cs](src/Dominio/Abstracciones/Resultado.cs#L1)
- Error de Reservas: [src/Dominio/Reservas/ErroresReserva.cs](src/Dominio/Reservas/ErroresReserva.cs#L1)
- Error de Usuarios: [src/Dominio/Usuarios/ErroresUsuario.cs](src/Dominio/Usuarios/ErroresUsuario.cs#L1)
- Error de Departamentos: [src/Dominio/Departamentos/ErroresDepartamento.cs](src/Dominio/Departamentos/ErroresDepartamento.cs#L1)
- Error de Resenas: [src/Dominio/Resenas/ErroresResena.cs](src/Dominio/Resenas/ErroresResena.cs#L1)

## 4. Flujos de negocio modelados

1. Crear usuario
Se crea el agregado Usuario y se registra evento de dominio UsuarioCreado.

2. Reservar departamento
Se calcula precio con ServicioPrecios, se crea Reserva en estado Reservada y se registra evento ReservaReservada.

3. Transiciones de reserva
Desde Reservada se puede Confirmar o Rechazar.
Desde Confirmada se puede Completar o Cancelar (solo si no inicio el periodo).

4. Crear resena
Solo se permite crear resena cuando la reserva esta Completada.

## 5. Invariantes y reglas importantes

- Rango de fechas invalido: inicio no puede ser posterior a fin.
- Calificacion valida: debe estar entre 1 y 5.
- Dinero solo suma montos con la misma moneda.
- No se puede confirmar/rechazar una reserva fuera del estado Reservada.
- No se puede completar/cancelar una reserva fuera del estado Confirmada.
- No se puede reseñar una reserva no completada.

## 6. Guia para estudiantes: como usar esta base

1. Estudia primero el lenguaje del dominio
Recorre las carpetas de Reservas, Usuarios, Departamentos y Resenas para comprender entidades y objetos de valor.

2. Escribe pruebas unitarias del dominio antes de agregar capas
Casos minimos sugeridos:
- transiciones validas e invalidas de Reserva,
- calculo de precio con y sin comodidades,
- validacion de Calificacion y RangoFechas,
- elegibilidad de Resena.

3. Crea la capa Application
Implementa casos de uso (por ejemplo: ReservarDepartamento, ConfirmarReserva, CrearResena) usando repositorios y unidad de trabajo.

4. Crea la capa Infrastructure
Implementa persistencia con EF Core, mapeos de Value Objects, y despacho de eventos de dominio.

5. Expone API o UI
Agrega endpoints y DTOs sin mover reglas de negocio fuera del dominio.

6. Mantiene el dominio limpio
Si una regla es del negocio, debe vivir en Dominio, no en controlador ni repositorio.

## 7. Decisiones que conviene mantener en trabajos futuros

- Mantener nombres del negocio para reforzar lenguaje ubicuo.
- Mantener metodos de fabrica y constructores privados cuando haya invariantes.
- Mantener errores tipados y Resultado para flujos esperables.
- Mantener eventos de dominio para desacoplar efectos secundarios.
- Evitar exponer setters publicos que rompan invariantes.

## 8. Posibles mejoras academicas (siguientes iteraciones)

- Agregar politicas de cancelacion con penalizacion.
- Agregar disponibilidad y calendario por departamento.
- Agregar promedio de calificaciones por departamento.
- Introducir CQRS basico en Application.
- Implementar patron Outbox para eventos de dominio.
- Agregar pruebas de arquitectura para proteger fronteras entre capas.

## 9. Glosario rapido de C#

Esta seccion resume terminos que aparecen mucho en este proyecto y en codigo C# profesional.

### 9.1 var

- Que es: una forma de declarar una variable dejando que el compilador infiera el tipo.
- Importante: var no significa tipo dinamico. El tipo sigue siendo fuerte y fijo.
- Cuando usarlo:
	- cuando el tipo es obvio por el lado derecho,
	- cuando evita repetir tipos largos.
- Cuando evitarlo:
	- cuando hace el codigo ambiguo para quien recien aprende.

Ejemplo simple:

		var total = 10;          // total es int
		var nombre = "Ana";     // nombre es string

### 9.2 abstract

- Que es: una clase base incompleta que no se puede instanciar directamente.
- Para que sirve: definir una plantilla comun para varios tipos hijos.
- En este proyecto: Comodidad se modela como abstraccion con variantes concretas.

### 9.3 sealed

- Que es: una clase que no puede heredarse.
- Para que sirve: proteger el comportamiento de un tipo y evitar extensiones no deseadas.
- En este proyecto: varias entidades y eventos se declaran sealed para fijar su comportamiento.

### 9.4 record

- Que es: un tipo muy util para objetos de valor.
- Ventaja principal: compara por valor (contenido) en lugar de identidad.
- En este proyecto: se usa en Value Objects como Nombre, Dinero, RangoFechas y Calificacion.

### 9.5 interface

- Que es: un contrato de comportamiento sin implementacion.
- Para que sirve: desacoplar dominio de infraestructura.
- En este proyecto: IRepositorioUsuarios, IRepositorioReservas e IUnidadDeTrabajo son contratos.

### 9.6 static

- Que es: miembro o clase que pertenece al tipo y no a una instancia.
- Uso tipico: metodos de fabrica, utilidades o catalogos de errores.
- En este proyecto: Resultado.Exito, Resultado.Fallo y clases de Errores usan esta idea.

### 9.7 Convenciones de nombres

Estas reglas son clave para leer y escribir codigo mantenible.

1. PascalCase
Se escribe cada palabra con inicial mayuscula.

Uso recomendado en C#:
- Clases, records, interfaces, metodos, propiedades.

Ejemplos:
- Reserva
- Crear
- PrecioTotal
- IRepositorioReservas

2. camelCase
Primera palabra en minuscula y siguientes en mayuscula.

Uso recomendado en C#:
- Variables locales y parametros de metodos.

Ejemplos:
- precioTotal
- usuarioId
- creadoEnUtc

3. _underscore (prefijo con guion bajo)
Se usa normalmente para campos privados.

Uso recomendado en C#:
- campos privados que almacenan estado interno.

Ejemplos:
- _valor
- _eventosDominio

Nota para este repositorio:
- Veras muchos nombres en castellano para reforzar lenguaje ubicuo del negocio.
- Lo importante es ser consistente y mantener una sola convencion en todo el proyecto.

### 9.8 Regla practica para decidir nombres

1. Si representa un tipo (clase, record, interfaz): PascalCase.
2. Si es variable local o parametro: camelCase.
3. Si es campo privado: _underscore + camelCase.
4. Si el nombre expresa negocio: priorizar terminos del dominio antes que tecnicismos.

## 10. Comandos utiles

Restaurar y compilar:

- dotnet restore
- dotnet build AirBnb.sln

Formatear codigo (si se usa csharpier):

- dotnet tool restore
- dotnet csharpier format .

---

