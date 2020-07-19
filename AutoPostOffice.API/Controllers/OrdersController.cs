using AutoMapper;
using AutoPostOffice.API.Data.Entities;
using AutoPostOffice.API.Data.Repositories;
using AutoPostOffice.API.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace AutoPostOffice.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IOrderRepository orderRepository;
        private readonly IPostRepository postRepository;

        private readonly IMapper mapper;

        public OrdersController(ILoggerManager logger, IOrderRepository orderRepository, IPostRepository postRepository, IMapper mapper)
        {
            this.logger = logger;
            this.orderRepository = orderRepository;
            this.postRepository = postRepository;
            this.mapper = mapper;

        }

        [HttpGet("{orderNumber:int}", Name = "GetOrder")]
        public IActionResult GetOrder(int orderNumber)
        {
            try
            {
                var orderFromRepo = orderRepository.GetOrder(orderNumber);

                if (orderFromRepo == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<OrderModel>(orderFromRepo));
            }
            catch (Exception ex)
            {
                logger.LogError($"Message: {ex.Message}");
                logger.LogError($"StackTrace: {ex.StackTrace}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<OrderModel> AddOrder(OrderCreationModel model)
        {
            try
            {
                var post = postRepository.GetPost(model.PostNumber);
                if (post == null)
                {
                    //return NotFound(model.PostNumber);

                    var response = new HttpResponseMessage()
                    {
                        StatusCode = (HttpStatusCode)404,
                        ReasonPhrase = "You are trying to make Order for nonexistent Post Office."
                    };

                    logger.LogWarn($"Trying to make Order for nonexistent Post Office: {model.PostNumber}");

                    return StatusCode(StatusCodes.Status404NotFound, response);
                }

                if (!post.Status)
                {
                    var response = new HttpResponseMessage()
                    {
                        StatusCode = (HttpStatusCode)403,
                        ReasonPhrase = "You are trying to make Order for inactive Post Office."
                    };

                    return StatusCode(StatusCodes.Status403Forbidden, response);
                }

                var order = mapper.Map<Order>(model);

                if (orderRepository.AddOrder(order))
                {
                    var returnOrder = mapper.Map<OrderModel>(order);
                    logger.LogInfo($"Added new Order: {Newtonsoft.Json.JsonConvert.SerializeObject(returnOrder)}");
                    return CreatedAtRoute("GetOrder", new { orderNumber = returnOrder.OrderNumber }, returnOrder);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Message: {ex.Message}");
                logger.LogError($"StackTrace: {ex.StackTrace}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("{orderNumber:int}/cancel")]
        public ActionResult<OrderModel> CancelOrder(int orderNumber, OrderCancelModel model)
        {
            try
            {
                var order = orderRepository.GetOrder(orderNumber);
                if (order == null)
                {
                    logger.LogInfo($"Trying to cancel nonexistent order: {orderNumber}");

                    var response = new HttpResponseMessage()
                    {
                        StatusCode = (HttpStatusCode)404,
                        ReasonPhrase = "You are trying to cancel nonexistent Order."
                    };
                    return StatusCode(StatusCodes.Status404NotFound, response);
                }

                logger.LogInfo($"Trying to cancel order: {orderNumber}");
                bool result = orderRepository.CancelOrder(orderNumber, model.Description);

                if (result)
                {
                    var returnOrder = mapper.Map<OrderModel>(order);
                    logger.LogInfo($"Order cancelled: {Newtonsoft.Json.JsonConvert.SerializeObject(returnOrder)}");
                    return CreatedAtRoute("GetOrder", new { orderNumber = returnOrder.OrderNumber }, returnOrder);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Message: {ex.Message}");
                logger.LogError($"StackTrace: {ex.StackTrace}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("{orderNumber:int}/alter")]
        public ActionResult<OrderModel> AlterOrder(int orderNumber, OrderAlterModel model)
        {
            try
            {
                if (model.PostNumber != null || model.Status != null)
                {
                    return BadRequest();
                }

                var order = orderRepository.GetOrder(orderNumber);
                if (order == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                if (model == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                var alteredOrder = mapper.Map<Order>(model);
                bool result = orderRepository.AlterOrder(orderNumber, alteredOrder);

                if (result)
                {                    
                    var returnOrder = mapper.Map<OrderModel>(order);
                    return CreatedAtRoute("GetOrder", new { orderNumber = returnOrder.OrderNumber }, returnOrder);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Message: {ex.Message}");
                logger.LogError($"StackTrace: {ex.StackTrace}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        [HttpGet("{orderNumber:int}/tracking")]
        public IActionResult GetOrderHistory(int orderNumber)
        {
            try
            {
                var orderTrackingFromRepo = orderRepository.GetOrderHistory(orderNumber);

                if (orderTrackingFromRepo == null)
                {
                    return NotFound();
                }

                IEnumerable<OrderTrackingModel> models = mapper.Map<IEnumerable<OrderTrackingModel>>(orderTrackingFromRepo);
                return Ok(models);
            }
            catch (Exception ex)
            {
                logger.LogError($"Message: {ex.Message}");
                logger.LogError($"StackTrace: {ex.StackTrace}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
