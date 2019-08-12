using System;
using System.Collections.Generic;
using Auction.Net.Models;
using Auction.Net.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Net.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IBidInfoRepository _bidInfoRepository;
        public ProductController(IProductRepository productRepository, IBidInfoRepository bidInfoRepository)
        {
            _productRepository = productRepository;
            _bidInfoRepository = bidInfoRepository;
        }

        // GET api/product
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productRepository.GetProducts();
        }

        // GET api/product/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productRepository.GetProduct(id);
        }

        // POST api/product
        [HttpPost]
        public void Post([FromBody]Product value)
        {
            //TODO: Implement post
        }

        // PUT api/product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Product value)
        {
            //TODO: Implement put
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //TODO: Implement delete
        }

        [HttpPost("{id}/bid")]
        public ActionResult Bid(int id, [FromBody]BidInfo value)
        {
            var product = _productRepository.GetProduct(id);
            if (product == null) return BadRequest("The product doesn't exist.");
            if ((product.LastBid == null || product.LastBid?.MaxBid < value.MaxBid) && product.ExpirationDate > DateTime.Now)
            {
                value.ProductId = id;
                value.BidId = _bidInfoRepository.AddBidInfo(value);
                product.LastBid = value;
                return Ok();
            }
            else if (product.LastBid?.MaxBid < value.MaxBid)
            {
                return BadRequest("The bid is lower than the actual maximum bid.");
            }
            else
            {
                return BadRequest("The auction has expired.");
            }
        }
    }
}
