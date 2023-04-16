using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace c_net.models;

public class Categoria
{
    //Notacion o Atributos --- permiten asigar restricciones tambien
    //[Key]
    public Guid CategoriId {get;set;}
    
    //Atributo Requerido al insertar un nuevo registro
    //[Required]
    //[MaxLength(150)]
    public string Nombre{get;set;}
    public string Descripcion {get;set;}

    public int Peso {get;set;}

    //relacionar uno con el otro
    //CLAVE VIRTUAL SE UTILIZA PARA MODIFICAR UN MÉTODO, PROPIEDAD, INDIZADOR O DECLARACIÓN DE EVENTO Y PERMITE INVALIDAR CUALQUIERA DE ESTOS ELEMENTOS EN UNA CLASE DERIVADA.
    //todas las tareas asocicadas a la categoria
    [JsonIgnore] // al momento de consultar no me traiga el listado de tareas
    public virtual ICollection<Tarea> Tareas {get;set;}
}