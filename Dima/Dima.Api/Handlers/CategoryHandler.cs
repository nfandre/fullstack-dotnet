using Dima.Api.Data;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;
using Dima.core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Descritpion = request.Description
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            
            return new Response<Category?>(category, StatusCodes.Status201Created, "Categoria Criada com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return new Response<Category?>(null, StatusCodes.Status500InternalServerError, "Não foi possível criar categoria");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(
                    category => category.Id == request.Id && category.UserId == request.UserId
                );

            if (category is null)
                return new Response<Category?>(null, StatusCodes.Status404NotFound, "Categoria não encontrada");

            category.Title = request.Title;
            category.Descritpion = request.Description;

            context.Categories.Update(category);
            await context.SaveChangesAsync();
        
            return new Response<Category?>(category, message: "Categoria atualizada com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<Category?>(null, StatusCodes.Status500InternalServerError, "Não foi possível alterar categoria");
        }

    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(
                    category => category.Id == request.Id && category.UserId == request.UserId
                );
        
            if (category is null)
                return new Response<Category?>(null, StatusCodes.Status404NotFound, "Categoria não encontrada");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            
            return new Response<Category?>(category, message: "Categoria exlcuída com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<Category?>(null, StatusCodes.Status500InternalServerError, "Não foi possível excluir categoria");
        }

    }

    public async Task<Response<Category?>> GetByIdAsync(GetCagetoryByIdRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .AsNoTracking() //Evita monitorar dados no banco, evitar observar mudanças no banco
                .FirstOrDefaultAsync(
                    category => category.Id == request.Id && category.UserId == request.UserId
                );
            return category is null ? 
                new Response<Category?>(null, StatusCodes.Status404NotFound, "Categoria não encontrada")
                :   new Response<Category?>(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<Category?>(null, StatusCodes.Status500InternalServerError, "Não foi possível bascar a categoria");
        }

    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context
                .Categories
                .AsNoTracking()
                .Where(c => c.UserId == request.UserId)
                .OrderBy(c => c.Title);
            
            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();
            
            return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new PagedResponse<List<Category>>(null, StatusCodes.Status500InternalServerError, "Não foi possível buscar as categorias");
        }
    }
}