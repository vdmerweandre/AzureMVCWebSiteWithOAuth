Install Windows Azure SDK for .NET for Visual Studio 2013.
Install Windows Azure Powershell console

Run Windows Azure storage emulator: provides a local environment that emulates the Windows Azure Blob, Queue, and Table services for development purposes.

---Endpoints return IHttpActionResult---
Encapsulates the result of the Web API endpoints

---Enabled Cross Origin Resource Sharing CORS for Web API---
Install-Package Microsoft.AspNet.WebApi.Cors -pre -project WebService
App_Start/WebApiConfig.cs -> config.EnableCors();
Decorate Controller/Action/Globally
[EnableCors(origins: "http://myclient.azurewebsites.net", headers: "*", methods: "*")]

Architecture
http://blog.longle.net/2013/05/11/genericizing-the-unit-of-work-pattern-repository-pattern-with-entity-framework-in-mvc/

Test 
AntiForgery

Filters
http://hackwebwith.net/tag/mvc5/

OData
http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/supporting-odata-query-options
http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/odata-actions
http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/calling-an-odata-service-from-a-net-client
http://roysvork.wordpress.com/2013/02/06/how-to-deserialize-delta-from-json-using-odatamediatypeformatter-with-entity-framework-code-first/
An important thing to note here – the default implementation of Delta<T> was intended to be used with ODataMediaTypeFormatter, since it uses specialized serializers/deserializers and when using it against i.e. the default JsonMediaTypeFormatter, based on JSON.NET it runs into some issues.

Multipart/batch requests
http://blogs.msdn.com/b/webdev/archive/2013/11/01/introducing-batch-support-in-web-api-and-web-api-odata.aspx

MVC data service - not used, but good example
http://codereview.stackexchange.com/questions/25141/is-there-a-better-way-to-consume-an-asp-net-web-api-call-in-an-mvc-controller

EF6
[MapToStoredProcedures]
http://msdn.microsoft.com/en-au/data/dn468673

DBMigrations
http://elegantcode.com/2012/04/12/entity-framework-migrations-tips/

Unity & LifetimeManagers
http://jankowskigrzegorz.blogspot.com.au/2013/06/microsoft-unity-perresolvelifetimemanag.html

Repositories & models
http://www.codeproject.com/Articles/615499/Models-POCO-Entity-Framework-and-Data-Patterns

HTTP Status Codes
http://www.w3.org/Protocols/HTTP/HTRESP.html

Ko performance tips
http://www.knockmeout.net/2012/03/knockoutjs-performance-gotcha-1ifwith.html

HATEOAS