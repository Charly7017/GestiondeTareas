using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionDeTareas.Models
{
	public class TareaCreacionViewModel:Tarea
	{
        public IEnumerable<SelectListItem> TiposTareas { get; set; }
    }
}
