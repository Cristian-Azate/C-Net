//Contexto: van todas las relaciones de los modelos para trasnformarlos en colecciones que van a representarse como tablas dentro de la base de datos

using Microsoft.EntityFrameworkCore;
using c_net.models;

namespace c_net;

//Db Context es la clase de Fluent API para configurar el Modelo
public class TareasContext: DbContext
{
    //Representan la tabla  es decir coleccion de datos de los modelos -- por eso el nombre en plural
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}

    //Metodo base del contructor y Configuracion de Entity Framework
    //lo nombro como options y utilizar el metodo base y le aso options
    public TareasContext(DbContextOptions<TareasContext> options) :base(options) 
    {
    }

    //sobrescribir un metodo interno dbcontec que es que diseña la bd
    //sobrescirbir la logica de los modelos o configurar los modelos
    //CONFIGURAR LA ENTIDAD
    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        //creamos una coleccion
        //data inicial o datos iniciales
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() {CategoriId = Guid.Parse("642f2d1e-6e5d-4f40-9260-c7ba1280727c"), Nombre="Actividades Pendientes", Peso=20});
        categoriasInit.Add(new Categoria() {CategoriId = Guid.Parse("642f2d1e-6e5d-4f40-9260-c7ba12807202"), Nombre="Actividades Personales", Peso=50});

        //configuro el modelo categoria --es decir -- construir o diseñar las restricciones para este modelo en base a FUNCIONES
        modelBuilder.Entity<Categoria>(categoria=>
        {
            //especifico que se convierta en una tabla
            categoria.ToTable("Categoria");
            categoria.HasKey(p=>p.CategoriId);

            categoria.Property(p=>p.Nombre).IsRequired().HasMaxLength(150);

            //required false habilita datos nulos y no me pide q asigne valor 
            categoria.Property(p=>p.Descripcion).IsRequired(false); //la especifico  TODAS LAS PROPIEDADES aunque no tenga restricciones

            categoria.Property(p=>p.Peso);

            //agrego los datos iniciales
            categoria.HasData(categoriasInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea() {TareaId = Guid.Parse("642f2d1e-6e5d-4f40-9260-c7ba12807210"), CategoriaId=Guid.Parse("642f2d1e-6e5d-4f40-9260-c7ba1280727c"), Prioridad_Tarea= Prioridad.Media, Titulo="Pago de servicios", Fecha_Creacion = DateTime.Now});
        tareasInit.Add(new Tarea() {TareaId = Guid.Parse("642f2d1e-6e5d-4f40-9260-c7ba12807211"), CategoriaId=Guid.Parse("642f2d1e-6e5d-4f40-9260-c7ba12807202"), Prioridad_Tarea= Prioridad.Baja, Titulo="Terminar serie netflix", Fecha_Creacion = DateTime.Now});

         //configuro el modelo categoria --es decir -- construir o diseñar las restricciones para este modelo
        modelBuilder.Entity<Tarea>(tarea=>
        {
            //especifico que se convierta en una tabla
            tarea.ToTable("Tarea");
            tarea.HasKey(p=>p.TareaId);

            //clave foreana
            //existe una propiedad categoria-- y que esa categoria tiene relacion con multiples tareas
            //o una categoria tiene una coleccion de tareas
            tarea.HasOne(p=> p.Categoria).WithMany(p=>p.Tareas).HasForeignKey(p=>p.CategoriaId);

            tarea.Property(p=>p.Titulo).IsRequired().HasMaxLength(200);

            tarea.Property(p=>p.Descripcion).IsRequired(false); //la especifico  TODAS LAS PROPIEDADES aunque no tenga restricciones
            tarea.Property(p=>p.Prioridad_Tarea);
            tarea.Property(p=>p.Fecha_Creacion);

            //funcion para ignorar campos
            tarea.Ignore(p=>p.Resumen);

            tarea.HasData(tareasInit);
        });
    }
}