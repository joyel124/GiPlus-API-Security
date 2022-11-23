using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Repositories;
using GiPlus.API.Sales.Domain.Services;
using GiPlus.API.Sales.Domain.Services.Communication;
using GiPlus.API.Security.Domain.Repositories;
using GiPlus.API.Shared.Domain.Repositories;

namespace GiPlus.API.Sales.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public SaleService(ISaleRepository saleRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _saleRepository = saleRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<Sale>> ListAsync()
    {
        return await _saleRepository.ListAsync();
    }

    public async Task<IEnumerable<Sale>> ListByUserIdAsync(int userId)
    {
        return await _saleRepository.FindByUserIdAsync(userId);
    }

    public async Task<SaleResponse> SaveAsync(Sale sale)
    {
        //Validate UserId
        var existingUser = await _userRepository.FindByIdAsync(sale.UserId);
        if (existingUser == null)
            return new SaleResponse("Invalid User");
        //Validate client Name
        try
        {
            //Add sale
            await _saleRepository.AddSync(sale);
            //Complete transaction
            await _unitOfWork.CompleteAsync();
            //Return Response
            return new SaleResponse(sale);
        }
        catch (Exception e)
        {
            //Error Handling
            return new SaleResponse($"An error occurred while saving the sale: {e:Message}");
        }
    }

    public async Task<SaleResponse> UpdateAsync(int saleId, Sale sale)
    {
        var existingSale = await _saleRepository.FindByIdAsync(saleId);
        //Validate client
        if (existingSale == null)
            return new SaleResponse("Sale not found");
        //Validate user
        var existingUser = await _userRepository.FindByIdAsync(sale.UserId);
        if (existingUser == null)
            return new SaleResponse("Invalid User");
        
        //Modify Fields
        existingSale.Date = sale.Date;
        existingSale.PaymentVoucher = sale.PaymentVoucher;
        existingSale.SaleDetails = sale.SaleDetails;
        existingSale.ClientId = sale.ClientId;

        try
        {
            _saleRepository.Update(existingSale);
            await _unitOfWork.CompleteAsync();

            return new SaleResponse(existingSale);
        }
        catch (Exception e)
        {
            //Error Handling
            return new SaleResponse($"An error occurred while updating the sale: {e.Message}");
        }
    }

    public async Task<SaleResponse> DeleteAsync(int saleId)
    {
        var existingSale = await _saleRepository.FindByIdAsync(saleId);

        if (existingSale == null)
            return new SaleResponse("Sale not found");

        try
        {
            _saleRepository.Remove(existingSale);
            await _unitOfWork.CompleteAsync();

            return new SaleResponse(existingSale);
        }
        catch (Exception e)
        {
            return new SaleResponse($"An error occurred while deleting the sale: {e.Message}");
        }
    }
}