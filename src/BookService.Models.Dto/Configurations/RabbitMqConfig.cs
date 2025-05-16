using UniversityHelper.Core.BrokerSupport.Attributes;
using UniversityHelper.Core.BrokerSupport.Configurations;
using UniversityHelper.Models.Broker.Common;
using UniversityHelper.Models.Broker.Requests.Auth;
using UniversityHelper.Models.Broker.Requests.Email;
using UniversityHelper.Models.Broker.Requests.Image;
using UniversityHelper.Models.Broker.Requests.Office;
using UniversityHelper.Models.Broker.Requests.Position;
using UniversityHelper.Models.Broker.Requests.Rights;
using UniversityHelper.Models.Broker.Requests.TextTemplate;
using UniversityHelper.Models.Broker.Requests.User;

namespace UniversityHelper.BookService.Models.Dto.Configurations;

public class RabbitMqConfig : BaseRabbitMqConfig
{
  //public string GetUserRolesEndpoint { get; set; }
  //public string CreateUserRoleEndpoint { get; set; }
  //public string DisactivateUserRoleEndpoint { get; set; }
  //public string ActivateUserRoleEndpoint { get; set; }
  //public string FilterRolesEndpoint { get; set; }


  [AutoInjectRequest(typeof(IGetUsersDataRequest))]
  public string GetUsersDataEndpoint { get; set; }

  [AutoInjectRequest(typeof(ICheckUsersExistence))]
  public string CheckUsersExistenceEndpoint { get; set; }
}
