using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;

namespace Shop.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string body)
        {
            MailjetClient client = new MailjetClient("d985a008719033368b56193f7f5c7cd5", "93a9523140163a78a9e3e12bdf1f044b")
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource,
                }
                .Property(Send.Messages, new JArray {
                    new JObject {
                        {
                            "From",
                            new JObject {
                                {"Email", "yamailpo4ta@gmail.com"},
                                {"Name", "Yaroslav"}
                            }
                        }, {
                            "To",
                            new JArray {
                                new JObject {
                                    {
                                        "Email",
                                        email
                                    }, {
                                        "Name",
                                        "DotNetMastery"
                                    }
                                }
                            }
                        }, {
                            "Subject",
                            subject
                        }, {
                            "HTMLPart",
                            body
                        }
                    }
                });
            await client.PostAsync(request);
        }
    }
}
