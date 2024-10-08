using System.ComponentModel.DataAnnotations;

namespace Dima.core.Requests.Categories;

public class CreateCategoryRequest : Request
{
    [Required(ErrorMessage = "Título inválido")]
    [MaxLength(ErrorMessage = "O título deve conter 80 caracteres")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Descrição inválido")]
    public string Description { get; set; } = string.Empty;
}