using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApi.Models;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly OrderContext OrderDb;

        //构造函数把OrderContext 作为参数，Asp.net core 框架可以自动注入OrderContext对象
        public OrderController(OrderContext context)
        {
            this.OrderDb = context;
        }

        // GET: api/order/{OrderId}  OrderId为路径参数
        [HttpGet("{OrderId}")]
        public ActionResult<Order> GetOrder(string id)
        {
            var Order = OrderDb.Orders.FirstOrDefault(t => t.OrderId == id);
            if (Order == null)
            {
                return NotFound();
            }
            return Order;
        }

        // GET: api/order
        // GET: api/order/pageQuery?name=课程&&isComplete=true
        [HttpGet]
        public ActionResult<List<Order>> GetOrders(string name, string id)
        {
            var query = buildQuery(name, id);
            return query.ToList();
        }

        // GET: api/order/pageQuery?skip=5&&take=10  
        // GET: api/order/pageQuery?name=课程&&isComplete=true&&skip=5&&take=10
        [HttpGet("pageQuery")]
        public ActionResult<List<Order>> queryOrder(string name, string id, int skip, int take)
        {
            var query = buildQuery(name, id).Skip(skip).Take(take);
            return query.ToList();
        }

        private IQueryable<Order> buildQuery(string name, string id)
        {
            IQueryable<Order> query = OrderDb.Orders;
            if (name != null)
            {
                query = query.Where(t => t.CustomerName.Contains(name));
            }
            if (id != null)
            {
                query = query.Where(t => t.OrderId.Contains(id));
            }
            return query;
        }


        // POST: api/order
        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            try
            {
                OrderDb.Orders.Add(order);
                OrderDb.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return order;
        }

        // PUT: api/order/{OrderId}
        [HttpPut("{OrderId}")]
        public ActionResult<Order> PutOrder(string OrderId, Order order)
        {
            if (OrderId != order.OrderId)
            {
                return BadRequest("OrderId cannot be modified!");
            }
            try
            {
                OrderDb.Entry(order).State = EntityState.Modified;
                OrderDb.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();
        }

        // DELETE: api/order/{OrderId}
        [HttpDelete("{OrderId}")]
        public ActionResult DeleteOrder(string OrderId)
        {
            try
            {
                var order = OrderDb.Orders.FirstOrDefault(t => t.OrderId == OrderId);
                if (order != null)
                {
                    OrderDb.Remove(order);
                    OrderDb.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

    }
}