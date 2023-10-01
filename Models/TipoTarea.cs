using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GestionDeTareas.Models
{
	public class TipoTarea
	{
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
		[Display(Name ="Nombre del tipo tarea")]
		[Remote(action:"VerificarExisteTipoTarea",controller:"TiposTareas")]
		public string Nombre { get; set; }
		public int UsuarioId { get; set; }
	}
}
