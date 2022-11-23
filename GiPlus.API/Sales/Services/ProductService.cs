using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Repositories;
using GiPlus.API.Sales.Domain.Services;
using GiPlus.API.Sales.Domain.Services.Communication;
using GiPlus.API.Security.Domain.Repositories;
using GiPlus.API.Shared.Domain.Repositories;

namespace GiPlus.API.Sales.Services;

public class ProductService : IProductService
{
   private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _productRepository.ListAsync();
    }

    public async Task<IEnumerable<Product>> ListByUserIdAsync(int userId)
    {
        return await _productRepository.FindByUserIdAsync(userId);
    }

    public async Task<ProductResponse> SaveAsync(Product product)
    {
        //Validate UserId
        var existingUser = await _userRepository.FindByIdAsync(product.UserId);
        if (existingUser == null)
            return new ProductResponse("Invalid User");
        try
        {
            //Add product
            await _productRepository.AddSync(product);
            //Complete transaction
            await _unitOfWork.CompleteAsync();
            //Return Response
            return new ProductResponse(product);
        }
        catch (Exception e)
        {
            //Error Handling
            return new ProductResponse($"An error occurred while saving the product: {e:Message}");
        }
    }

    public async Task<ProductResponse> UpdateAsync(int productId, Product product)
    {
        var existingProduct = await _productRepository.FindByIdAsync(productId);
        //Validate client
        if (existingProduct == null)
            return new ProductResponse("Product not found");
        //Validate user
        var existingUser = await _userRepository.FindByIdAsync(product.UserId);
        if (existingUser == null)
            return new ProductResponse("Invalid User");
        
        //Modify Fields
        existingProduct.Name = product.Name;
        existingProduct.Brand = product.Brand;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;

        try
        {
            _productRepository.Update(existingProduct);
            await _unitOfWork.CompleteAsync();

            return new ProductResponse(existingProduct);
        }
        catch (Exception e)
        {
            //Error Handling
            return new ProductResponse($"An error occurred while updating the product: {e.Message}");
        }
    }

    public async Task<ProductResponse> DeleteAsync(int productId)
    {
        var existingProduct = await _productRepository.FindByIdAsync(productId);

        if (existingProduct == null)
            return new ProductResponse("Product not found");

        try
        {
            _productRepository.Remove(existingProduct);
            await _unitOfWork.CompleteAsync();

            return new ProductResponse(existingProduct);
        }
        catch (Exception e)
        {
            return new ProductResponse($"An error occurred while deleting the product: {e.Message}");
        }
    }
}