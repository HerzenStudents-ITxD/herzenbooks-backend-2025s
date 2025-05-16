//using UniversityHelper.Core.BrokerSupport.Broker;
//using UniversityHelper.BookService.Data.Interfaces;
//using MassTransit;
//using UniversityHelper.Models.Broker.Requests.Book;

//namespace UniversityHelper.BookService.Broker.Consumers;

//public class CheckBookAccessConsumer : IConsumer<ICheckBookAccessRequest>
//{
//    private readonly IBookAgentRepository _agentRepository;

//    public CheckBookAccessConsumer(IBookAgentRepository agentRepository)
//    {
//        _agentRepository = agentRepository;
//    }

//    public async Task Consume(ConsumeContext<ICheckBookAccessRequest> context)
//    {
//        var response = OperationResultWrapper.CreateResponse(CheckAccessAsync, context.Message);

//        await context.RespondAsync<IOperationResult<bool>>(response);
//    }

//    private async Task<object> CheckAccessAsync(ICheckBookAccessRequest request)
//    {
//        return await _agentRepository.IsModeratorAsync(request.UserId, request.BookId) ||
//               await _agentRepository.IsAgentAsync(request.UserId, request.BookId);
//    }
//}