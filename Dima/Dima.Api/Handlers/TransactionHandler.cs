using Dima.Api.Data;
using Dima.core.Common.Extensions;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Transactions;
using Dima.core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class TransactionHandler(AppDbContext context): ITransactionHandler
{
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Amount = request.Amount,
                PaidOrReceivedAt = request.PairdOrReceivedAt,
                Title = request.Title,
                Type = request.Type
            };

            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, StatusCodes.Status201Created, "Transação Criada com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<Transaction?>( null, StatusCodes.Status500InternalServerError,
                "Não foi possível criar transação");
        }

    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (transaction is null)
                return new Response<Transaction?>(data: null, StatusCodes.Status404NotFound,
                    "Transação não encontrada");
                
            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.PaidOrReceivedAt = request.PairdOrReceivedAt;
            transaction.Title = request.Title;

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<Transaction?>(null, StatusCodes.Status500InternalServerError,
                "Não foi possível atualizar a transação");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (transaction is null)
                return new Response<Transaction?>(data: null, StatusCodes.Status404NotFound,
                    "Transação não encontrada");
            
            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();
            
            return new Response<Transaction?>(transaction);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<Transaction?>(null, StatusCodes.Status500InternalServerError,
                "Não foi possível deletar a transação");
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return transaction is null ?
                new Response<Transaction?>(data: null, StatusCodes.Status404NotFound, "Transação não encontrada")
                : new Response<Transaction?>(transaction);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<Transaction?>(null, StatusCodes.Status500InternalServerError,
                "Não foi possível buscar a transação");
        }
    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new PagedResponse<List<Transaction>?>(null, StatusCodes.Status500InternalServerError,
                "Não foi possível determinar a data de início ou término");
        }

        try
        {
            var query = context
                .Transactions
                .AsNoTracking()
                .Where(x => 
                    x.CreatedAt >= request.StartDate 
                    && x.CreatedAt <= request.EndDate
                    && x.UserId == request.UserId
                )
                .OrderBy(x => x.CreatedAt);
        
            var transaction = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();
        
            return new PagedResponse<List<Transaction>?>(transaction, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new PagedResponse<List<Transaction>?>(null, StatusCodes.Status500InternalServerError, "Não foi possível buscar as transacoes"); ;
        }
    }
}