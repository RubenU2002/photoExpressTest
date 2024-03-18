# PhotoExpress Project

## Descripción
PhotoExpress es un proyecto dividido en dos partes principales: un frontend construido con Angular 17 y un backend desarrollado en .NET 8 (WebAPI con controladores). Este proyecto está diseñado para proporcionar una solución integral para el manejo de eventos.

## Requisitos Previos
Antes de comenzar, asegúrate de tener instalado lo siguiente:
- [Node.js](https://nodejs.org/) (Incluye npm) para el frontend.
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download) para el backend.
- [Sql Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads) para la persistencia de datos
## Instalación y Configuración
### Base de datos (photoExpressDB)
1. Crear la base de datos con el siguiente comando: create database photoExpressDB
2. Ejecutar el Script adjunto (script tablas.sql)
3. configurar la cadena de conexión en el archivo appsetting.json del proyecto `photoExpressBackend/webapi` según se haya configurado el sql server
### Backend (photoExpressBackend)
1. Abre una terminal o línea de comandos.
2. Navega al directorio `photoExpressBackend/webapi` dentro del proyecto.
3. Ejecuta el siguiente comando para restaurar los paquetes necesarios:
dotnet restore
4. Para iniciar el servidor backend, ejecuta: dotnet run
5. El backend debería estar corriendo ahora en `http://localhost:5000/` (o el puerto que hayas configurado).
6. Esta será la url a consumir desde el front, y la cual se debe configurar en el archivo ubicado en `PhotoExpressFrontend/src/environment/environment.ts`  en el atributo apiUrl

### Frontend (photoExpressFrontend)
1. Abre una nueva terminal o línea de comandos.
2. Navega al directorio `photoExpressFrontend`.
3. Instala las dependencias del proyecto ejecutando: npm install
4. Una vez finalizada la instalación, inicia la aplicación con: ng serve
5. El frontend estará disponible en `http://localhost:4200/`.

## Uso
- Navega a `http://localhost:4200/` en tu navegador para ver la interfaz de PhotoExpress.
- La API backend es accesible en `http://localhost:5000/` o ruta dada por .Net al ejecutar "dotnet run".
## Documentación tecnica
1. La documentación tecnica se encontrará en la carpeta "Docs"