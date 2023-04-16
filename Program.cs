using c_net;
using c_net.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);



//Antes de que se construya configuramos Entity Framework
//builder.Services.AddDbContext<TareasContext>(p=> p.UseInMemoryDatabase("TareasDB")); //en memoria
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas")); //en SQL SERVER la cadena esta en appsettings

//
var app = builder.Build();

//endpoints
app.MapGet("/", () => "Hello World!");

//metodo asincrono                  recibo el contexto de entity framework utilizando el atributo From services
//dbContext es el nombre de la variable que recibe el contexto
//CREO LA BASE DE DATOS
app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) =>
{
    //codido para crear la base de datos y verificar que se puede crear
    dbContext.Database.EnsureCreated();
    //si creo muestra un dato sino un error
    return Results.Ok("Base de Datos en Memoria: "+ dbContext.Database.IsInMemory());
});

//CONSULTAR DATOS
app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    //dentro del contexto esta la coleecion de tareas
    //que traiga los datos de la categoria
    // return Results.Ok(dbContext.Tareas.Include(p=>p.Categoria).Where(p=>p.Prioridad_Tarea  == c_net.models.Prioridad.Baja));
    return Results.Ok(dbContext.Tareas.Include(p=>p.Categoria));
});

//GUARDAR DATOS
//                                                                       ESPECIFICO EL MODELO O LOS DATOS Q VAMOS A RECIBIR PARA GUARDAR
//                                                                       INDICAR QUE DESDE el cuerpo del REQUEST NOS VA A LLEGAR EL OBJETO TAREA
app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.Fecha_Creacion = DateTime.Now;

    //funcion para agregar el nuevo elemento al contexto 
    await dbContext.AddAsync(tarea);

    //otra forma agregando a la coleccion
    //await dbContext.Tareas.AddAsync(tarea);

    //metodo para confirmar los cambios en el contexto
    await dbContext.SaveChangesAsync();

    //si sucede un error el metodo devuelve el error y no devuelve el OK
    return Results.Ok();
});

//ACTUALIZAR DATOS
//                                                                                              DESDE LA RUTA
app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea,[FromRoute] Guid id) =>
{
    //busco 1ro la tarea actual

    var tareaActual = dbContext.Tareas.Find(id); //si coloco id busca la key

    if(tareaActual != null)
    {
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.Prioridad_Tarea = tarea.Prioridad_Tarea;
        tareaActual.Descripcion = tarea.Descripcion;

        //metodo para confirmar los cambios en el contexto
        await dbContext.SaveChangesAsync();

        //si sucede un error el metodo devuelve el error y no devuelve el OK
        return Results.Ok();
    }

    return Results.NotFound();
  
});

//ELIMINAR DATOS

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea,[FromRoute] Guid id) =>
{
    //busco 1ro la tarea actual

    var tareaActual = dbContext.Tareas.Find(id); //si coloco id busca la key

    if(tareaActual != null)
    {
        dbContext.Remove(tareaActual);

         //metodo para confirmar los cambios en el contexto
        await dbContext.SaveChangesAsync();

        //si sucede un error el metodo devuelve el error y no devuelve el OK
        return Results.Ok();
    }

    return Results.NotFound();
  
});

app.Run();
