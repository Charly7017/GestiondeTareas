using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GestionDeTareas.Models
{
	public class Tarea
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name = "Nombre")]
		public string Nombre { get; set; }
		[Display(Name = "Tipo Tarea")]
		public int TipoTareaId { get; set; }
		[Display(Name = "Descripcion")]
		public string Descripcion { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime FechaCreacion { get; set; } = DateTime.Today;
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime FechaLimite { get; set; } = DateTime.Today;
		public bool Completada { get; set; } = false;
		public string TareaNombre { get; set; }
		public string Categoria { get; set; }

    }
}
