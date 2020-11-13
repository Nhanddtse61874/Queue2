using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using Business;
using Business.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Persistence.DataAccess;
using Persistence.Models;
using Persistence.Repositories;
using RecommenceSystemCapstoneV2.Controllers;
using RecommenceSystemCapstoneV2.ViewModels;

namespace TriggerQueu
{
    public static class Function1
    {

        [FunctionName("QueueTrigger")]
        public static void Run(
            [QueueTrigger("azure", Connection = "AzureWebJobsStorage")] 
            string message, 
            ILogger log)
        {
            try
            {

                RSDbContext context = new RSDbContext();
                IMapper _mapper = AutoMapperConfig.MapperConfig().CreateMapper();
                IRecommenceHobbyService _hobbyService = new RecommenceHobbyService(new UnitOfWork(context));
                ICategoryService _categoryService = new CategoryService(new UnitOfWork(context));
                IUserService _userService = new UserService(new UnitOfWork(context));
                IProductService _productService = new ProductService(new UnitOfWork(context));

                var recommence = JsonSerializer.Deserialize<Recommence>(message);

                var result = _hobbyService.RecommenceByHobbyGetListProduct(recommence);
                var userId = recommence.UserId;

                //create new category if not exist
                foreach (var item in result)
                {
                    var categoryId = item.CategoryId;
                    if (_categoryService.CheckCategory(categoryId))
                    {
                        CreateCategoryViewModel newCate = new CreateCategoryViewModel();
                        newCate.Code = categoryId;
                        _categoryService.Create(_mapper.Map<Category>(newCate));
                    }
                }

                //create products if not exist
                var products = _mapper.Map<IEnumerable<CreateProductViewModel>>(result)
                    .Select(x => _productService.Create(_mapper.Map<Product>(x)));

                //create new user if not exist
                CreateUserViewModel newUser = new CreateUserViewModel();
                newUser.Code = userId;
                _userService.Create(_mapper.Map<User>(newUser));

                //select list products'code
                IEnumerable<string> listCode = result.Select(x => x.Code);

                //create RecommenceHobbyModel to save database
                CreateRecommenceByHobbyViewModel hobbyViewModel = new CreateRecommenceByHobbyViewModel();
                var list = _mapper.Map<IEnumerable<ProductViewModel>>(products);
                hobbyViewModel.ProductRecommenceHobbies = list;
                hobbyViewModel.UserId = userId;
                var a = _mapper.Map<RecommenceHobby>(hobbyViewModel);
                _hobbyService.LoadAndUpdate(a);

///--------------------------------------------------------------------
///

            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                throw;
            }
        }
    }

    
}
