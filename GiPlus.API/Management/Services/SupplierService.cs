using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Repositories;
using GiPlus.API.Management.Domain.Services;
using GiPlus.API.Management.Domain.Services.Communication;
using GiPlus.API.Security.Domain.Repositories;
using GiPlus.API.Shared.Domain.Repositories;

namespace GiPlus.API.Management.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public SupplierService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<Supplier>> ListAsync()
    {
        return await _supplierRepository.ListAsync();
    }

    public async Task<IEnumerable<Supplier>> ListByUserIdAsync(int userId)
    {
        return await _supplierRepository.FindByUserIdAsync(userId);
    }

    public async Task<SupplierResponse> SaveAsync(Supplier supplier)
    {
        //Validate UserId
        var existingUser = await _userRepository.FindByIdAsync(supplier.UserId);
        if (existingUser == null)
            return new SupplierResponse("Invalid User");
        try
        {
            //Add supplier
            await _supplierRepository.AddSync(supplier);
            //Complete transaction
            await _unitOfWork.CompleteAsync();
            //Return Response
            return new SupplierResponse(supplier);
        }
        catch (Exception e)
        {
            //Error Handling
            return new SupplierResponse($"An error occurred while saving the supplier: {e:Message}");
        }
    }

    public async Task<SupplierResponse> UpdateAsync(int supplierId, Supplier supplier)
    {
        var existingSupplier = await _supplierRepository.FindByIdAsync(supplierId);
        //Validate client
        if (existingSupplier == null)
            return new SupplierResponse("Supplier not found");
        //Validate user
        var existingUser = await _userRepository.FindByIdAsync(supplier.UserId);
        if (existingUser == null)
            return new SupplierResponse("Invalid User");
        
        //Modify Fields
        existingSupplier.Name = supplier.Name;
        existingSupplier.Ruc = supplier.Ruc;
        existingSupplier.Phone = supplier.Phone;
        existingSupplier.Email = supplier.Email;
        existingSupplier.Address=supplier.Address;

        try
        {
            _supplierRepository.Update(existingSupplier);
            await _unitOfWork.CompleteAsync();

            return new SupplierResponse(existingSupplier);
        }
        catch (Exception e)
        {
            //Error Handling
            return new SupplierResponse($"An error occurred while updating the supplier: {e.Message}");
        }
    }

    public async Task<SupplierResponse> DeleteAsync(int supplierId)
    {
        var existingSupplier = await _supplierRepository.FindByIdAsync(supplierId);

        if (existingSupplier == null)
            return new SupplierResponse("Supplier not found");

        try
        {
            _supplierRepository.Remove(existingSupplier);
            await _unitOfWork.CompleteAsync();

            return new SupplierResponse(existingSupplier);
        }
        catch (Exception e)
        {
            return new SupplierResponse($"An error occurred while deleting the supplier: {e.Message}");
        }
    }
}