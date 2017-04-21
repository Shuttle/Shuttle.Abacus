using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Abacus.DataContracts;
using Abacus.Domain;
using Abacus.Infrastructure;
using Abacus.Web;

namespace Abacus.Endpoints
{
    public class CalculationsController : EndpointController
    {
        private readonly IArgumentService argumentService;
        private readonly IMethodRepository methodRepository;

        public CalculationsController(IArgumentService argumentService, IMethodRepository methodRepository)
        {
            this.argumentService = argumentService;
            this.methodRepository = methodRepository;
        }

        public ActionResult Index()
        {
            switch (Request.HttpMethod.ToLower())
            {
                case "post":
                {
                    return CalculatePremium();
                }
            }

            throw new HttpException(405, "Only POST allowed.");
        }

        private ActionResult CalculatePremium()
        {
            return Scoped(uow =>
                          {
                              var response = new ExecuteMethodResponse();

                              var request =
                                      ObjectSerializer.Deserialize<ExecuteMethodRequest>(GetRequestInputString());

                              var methodName =
                                      request.ArgumentAnswers.Find(
                                              answer =>
                                              answer.Name.Equals(Argument.MethodName.Name,
                                                                 StringComparison.InvariantCultureIgnoreCase));

                              if (methodName == null)
                              {
                                  response.Messages.Add(new Message
                                                        {
                                                                Type = "error",
                                                                Text =
                                                                        string.Format(
                                                                        "Argument answer '{0}' has not been provided.",
                                                                        Argument.MethodName.Name)
                                                        });

                                  return Xml(response);
                              }

                              uow.WillUseNothing();
                              uow.WillUseFullObjectGraph();

                              var method = methodRepository.Get(methodName.Value);

                              if (method == null || method is InvalidMethod)
                              {
                                  response.Messages.Add(new Message
                                                        {
                                                                Type = "error",
                                                                Text =
                                                                        string.Format(
                                                                        "Could not find a method with name '{0}'.",
                                                                        methodName.Value)
                                                        });

                                  return Xml(response);
                              }

                              uow.WillUseNothing();
                              uow.WillUse<Argument>();

                              var answers = new List<KeyValuePair<string, string>>();

                              request.ArgumentAnswers.ForEach(
                                      answer => answers.Add(new KeyValuePair<string, string>(answer.Name, answer.Value)));

                              var context = argumentService.MethodContext(method.MethodName, answers);

                              if (context.OK)
                              {
                                  method.Calculate(context);
                              }

                              if (!context.OK)
                              {
                                  context.ErrorMessages.ForEach(
                                          message => response.Messages.Add(new Message
                                                                           {
                                                                                   Type = "error",
                                                                                   Text = message
                                                                           }));

                                  return Xml(response);
                              }

                              context.WarningMessages.ForEach(
                                      message => response.Messages.Add(new Message
                                                                       {
                                                                               Type = "warning",
                                                                               Text = message
                                                                       }));

                              context.InformationMessages.ForEach(
                                      message => response.Messages.Add(new Message
                                                                       {
                                                                               Type = "information",
                                                                               Text = message
                                                                       }));

                              context.Results.ForEach(result => response.CalculatedValues.Add(
                                                                        new NameValue
                                                                        {
                                                                                Name = result.Name,
                                                                                Value = result.Value.ToString(),
                                                                        }));

                              response.GraphNodes = context.GraphNodes();
                              response.Total = context.Total.Value.ToString();

                              return Xml(response);
                          }
                    );
        }
    }
}