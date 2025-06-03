using Apps.RePurpose.Actions;
using Apps.RePurpose.Connections;
using Apps.RePurpose.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.RePurpose.Base;

namespace Tests.RePurpose;

[TestClass]
public class RepurposeTests : TestBase
{
    [TestMethod]
    public async Task Simple_text_works()
    {
        var actions = new Actions(InvocationContext, FileManagementClient);
        var content = "Blackbird is an integration platform-as-a-service (iPaaS) solution. We can solve the problem of applications operating in isolation and in dispersed modalities by enabling integration, automation, and seamless analytics between the apps, data, content, participants, and devices used for localization and globalization management. However, Blackbird goes beyond connecting applications, data, and content within an organization. It can also connect organizations into networks - e.g. the end clients who create the content with multiple localization vendors.​\r\n\r\nOur mission is to become the global language services industry’s best iPaaS and automation-platform-as-a-service technology vendor.\r\n\r\nAt Blackbird, our integration platform revolves around Birds. All the time.\r\n\r\nLines of code are transformed into the four forces of flight: lift (triggers), weight (actions), thrust (apps), and drag (connections). The way the four forces act on your Bird makes it do different things. Fine, but how, you could ask.\r\n\r\nThink about the apps you use for your business. Your initial list should have at least one e-mail client, project management software, translation memory software or billing software. You can connect and make them communicate with each other in Blackbird.\r\n\r\nLet’s assume that you get a request for a quote from a client of yours in an e-mail. Normally you would open the e-mail, save the file you receive, create a project in your project management software and copy info from the email there, open your translation memory software, run an analysis on the file, and finally open your billing software and generate the quote based on the CAT analysis.\r\n\r\nOn the other hand, at Blackbird, you can create this workflow as a Bird, execute it in an automated way - and use it in Flights; yes, we love aviation.\r\n\r\nFirst of all your Bird will need a trigger - the request for quote email from your client in this example. From now on when you get an email from this client, your Bird will automatically execute the series of actions you define in the next step(s) - like saving the file, creating the project, copying the info from the body of the email, opening the translation memory software, running analysis, and generating the quote. Once you published your Bird, it will fly every time a new email from your client comes in. As simple as that.";
        var instructions = "Explain this in a social media post to a younger audience. Use emojis where possible.";
        var result = await actions.RepurposeContent(content, new RepurposeRequest { StyleGuide = instructions });
        Console.WriteLine(result.RepurposedText);
        Assert.IsNotNull(result.RepurposedText);
    }

    [TestMethod]
    public async Task Simple_txt_works()
    {
        var actions = new Actions(InvocationContext, FileManagementClient);
        var file = new FileReference { Name = "blackbird.txt" };
        var instructions = "Explain this in a LinkedIn post to a professional audience.";
        var result = await actions.RepurposeFile(new FileRequest { File = file }, new RepurposeRequest { StyleGuide = instructions });
        Console.WriteLine(result.RepurposedText);
        Assert.IsNotNull(result.RepurposedText);
    }

    [TestMethod]
    public async Task Xliff_works()
    {
        var actions = new Actions(InvocationContext, FileManagementClient);
        var file = new FileReference { Name = "contentful.html.xliff" };
        var instructions = "Explain this in a LinkedIn post to a professional audience.";
        var result = await actions.RepurposeFile(new FileRequest { File = file }, new RepurposeRequest { StyleGuide = instructions });
        Console.WriteLine(result.RepurposedText);
        Assert.IsNotNull(result.RepurposedText);
    }

    [TestMethod]
    public async Task Html_works()
    {
        var actions = new Actions(InvocationContext, FileManagementClient);
        var file = new FileReference { Name = "contentful.html" };
        var instructions = "Pay attention.";
        var result = await actions.RepurposeFile(new FileRequest { File = file }, new RepurposeRequest { StyleGuide = instructions, Touchpoint = "Tweet" });
        //Console.WriteLine(result.SystemPrompt);
        //Console.WriteLine(result.RepurposedText);
        Console.WriteLine(result.RepurposedText.Length);
        Assert.IsNotNull(result.RepurposedText);
    }
}