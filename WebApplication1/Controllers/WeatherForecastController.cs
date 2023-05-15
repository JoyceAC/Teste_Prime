using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Codility.WarehouseApi
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;            
        }        

        // Return OkObjectResult(IEnumerable<WarehouseEntry>)
        public IActionResult GetProducts()
        {            
            List<WarehouseEntry> productList = new List<WarehouseEntry>();            

            foreach (ProductRecord productRecord in _warehouseRepository.GetProductRecords())
            {
                if(productRecord.Quantity > 0)
                {
                    WarehouseEntry product = new WarehouseEntry();
                    product.ProductId = productRecord.ProductId;
                    product.Quantity = productRecord.Quantity;

                    productList.Add(product);
                }
            }

            return Ok(productList);
            // Console.WriteLine("Sample debug output");
            throw new NotImplementedException();
        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooLowMessage)
        public IActionResult SetProductCapacity(int productId, int capacity, string s)
        {

            //HttpResponseMessage respMessage = new HttpResponseMessage();
            Microsoft.AspNetCore.Mvc.ActionResult actionResult = null;

            int qtdeAtual = 0;
            foreach (ProductRecord productRecord in _warehouseRepository.GetProductRecords())
            {
                if (productRecord.ProductId == productId)
                {
                    qtdeAtual = productRecord.Quantity;
                    break;
                }
            }
            if (capacity > 0 && capacity > qtdeAtual)
            {
                try
                {
                    _warehouseRepository.SetCapacityRecord(productId, capacity);
                    actionResult = Ok();
                }
                
                catch(Exception ex)
                {
                    throw new Exception("Not possible to edit the capacity of the product " + ex);
                }
            }
            else if (capacity <= 0)
            {
                NotPositiveQuantityMessage message = new NotPositiveQuantityMessage();
                actionResult = BadRequest(message);
                
            }
            else
            {
                QuantityTooLowMessage message = new QuantityTooLowMessage();
                actionResult = BadRequest(message); ;
            }

            return actionResult;
            
            //throw new NotImplementedException();
        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooHighMessage)
        public IActionResult ReceiveProduct(int productId, int qty)
        {
            Microsoft.AspNetCore.Mvc.ActionResult actionResult = null;
            int qtdeAtual = 0;
            foreach (ProductRecord productRecord in _warehouseRepository.GetProductRecords())
            {
                if (productRecord.ProductId == productId)
                {
                    qtdeAtual = productRecord.Quantity;
                }
            }
            if (qty > 0 && qty <= 999)
            {
                foreach (ProductRecord productRecord in _warehouseRepository.GetProductRecords())
                {
                    if (productRecord.ProductId == productId)
                    {
                        try
                        {
                            _warehouseRepository.SetProductRecord(productId, qtdeAtual + qty);
                            actionResult =  Ok();
                            break;
                        }
                        catch(Exception ex)
                        {
                            throw new Exception("Not possible to edit the quantity of the product " + ex);
                        }
                    }
                }
            }
            else if(qty <= 0)
            {
                NotPositiveQuantityMessage message = new NotPositiveQuantityMessage();
                actionResult = BadRequest(message);
            }
            else
            {
                QuantityTooHighMessage message = new QuantityTooHighMessage();
                actionResult = BadRequest(message);
            }
            return actionResult;
            //throw new NotImplementedException();
        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooHighMessage)
        public IActionResult DispatchProduct(int productId, int qty)
        {
            Microsoft.AspNetCore.Mvc.ActionResult actionResult = null;
            int qtdeAtual = 0;
            foreach (ProductRecord productRecord in _warehouseRepository.GetProductRecords())
            {
                if (productRecord.ProductId == productId)
                {
                    qtdeAtual = productRecord.Quantity;
                }
            }
            if (qty > 0 && qty < qtdeAtual)
            {
                try
                {
                    _warehouseRepository.SetProductRecord(productId, qtdeAtual - qty);
                    actionResult = Ok();
                }

                catch (Exception ex)
                {
                    throw new Exception("Not possible to edit the capacity of the product " + ex);
                }
            }
            else if (qty <= 0)
            {
                NotPositiveQuantityMessage message = new NotPositiveQuantityMessage();
                actionResult = BadRequest(message);
            }
            else
            {
                QuantityTooHighMessage message = new QuantityTooHighMessage();
                actionResult = BadRequest(message);
            }
            return actionResult;
            //throw new NotImplementedException();
        }
    }
}
