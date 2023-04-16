using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c_net.models;

    public class Tarea
    {
        //[Key]
        public Guid TareaId {get;set;}
        //[ForeignKey("CategoriaId")]
        public Guid CategoriaId {get;set;}
        //[Required]
        //[MaxLength(200)]
        public string Titulo {get;set;}
        public string Descripcion {get;set;}  
        public Prioridad Prioridad_Tarea {get;set;}
        public DateTime Fecha_Creacion {get;set;} 
        
        //relacionar uno con el otro
        //CLAVE VIRTUAL SE UTILIZA PARA MODIFICAR UN MÉTODO, PROPIEDAD, INDIZADOR O DECLARACIÓN DE EVENTO Y PERMITE INVALIDAR CUALQUIERA DE ESTOS ELEMENTOS EN UNA CLASE DERIVADA.
        public virtual Categoria Categoria {get;set;}
        //Omitir Atributo que no queqremos que se cree en la BD
        //sino que se generaria dentro del codigo
        //[NotMapped]
        public string Resumen;
    }

    public enum Prioridad
    {
        Baja,
        Media,
        Alta
    }
