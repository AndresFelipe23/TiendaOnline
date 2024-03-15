
# Tienda Online - Comercio Electrónico en ASP.NET Core MVC

Este proyecto de comercio electrónico desarrollado en ASP.NET Core MVC. Esta aplicación web ofrece una plataforma de compras en línea que permite a los usuarios buscar productos, agregarlos al carrito de compras y proceder con la compra. El proyecto está diseñado con fines educativos y destaca varios conceptos clave de desarrollo web.


## Tech Stack

- **ASP.NET Core MVC**
- **SQL Server**
- **Entity Framework**
- **Migrations**
- **Bootstrap**
- **Font Awesome**
- **Code Generation**
- **C#**
- **.NET Core SDK**




## Screenshots
![INICIO](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/0c75019a-72d6-4537-922a-c53d82bca842)

![PRODUCTOS](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/c31e5926-3bd6-42bb-9235-052439dc3d2e)

![DETALLE PRODUCTO](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/92805760-92a7-4de2-a583-4e2f7520a63c)

![CARRITO DE COMPRAS](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/7b993ed0-70ba-49f6-84bc-c76368cccdff)

![LOGIN](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/7cd4a2fa-87e2-426c-acda-6408a2660802)

![REGISTRO](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/6f7c0f5b-a158-47d9-a8d3-63e3bb2e6384)

![PERFIL](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/fb78c59e-ae91-4f95-8017-28b33efa08ac)

![ADMIN DASBOARD](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/16b030d0-277f-4b35-9115-de34510d8861)

![PRODUCTOS ADMIN](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/9e005859-5aa2-4c17-8ba5-aa35fe4aced5)

![PAGO PAYPAL](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/0180b37b-2537-4924-b9bd-1b9e4bb662c9)

![PAGO REALIZADO](https://github.com/AndresFelipe23/TiendaOnline/assets/75282613/60cfe981-f283-4198-8b88-3c8544254ea3)


## Patrón de arquitectura MVC

El proyecto sigue una arquitectura Modelo-Vista-Controlador (MVC), que es un patrón de diseño ampliamente utilizado en aplicaciones web. Esta arquitectura separa claramente la lógica de negocio (Modelo) de la presentación (Vista) y la lógica de control (Controlador). Esto permite una estructura organizada y mantenible para el proyecto.

## Archivos del Proyecto

El proyecto consta de múltiples archivos y carpetas. Los principales incluyen:

- Modelos: Contiene las clases que representan los objetos del mundo real, como productos y carritos.

- Controladores: Manejan las solicitudes del usuario, interactúan con los modelos y devuelven vistas o datos al cliente.

- Vistas: Definen la interfaz de usuario y cómo se presentan los datos al usuario.

- Migrations: Almacena las migraciones de base de datos para actualizar el esquema de la base de datos.


## Instrucciones de Ejecución

Para ejecutar y probar este proyecto, siga estos pasos:

1. Asegúrese de tener instalados el .NET Core SDK y Visual Studio Code en su sistema.

2. Clone este repositorio en su máquina local.

3. Abra el proyecto en Visual Studio Code.

4. Configure la cadena de conexión de la base de datos en el archivo appsettings.json.

5. Abra la terminal y navegue hasta la carpeta raíz del proyecto.

6. Ejecute el comando dotnet ef database update para aplicar las migraciones y crear la base de datos.

- Deberá insertar los roles Administrador, Staff y Cliente para que el formulario de registro funcione.

- Puede restaurar la base de datos desde el .bak para hacer uso de los registros de prueba como: Categorías, Roles, Productos y Banners. Usuario Administrador agregado con Usuario='admin' y clave='InfoToolsSV'.

7. Ejecute el proyecto con el comando dotnet run.

8. Abra su navegador web y vaya a https://localhost:5001 para acceder a la aplicación.

```bash
  dotnet run
```
    
## License

Este proyecto se distribuye bajo una Licencia Educativa que permite el uso exclusivamente con fines educativos. No está permitido vender el proyecto ni compartirlo con fines comerciales. La licencia no permite la redistribución o el uso del proyecto en aplicaciones comerciales o de producción.




## Authors

- [InfoToolsSV ](https://www.youtube.com/@InfoToolsSV/membership)
- [Andres Espitia ](https://github.com/AndresFelipe23)

